<template>
  <a-layout>
    <!-- 博客 -->
    <a-layout-sider width="70%" theme="light">
      <blog-list ref="blogList"></blog-list>
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
        <p>作者：{{ website.author }}</p>
        <p>博客仓库地址：<a :href="config.repository.url">blog</a></p>
        <p>我的邮箱：<a :href="`mailto:${website.email}`">{{ website.email }}</a></p>
        <p>版本号：{{ config.version }}</p>
        <p><a href="https://beian.miit.gov.cn">{{ website.record_no }}</a></p>
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
import config from 'appconfig';

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
      },
      config,
      searchValue: '',
      website: this.$config.website
    };
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.searchValue = to.query.search;
      vm.$refs.blogList.onSearch(vm.searchValue);
    });
  },
  beforeRouteUpdate (to, from, next) {
    this.searchValue = to.query.search;
    this.$refs.blogList.onSearch(this.searchValue);
    next();
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
