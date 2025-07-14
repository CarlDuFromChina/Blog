<template>
  <a-layout>
    <!-- 博客 -->
    <a-layout-sider width="70%" theme="light">
      <blog-list ref="blogList"></blog-list>
    </a-layout-sider>
    <!-- 博客 -->
    <a-layout-sider width="30%" style="overflow:hidden" theme="light">
      <!-- 关于我 -->
      <me style="margin-bottom:20px;"></me>
      <!-- 关于我 -->

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
import { RecommandList, Links, Idea, BlogList, SeriesList, Me } from './components'
import packageConfig from '../../../../package.json';

export default {
  name: 'home',
  components: { RecommandList, Links, Idea, BlogList, SeriesList, Me },
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
      config: packageConfig,
      searchValue: '',
      website: {
        author: '',
        email: '',
        record_no: ''
      }
    };
  },
  created() {
    sp.get('api/sys_config/website_info').then(resp => {
      if (!sp.isNullOrEmpty(resp)) {
        try {
          var { author, email, record_no } = resp;
          this.website.author = author;
          this.website.email = email;
          this.website.record_no = record_no;
        } catch {
          this.$message.error('网站信息配置格式错误');          
        }
      }
    })
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
