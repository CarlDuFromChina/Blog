<template>
  <div style="background:#edeef2">
    <!-- 菜单 -->
    <sp-menu>
      <template slot="menus">
        <li class="el-menu-item" style="float:right;" @click="login">登录</li>
        <li class="el-menu-item" style="float:right;" @click="register">注册</li>
      </template>
      <div class="header-img">
        <div class="scene">
          <span>Hi, Karl</span>
        </div>
        <div class="information">
          <img src="../../assets/images/smartboy.jpg" alt="" style="width:100px;height:100px;border-radius:50px" />
          <h2>Any advanced technology is no different from magic.</h2>
        </div>
      </div>
    </sp-menu>
    <!-- 菜单 -->
    <div class="container">
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
        <el-aside width="30%" style="padding-left:20px;">
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
    </div>
    <div class="footer">
      <div class="footer-wrapper">
        <p>没事，我还顶得住</p>
        <p>Made By Karl Du</p>
      </div>
    </div>
  </div>
</template>

<script>
import spMenu from './spMenu';
import spSection from './spSection';
import recommendInfo from '../admin/recommendInfo/recommendInfoList';
import idea from '../admin/idea/ideaCard';

export default {
  name: 'home',
  components: { spMenu, spSection, recommendInfo, idea },
  data() {
    return {
      activeIndex: '1',
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
    login() {
      this.$router.push({
        name: 'login'
      });
    },
    register() {
      this.$message.error('暂未开放注册');
    },
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
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

<style lang="less" scoped>
.header-img {
  height: 650px;
  position: relative;
  width: 100%;
  background-size: cover;
  background-position: center 50%;
  background-repeat: no-repeat;
  margin-bottom: 150px;
  background-image: url('http://mangoya.cn/upload/theme/yourName/20fbf79218e1c60c9bc96c1442f916fa.jpg');
  .scene {
    width: 100%;
    text-align: center;
    font-size: 100px;
    font-weight: 200;
    color: #fff;
    position: absolute;
    left: 0;
    top: 160px;
    font-family: 'Sigmar One', Arial;
    text-shadow: 0 2px 2px #47456d;
    > span {
      text-shadow: 1px 1px 0 #ff3f1a, -1px -1px 0 #00a7e0;
    }
  }
  .information {
    text-align: center;
    width: 70%;
    margin: auto;
    position: relative;
    top: 480px;
    padding: 40px 0;
    font-size: 16px;
    opacity: 0.98;
    background: rgba(230, 244, 249, 0.8);
    border-radius: 5px;
    z-index: 1;
    animation: b 1s ease-out;
    -webkit-animation: b 1s ease-out;
    > h2 {
      margin-top: 20px;
      font-size: 18px;
      font-weight: 700;
    }
  }
}
.container {
  max-width: 80%;
  min-height: 800px;
  margin: 0 auto;
  padding: 0 10px 50px 10px;
}
.footer {
  color: #888;
  font-size: 12px;
  line-height: 1.5;
  text-align: center;
  width: 100%;
  position: absolute;
  min-height: 50px;
  .footer-wrapper {
    width: 100%;
    background: #232323;
    padding: 15px 10px 10px 10px;
    box-sizing: border-box;
    width: 100% !important;
  }
}
</style>
