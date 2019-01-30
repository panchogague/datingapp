<template>
  <v-app>
    <app-nav-bar></app-nav-bar>
    <v-content>
     <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
import NavBar from './components/shared/NavBar'

export default {
  name: 'App',
  components: {
    'app-nav-bar':NavBar
  },
  data () {
    return {
      //
    }
  }
}
</script>
