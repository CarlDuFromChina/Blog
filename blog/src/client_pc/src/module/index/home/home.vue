<template>
  <a-layout>
    <!-- 博客 -->
    <a-layout-sider width="70%" theme="light">
      <h3>最新博客</h3>
      <blog-list></blog-list>
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
import blogList from './blogList';

export default {
  name: 'home',
  components: { recommandList, recommandPictures, idea, blogList },
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
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-layout-sider-light {
  background: #edeef2;
}
</style>
