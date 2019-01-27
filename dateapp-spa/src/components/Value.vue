<template>
  <ul>
    <li v-for="item in values" :key="item.id">{{item.name}}</li>
  </ul>
</template>
<script>
export default {
  data: () => ({
    values: []
  }),
  created() {
    this.$http
      .get("https://localhost:44386/api/values")
      .then(response => {
          
        this.values = response.data;
      })
      .catch(e => {
        alert(e);
      });
  }
}
</script>

