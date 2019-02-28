<template>
    <div>
        Lists
    </div>
</template>
<script>
import axios from 'axios';
import {URLAPI} from '../../../config/variables.js';
export default {
      data: () => ({}),
      created() {
          axios({url: URLAPI+'users', method: 'GET' })
              .then(resp => {
                  console.log(resp);
              })
              .catch(err=> console.log(err));
      },

}
</script>