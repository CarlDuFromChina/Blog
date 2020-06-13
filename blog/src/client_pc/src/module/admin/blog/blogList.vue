<template>
  <sp-blog-card ref="blog" :fetch="fetchData" v-infinite-scroll="load" style="overflow:auto;"></sp-blog-card>
</template>

<script>
export default {
  name: 'blogList',
  data() {
    return {
      loading: false,
      pageSize: 16,
      pageIndex: 1,
      totalRecords: 0
    };
  },
  methods: {
    load() {
      if (this.pageSize * this.pageIndex < this.totalRecords) {
        this.pageSize += 8;
        this.$refs.blog.loadData();
      }
    },
    fetchData() {
      const searchList = [{ Name: 'blog_type', Value: this.$route.params.type }];
      return sp
        .get(`api/blog/GetDataList?orderBy=&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=${JSON.stringify(searchList)}`)
        .then(resp => {
          this.totalRecords = resp.RecordCount;
          return resp;
        })
        .catch(() => this.$message.error('加载出错了'));
    }
  }
};
</script>

<style></style>
