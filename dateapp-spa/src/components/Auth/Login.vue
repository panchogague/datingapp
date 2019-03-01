<template>
  <div>
  <v-form
    ref="form"
    v-model="valid"
    lazy-validation
    v-if="!isLogin"
  >
    <v-layout row wrap class="text-lg-right">
      <v-spacer></v-spacer>
       <v-flex xs8 sm6 md3>
              <v-text-field
                  label="User Name"
                  flat
                  solo-inverted
                  hide-details
                  required
                  class="pa-1"
                  v-model="userName"
                  ></v-text-field>
      </v-flex>
      <v-flex xs8 sm6 md3>
                  <v-text-field
                  label="Password"
                  type="password"
                  flat
                  solo-inverted
                  hide-details
                  required
                  class="pa-1"
                  v-model="password"
                  ></v-text-field>
      </v-flex>
      <v-flex xs6 sm4 md2 class="pa-1">
        <v-btn color="success" @click="login"
         >
          <span class="mr-2" >Login</span>
        </v-btn> 
      </v-flex>
    </v-layout>
  </v-form>
   <div class="text-xs-center" v-if="isLogin">
    <v-menu offset-y>
      <v-btn
        slot="activator"
        flat
      >
        Welcome {{userLogIn}}
        <v-icon>keyboard_arrow_down</v-icon>
      </v-btn>
      <v-list>
        <v-list-tile to="/member/edit">
          <v-list-tile-title><v-icon>account_circle</v-icon> Edit</v-list-tile-title>
        </v-list-tile>
        <v-list-tile @click="logOut">
          <v-list-tile-title><v-icon>input</v-icon> Log Out</v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-menu>
  </div>
  </div>
</template>
<script>
export default {
  data () {
    return {
      userName:'',
      password:'',
      valid:true,
      errorUsername:'',
      errorPassword:''
    }
  },
  methods: {
    login () {
      // this.valid=true;
      // if(this.userName === ''){
      //   this.valid=false;
      //   this.errorUsername ="Ingrese usuario";
      // }

      // if(this.password === ''){
      //    this.valid=false;
      //    this.errorUsername ="Ingrese password";
      // }
     
      // if(!this.valid) {
      //   return;
      // }

      this.$store.dispatch('login', 
      {  
        username: this.userName,
        password: this.password 
      })
      .then(response => {
        this.$alertify.success('login success');
        this.$router.push('member') 
      })
      .catch(e => {
        this.$alertify.error('user or password wrong');
      });
    },
    logOut () {
      this.$store.dispatch('logout').then(resp=>
      {
        this.userName='';
        this.password='';
        this.$alertify.success('log out success');
        this.$router.push('/') 
      });
    }
  },
  computed: {
    isLogin() {
      return this.$store.getters.isLoggedIn
    },
    userLogIn () {
      return this.$store.getters.userName;
    }
  },
}
</script>