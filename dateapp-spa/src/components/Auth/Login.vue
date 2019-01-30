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
   <v-toolbar-items class="hidden-sm-and-down" v-if="isLogin">
      <span class="p-1">Welcome {{userName}}</span>
      <v-btn flat @click="logOut">Log Out</v-btn>
    </v-toolbar-items>
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
      this.valid=true;
      if(this.userName === ''){
        this.valid=false;
        this.errorUsername ="Ingrese usuario";
      }

      if(this.password === ''){
         this.valid=false;
         this.errorUsername ="Ingrese password";
      }
     
      if(!this.valid) {
        return;
      }

      this.$store.dispatch('login', 
      {  
        username: this.userName,
        password: this.password 
      })
      .then(response => {
        console.log(response);
      })
      .catch(e => {
        alert(e);
      });
    },
    logOut () {
      this.$store.dispatch('logout').then(resp=>
      {
        this.userName='';
        this.password='';
      });
    }
  },
  computed: {
    isLogin() {
      return this.$store.getters.isLoggedIn
    }
  },
}
</script>