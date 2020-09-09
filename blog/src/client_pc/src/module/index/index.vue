<template>
  <div class="index" v-infinite-scroll="loadMore" :infinite-scroll-distance="10">
    <!-- 菜单 -->
    <sp-menu :menus="menus" @menu-change="menuChange">
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
    <div id="container" class="container">
      <router-view></router-view>
    </div>
    <div class="footer">
      <div class="footer-wrapper">
        <p>没事，我还顶得住</p>
        <p>Made By Karl Du</p>
      </div>
    </div>
    <transition name="hehe">
      <div class="ant-back-top" v-if="btnFlag">
        <div class="ant-back-top-content" @click="backTop">
          <div class="ant-back-top-icon"></div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import infiniteScroll from 'vue-infinite-scroll';

export default {
  name: 'index',
  directives: { infiniteScroll },
  data() {
    return {
      bottom: 100,
      scrollTop: 0,
      btnFlag: false,
      activeIndex: '1',
      menus: [
        {
          name: '首页',
          click: () => {
            this.$router.push({ name: 'home' });
          }
        },
        {
          name: '友人帐',
          click: () => {
            this.$router.push({ name: 'friends' });
          }
        },
        {
          name: '关于',
          click: () => {
            this.$router.push({ name: 'aboutme' });
          }
        }
      ]
    };
  },
  mounted() {
    window.addEventListener('scroll', this.scrollToTop, true);
  },
  destroyed() {
    window.removeEventListener('scroll', this.scrollToTop, true);
  },
  methods: {
    // 点击图片回到顶部方法，加计时器是为了过渡顺滑
    backTop() {
      let timer = setInterval(() => {
        let ispeed = Math.floor(-this.scrollTop / 5);
        document.querySelector('.index').scrollTop = document.documentElement.scrollTop = document.body.scrollTop = this.scrollTop + ispeed;
        if (this.scrollTop === 0) {
          clearInterval(timer);
        }
      }, 16);
    },
    // 为了计算距离顶部的高度，当高度大于60显示回顶部图标，小于60则隐藏
    scrollToTop() {
      let scrollTop =
        window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || document.querySelector('.index').scrollTop;
      this.scrollTop = scrollTop;
      if (this.scrollTop > 300) {
        this.btnFlag = true;
      } else {
        this.btnFlag = false;
      }
    },
    // 跳转登录页
    login() {
      this.$router.push({
        name: 'login'
      });
    },
    register() {
      this.$message.error('暂未开放注册');
    },
    // 菜单切换
    menuChange() {
      document.getElementById('container').scrollIntoView();
    },
    loadMore() {
      this.$bus.$emit('load-more');
    }
  }
};
</script>

<style lang="less" scoped>
.hehe-enter,
.hehe-leave-to {
  opacity: 0;
}
.hehe-enter-to,
.hehe-leave {
  opacity: 1;
}
.hehe-enter-active,
.hehe-leave-active {
  transition: all 1s;
}
.index {
  background: #edeef2;
  height: 100%;
  overflow-y: scroll;
}
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
  margin: 0 auto;
  padding: 0 10px 0px 10px;
}
.footer {
  color: #888;
  font-size: 12px;
  line-height: 1.5;
  text-align: center;
  width: 100%;
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
