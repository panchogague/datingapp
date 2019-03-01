<template>
  <v-layout >
    <v-flex xs12 sm8 d-flex>
         <v-hover>
      <v-card  slot-scope="{ hover }"
      :class="`elevation-${hover ? 12 : 2}`">
        <v-img
          class="white--text"
          height="200px"
          aspect-ratio="2.75"
          :src="user.photoUrl"
        >
        </v-img>
        <v-card-title>
          <div>
            <span class="grey--text">
                <b><v-icon>account_box</v-icon>
                    {{user.knowAs}}
                </b>, {{user.age}}
            </span><br>
            <div> {{ user.city }}, {{ user.country }} </div>
          </div>
        </v-card-title>
        <v-card-actions>
          <v-btn flat color="orange" :to="'/members/'+user.id"><v-icon>account_box</v-icon></v-btn>
          <v-btn flat color="orange"><v-icon>favorite</v-icon></v-btn>
          <v-btn flat color="orange"><v-icon>email</v-icon></v-btn>
        </v-card-actions>
      </v-card>
                </v-hover>
    </v-flex>
  </v-layout>
</template>
<script>
export default {
    props:[
        'user'
    ]
}
</script>

