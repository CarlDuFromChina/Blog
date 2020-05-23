<template>
  <div style="background:#edeef2">
    <!-- 菜单 -->
    <sp-menu :menus="menus">
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
      <router-view></router-view>
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
export default {
  name: 'index',
  data() {
    return {
      activeIndex: '1',
      menus: [
        {
          name: '首页',
          click: () => {
            document.body.scrollIntoView();
            this.$router.push({ name: 'home' });
          }
        },
        { name: '归档', click: () => {} },
        { name: '友人帐', click: () => {} },
        {
          name: '关于',
          click: () => {
            document.body.scrollIntoView();
            this.$router.push({ name: 'aboutme' });
          }
        }
      ]
    };
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
