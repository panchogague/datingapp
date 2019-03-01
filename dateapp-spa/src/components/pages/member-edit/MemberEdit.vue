<template> 
      <v-container grid-list-md>
          <v-layout  row wrap>
            <v-flex xs4>
                 <h1>Your Profile</h1> <br>
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
                      <div class="headline">{{user.knowUs}}</div>
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
               <v-btn color="primary" @click="updateMember" :disabled="!dirty">Save Changes</v-btn>
              </v-card-actions>
            </v-card>
            </v-flex><v-spacer></v-spacer>
            <v-flex xs7>
                <v-alert type="info" :value="dirty">
                    <strong>Information: You have made changes. Any unsaved changes will be lost!</strong>
                    </v-alert>
                <v-tabs
                v-model="active"
                fixed-tabs
                >
                    <v-tab>
                           Edit Profile
                    </v-tab>
                    <v-tab-item class="pa-3">
                        <v-form>
                            <h4>Description</h4>
                            <v-textarea
                                v-model="user.introduction"
                                hint="Hint text"
                                ></v-textarea>
                            <h4>Looking For</h4>
                             <v-textarea
                                v-model="user.lookingFor"
                                hint="Hint text"
                                ></v-textarea>
                            <h4>Interests</h4>
                             <v-textarea
                                v-model="user.interests"
                                hint="Hint text"
                                ></v-textarea>
                            <h4>Location Details</h4>
                            <v-layout>
                                <v-flex xs12 md4>
                                    <v-text-field label="Country" type="text" v-model="user.country"></v-text-field>
                                </v-flex>
                                <v-flex xs12 md4>
                                    <v-text-field label="City" type="text" v-model="user.city"></v-text-field>
                                </v-flex>
                            </v-layout>
                        </v-form>

                    </v-tab-item>
                     <v-tab>
                          Edit Photos
                    </v-tab>
                    <v-tab-item class="pa-3">
                        <p>fotos edit will be here</p>
                    </v-tab-item>
    </v-tabs>
            </v-flex>
        </v-layout>
      </v-container>
    
</template>
<script>
import UserService from '../../../services/userService.js';
export default {
     data: () => ({
          user:{},
          dirty: false,
           active: null,
          }),
     computed: {
        userId() {
        return this.$store.getters.userId
        }
     },
     methods:{
         updateMember(){
             UserService.updateMember(this.userId,this.user)
             .then(resp=>{
                this.dirty=false;
                this.$alertify.success("Profile update successfully");
             }).catch((err)=> this.$alertify.error(err))
             
         }
     },
      mounted() {
          UserService.getMember(this.userId).then(response => {
               this.user = response
           })
           .catch(error => {
               this.$alertify.error(error);
           })
      },
      watch:
        {
            user:
            {
                handler(newVal, oldVal)
                {
                    this.dirty = !(oldVal !== '' && oldVal !== newVal);
                },
                deep: true
            }
        },
        beforeRouteLeave  (to, from, next) {
            if(this.dirty){
                if(confirm ('Are you sure you want to continue? Any unsaved changes will be lost')){
                     next();
                     return;
                }
                return;
            }
            next();
        }
}
</script>