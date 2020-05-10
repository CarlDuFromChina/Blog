<template>
  <sp-blog-card
    :fetch="fetchData"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
    @loading="loading = true"
    @loading-close="loading = false"
  ></sp-blog-card>
</template>

<script>
export default {
  name: 'blogList',
  data() {
    return {
      loading: false
    };
  },
  methods: {
    fetchData() {
      const searchList = [{ Name: 'blog_type', Value: this.$route.name }];
      return sp
        .get(`api/blog/GetDataList?orderBy=&pageSize=10&pageIndex=1&searchList=${JSON.stringify(searchList)}`)
        .then(resp => resp)
        .catch(() => this.$message.error('加载出错了'));
    }
  }
};
</script>

<style></style>
