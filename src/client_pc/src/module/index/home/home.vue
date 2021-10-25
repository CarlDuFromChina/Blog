<template>
  <a-layout>
    <!-- 博客 -->
    <a-layout-sider width="70%" theme="light">
      <blog-list></blog-list>
    </a-layout-sider>
    <!-- 博客 -->
    <a-layout-sider width="30%" style="overflow:hidden" theme="light">
      <!-- 精品文章 -->
      <series-list style="margin-bottom:20px;"></series-list>
      <!-- 精品文章 -->

      <!-- 推荐博客 -->
      <recommand-list style="margin-bottom:20px;"></recommand-list>
      <!-- 推荐博客 -->

      <!-- 想法 -->
      <idea :pageSize="5" style="margin-bottom:20px;"></idea>
      <!-- 想法 -->

      <!-- 项目链接 -->
      <links style="margin-bottom:20px;"></links>
      <!-- 项目链接 -->

      <div class="more">
        <p>作者：Karl Du</p>
        <p>博客仓库地址：<a href="https://github.com/CarlDuFromChina/blog">blog</a></p>
        <p>我的邮箱：<a href="mailto:18556906294@163.com">18556906294@163.com</a></p>
        <p>版本号：2.3.2</p>
        <p><a href="http://www.miitbeian.gov.cn">苏ICP备2020054977号</a></p>
      </div>
    </a-layout-sider>
  </a-layout>
</template>

<script>
import recommandList from './recommandList';
import links from './links';
import idea from './idea';
import blogList from './blogList';
import seriesList from './seriesList.vue';

export default {
  name: 'home',
  components: { recommandList, links, idea, blogList, seriesList },
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

.more {
  border-radius: 2px;
  margin-bottom: 1.3rem;
  line-height: 1;
  font-size: 14px;
  color: #9aa3ab;
}
</style>
