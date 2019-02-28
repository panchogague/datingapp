import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import Axios from 'axios'
import {store} from './stores/store'
import {router} from './routers/router'
import VueAlertify from 'vue-alertify'
import moment from 'moment'

Vue.filter('formatDate', function(value) {
  if (value) {
    return moment(String(value)).format(" MMMM Do YYYY")
  }
});
Vue.use(VueAlertify);
Vue.prototype.$http = Axios;
const token = localStorage.getItem('token')
if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = 'Bearer '+ token
}
Vue.config.productionTip = false

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next()
      return
    }
    next('/') 
  } else {
    next() 
  }
})
new Vue({
  store,
  router,
  render: h => h(App),
  created() {
    store.dispatch('setTokenUser')
  },
}).$mount('#app')
