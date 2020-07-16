<template>
  <a-layout>
    <!-- 博客 -->
    <a-layout-sider width="60%" theme="light">
      <h3>最新博客</h3>
      <!-- <sp-blog-card ref="blog" :fetch="fetchData" readonly newTag v-infinite-scroll="loadMore"></sp-blog-card> -->
    </a-layout-sider>
    <!-- 博客 -->
    <a-layout-sider width="30%" style="overflow:hidden" theme="light">
      <!-- 推荐 -->
      <recommand-pictures></recommand-pictures>
      <!-- 推荐 -->

      <!-- 推荐博客 -->
      <recommand-list></recommand-list>
      <!-- 推荐博客 -->

      <!-- 想法 -->
      <idea :pageSize="5"></idea>
      <!-- 想法 -->
    </a-layout-sider>
  </a-layout>
</template>

<script>
import recommandList from './recommandList';
import recommandPictures from './recommandPictures';
import idea from './idea';

export default {
  name: 'home',
  components: { recommandList, recommandPictures, idea },
  data() {
    return {
      loading: 'false',
      blog: {
        pageSize: 20,
        pageIndex: 1,
        totalRecords: 0,
        allowLoad: () => {
          return this.pageSize * this.pageIndex < this.totalRecords;
        }
      }
    };
  },
  methods: {
    loadMore() {
      if (this.blog.allowLoad()) {
        this.blog.pageSize += 10;
        this.$refs.blog.loadData();
      }
    },
    fetchData() {
      return sp
        .get(`api/blog/GetDataList?orderBy=createdon desc&pageSize=${this.blog.pageSize}&pageIndex=${this.blog.pageIndex}&searchList=`)
        .then(resp => {
          this.blog.totalRecords = resp.RecordCount;
          return resp;
        })
        .catch(() => this.$message.error('加载出错了'));
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-layout-sider-light {
  background: #edeef2;
}
</style>
