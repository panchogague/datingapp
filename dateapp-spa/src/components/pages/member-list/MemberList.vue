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

import MemberCard from '../../member-card/MemberCard';
import UserService from '../../../services/userService.js';
export default {
      components:{
          'app-member-card':MemberCard
      },
      data: () => ({
          members:[],
          pageNumber:1,
          pageSize:5
      }),
      methods:{
          loadingMembers(){
            UserService.getMembers(this.pageNumber,this.pageSize).then(response => {
               this.members = response.data
           })
           .catch(error => {
               this.$alertify.error(error);
           })
          }
      },
      mounted() {
          this.loadingMembers();
      },
      

}
</script>