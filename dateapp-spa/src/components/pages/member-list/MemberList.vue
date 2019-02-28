<template>
    <v-content>
      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex xs12 sm6 md3  v-for="member in members" :key="member.id">
              <app-member-card :user="member" ></app-member-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
</template>
<script>
import axios from 'axios';
import {URLAPI} from '../../../config/variables.js';
import MemberCard from '../../member-card/MemberCard';

export default {
      components:{
          'app-member-card':MemberCard
      },
      data: () => ({
          members:[]
      }),
      methods:{
          loadingMembers(){
             return axios({url: URLAPI+'users', method: 'GET' })
              .then(resp => {
                  this.members = resp.data;
              })
              .catch(err=> this.$alertify.error(err));
          }
      },
      created() {
          this.loadingMembers();
      },

}
</script>