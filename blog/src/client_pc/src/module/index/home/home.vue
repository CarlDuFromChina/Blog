<template>
  <el-container>
    <!-- 博客 -->
    <el-aside width="60%">
      <h3>最新博客</h3>
      <el-divider></el-divider>
      <sp-blog-card ref="blog" :fetch="fetchData" readonly newTag v-infinite-scroll="loadMore"></sp-blog-card>
    </el-aside>
    <!-- 博客 -->
    <el-aside width="30%" style="padding-left:40px;overflow:hidden">
      <!-- 推荐 -->
      <sp-section title="推荐书籍">
        <recommand-pictures></recommand-pictures>
      </sp-section>
      <!-- 推荐 -->
      <!-- 推荐博客 -->
      <sp-section title="推荐博客">
        <recommand-list></recommand-list>
      </sp-section>
      <!-- 推荐博客 -->
      <!-- 想法 -->
      <sp-section title="想法">
        <idea :pageSize="5"></idea>
      </sp-section>
      <!-- 想法 -->
    </el-aside>
  </el-container>
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
