<template>
  <el-container>
    <!-- 博客 -->
    <el-aside width="60%" v-loading="loading">
      <h3>最新博客</h3>
      <el-divider></el-divider>
      <sp-blog-card
        :fetch="fetchData"
        element-loading-text="拼命加载中"
        element-loading-spinner="el-icon-loading"
        @loading="loading = true"
        @loading-close="loading = false"
        readonly
      ></sp-blog-card>
    </el-aside>
    <!-- 博客 -->
    <el-aside width="30%" style="padding-left:40px;">
      <!-- 推荐 -->
      <sp-section title="推荐书籍">
        <el-carousel height="300px">
          <el-carousel-item v-for="(item, index) in recommendPic" :key="index">
            <el-image :src="item.src"></el-image>
          </el-carousel-item>
        </el-carousel>
      </sp-section>
      <!-- 推荐 -->
      <!-- 推荐博客 -->
      <sp-section title="推荐博客">
        <recommend-info minHeight="300px" type="readonly" :pageSize="5"></recommend-info>
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
import recommendInfo from '../admin/recommendInfo/recommendInfoList';
import idea from '../admin/idea/ideaCard';

export default {
  name: 'home',
  components: { recommendInfo, idea },
  data() {
    return {
      loading: 'false',
      recommendPic: []
    };
  },
  created() {
    this.getRecommendPictures().then(resp => {
      this.recommendPic = resp.DataList.map(item => ({ src: item.url }));
    });
  },
  methods: {
    // 获取推荐图片
    getRecommendPictures() {
      const searchList = [
        {
          Name: 'recommend_type',
          Value: 'picture'
        }
      ];
      return sp.get(`api/recommendInfo/GetDataList?orderBy=createdon&pageSize=10&pageIndex=1&searchList=${JSON.stringify(searchList)}`);
    },
    fetchData() {
      return sp
        .get(`api/blog2/GetDataList?orderBy=createdon&pageSize=10&pageIndex=1&searchList=`)
        .then(resp => resp)
        .catch(() => this.$message.error('加载出错了'));
    }
  }
};
</script>
