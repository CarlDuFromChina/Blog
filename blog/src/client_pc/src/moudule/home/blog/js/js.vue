<template>
  <sp-blog-table :fetch="fetchData"></sp-blog-table>
</template>

<script>
export default {
  name: 'js',
  created() {},
  methods: {
    fetchData() {
      return sp.get(`api/blog/GetDataList?searchList=${JSON.stringify(this.searchList)}`).then(resp => resp);
    }
  },
  computed: {
    searchList() {
      return [{ Name: 'blog_type', Value: 'js' }];
    }
  }
};
</script>

<style></style>
