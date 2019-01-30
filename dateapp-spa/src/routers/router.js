import Home from '../components/Home'
import Register from '../components/Auth/Register'
import VueRouter from 'vue-router'
import Vue from 'vue'

Vue.use(VueRouter)

const routesConfig =  [
    {path:'/', component:Home},
    {path:'/register', component:Register}
];
export const router = new VueRouter({routes:routesConfig});