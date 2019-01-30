<template>
  <v-jumbotron>
    <v-container fill-height>
      <v-layout align-center>
        <v-flex>
          <h3 class="display-3">Find your match</h3>

          <span class="subheading">Come on in to view your matches... All you need to do is sign up!</span>

          <v-divider class="my-3"></v-divider>
          <v-btn
            class="mx-0 ma-1"
            color="primary"
            large
            to="/register"
          >
            Register
          </v-btn>          
          <v-btn
            class="mx-0"
            color="warning"
            large
          >
            See more
          </v-btn>
        </v-flex>
      </v-layout>
    </v-container>
  </v-jumbotron>
</template>