<template>
  <sp-blog-card :fetch="fetchData"></sp-blog-card>
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
