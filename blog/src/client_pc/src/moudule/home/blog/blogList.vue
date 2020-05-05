<template>
  <sp-blog-card :fetch="fetchData"></sp-blog-card>
</template>

<script>
export default {
  name: 'blogList',
  methods: {
    fetchData() {
      const searchList = [{ Name: 'blog_type', Value: this.$route.name }];
      return sp.get(`api/blog/GetDataList?orderBy=&pageSize=10&pageIndex=1&searchList=${JSON.stringify(searchList)}`).then(resp => resp);
    }
  }
};
</script>

<style></style>
