<template>
  <sp-blog-table :fetch="fetchData"></sp-blog-table>
</template>

<script>
export default {
  name: 'csharp',
  computed: {
    searchList() {
      return [{ Name: 'blog_type', Value: 'csharp' }];
    }
  },
  methods: {
    fetchData() {
      return sp.get(`api/blog/GetDataList?searchList=${JSON.stringify(this.searchList)}`).then(resp => resp);
    }
  }
};
</script>

<style></style>
