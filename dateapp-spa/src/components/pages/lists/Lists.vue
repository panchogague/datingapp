<template>
    <div>
        Lists
    </div>
</template>
<script>
import UserService from '../../../services/userService.js';
export default {
      data: () => ({
          members:[],
          pageNumber:1,
          pageSize:5
      }),
      created() {
          UserService.getMembers(this.pageNumber,this.pageSize,'likeer')
              .then(resp => {
                  console.log(resp);
              })
              .catch(err=> console.log(err));
      },

}
</script>