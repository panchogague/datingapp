<template>
    
      <v-container grid-list-md>
          <v-layout>
            <v-flex xs4>
                 <v-card>
              <v-layout>
                <v-flex xs7 ma-2>
                  <v-img
                    :src="user.photoUrl"
                    height="200px"
                    contain
                  ></v-img>
                </v-flex>
                <v-flex xs7>
                  <v-card-title primary-title>
                    <div>
                      <div class="headline">{{user.knowAs}}</div>
                      <div>{{user.city}}, {{user.country}}</div>
                      <div>{{user.age}} years</div>
                       <div>
                        <strong>Last Active:</strong>
                        <p>{{user.lastActive | formatDate}}</p>
                    </div>
                    <div>
                        <strong>Member since:</strong>
                        <p>{{user.created | formatDate}}</p>
                    </div>
                    </div>
                  </v-card-title>
                </v-flex>
              </v-layout>
              <v-divider light></v-divider>
              <v-card-actions class="pa-3">
               
                <v-spacer></v-spacer>
               <v-btn flat color="primary"><v-icon>thumb_up_alt</v-icon>Like</v-btn>
                <v-btn flat color="secondary"><v-icon>message</v-icon>Message</v-btn>
              </v-card-actions>
            </v-card>
            </v-flex><v-spacer></v-spacer>
            <v-flex xs7>
                <v-tabs
                v-model="active"
                fixed-tabs
                >
                    <v-tab>
                           About {{ user.knowAs }}
                    </v-tab>
                    <v-tab-item class="pa-3">
                        <h4>Description</h4>
                        <p>{{user.introduction}}</p>
                        <h4>Looking For</h4>
                        <p>{{user.lookingFor}}</p>
                    </v-tab-item>
                     <v-tab>
                          Interests
                    </v-tab>
                    <v-tab-item class="pa-3">
                        <h4>Interests</h4>
                        <p>{{user.interests}}</p>
                    </v-tab-item>
                    <v-tab>
                          Photos
                    </v-tab>
                    <v-tab-item class="pa-3">
                       <v-carousel
                            delimiter-icon="stop"
                            prev-icon="mdi-arrow-left"
                            next-icon="mdi-arrow-right"
                        >
                            <v-carousel-item
                            v-for="(item,i) in items"
                            :key="i"
                            :src="item.src"
                            ></v-carousel-item>
                        </v-carousel>
                    </v-tab-item>
                     <v-tab>
                          Messages
                    </v-tab>
                    <v-tab-item class="pa-3">
                        <p>Messages will go here</p>
                    </v-tab-item>
    </v-tabs>
            </v-flex>
        </v-layout>
      </v-container>
    
</template>
<script>
import axios from 'axios';
import {URLAPI} from '../../../config/variables.js';
export default {
     data: () => ({
          user:{}, 
          active: null,
          items: [
          {
            src: 'https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg'
          },
          {
            src: 'https://cdn.vuetifyjs.com/images/carousel/sky.jpg'
          },
          {
            src: 'https://cdn.vuetifyjs.com/images/carousel/bird.jpg'
          },
          {
            src: 'https://cdn.vuetifyjs.com/images/carousel/planet.jpg'
          }
        ]
      }),
      methods:{
          loadingMember(id){
             return axios({url: URLAPI+'users/'+id, method: 'GET' })
              .then(resp => {
                  this.user = resp.data;
                  console.log(this.user);
              })
              .catch(err=> this.$alertify.error(err));
          },
           
      },
      created() {
          this.loadingMember(this.$route.params.id);
      }
}
</script>