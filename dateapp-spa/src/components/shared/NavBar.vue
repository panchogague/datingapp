<template>
    <v-toolbar app>
      <v-toolbar-title  >
         <v-btn class="headline text-uppercase pa-3" flat to="/">Dating App</v-btn>
      </v-toolbar-title>
       <v-toolbar-items class="hidden-sm-and-down"  v-if="isLogin">
      <v-btn flat to="/members">Matches</v-btn>
      <v-btn flat to="/lists">Lists</v-btn>
      <v-btn flat to="/messages">Messages</v-btn>
    </v-toolbar-items>
      <v-spacer></v-spacer>
      <v-avatar v-if="isLogin"
         size="45"
          color="grey lighten-4"
        >
          <img :src="avatarUrl" alt="avatar">
        </v-avatar>
      <app-login></app-login>
    </v-toolbar>
</template>
<script>
import Login from '../Auth/Login'
import UserService from '../../services/userService.js';
export default {
     components: {
    'app-login':Login
  },
  data () {
    return {
    }
  },
  mounted(){
    if(!this.isLogin){
      return;
    }
    UserService.getMember(this.userId).then(response => {
      console.log(response.photoUrl)
               this.$store.dispatch('setAvatarUrl', 
              {  
                url: response.photoUrl
              })

           })
           .catch(error => {
               this.$alertify.error(error);
           })
   
  },
   computed: {
    isLogin() {
      return this.$store.getters.isLoggedIn
    },
     userId() {
        return this.$store.getters.userId
        },
         avatarUrl() {
        return this.$store.getters.avatarUrl
        }
   }
}
</script>