<template>
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase pa-3">
        <span>Dating App</span>
      </v-toolbar-title>
       <v-toolbar-items class="hidden-sm-and-down">
      <v-btn flat>Matches</v-btn>
      <v-btn flat>Lists</v-btn>
      <v-btn flat>Messages</v-btn>
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
  }
}
</script>