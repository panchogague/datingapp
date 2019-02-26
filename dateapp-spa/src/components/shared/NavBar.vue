<template>
    <v-toolbar app>
      <v-toolbar-title  >
         <v-btn class="headline text-uppercase pa-3" flat to="/">Dating App</v-btn>
      </v-toolbar-title>
       <v-toolbar-items class="hidden-sm-and-down"  v-if="isLogin">
      <v-btn flat to="/member">Matches</v-btn>
      <v-btn flat to="/lists">Lists</v-btn>
      <v-btn flat to="/messages">Messages</v-btn>
    </v-toolbar-items>
      <v-spacer></v-spacer>
      <app-login></app-login>
    </v-toolbar>
</template>
<script>
import Login from '../Auth/Login'
export default {
     components: {
    'app-login':Login
  },
  data () {
    return {
      //
    }
  },
   computed: {
    isLogin() {
      return this.$store.getters.isLoggedIn
    }
   }
}
</script>