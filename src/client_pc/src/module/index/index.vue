<template>
  <div class="index">
    <!-- 菜单 -->
    <sp-menu :menus="menus" @menu-change="menuChange">
      <template slot="menus">
        <sp-menu-item @click="login" style="float:right;" v-show="!isLoggedIn">登录</sp-menu-item>
        <sp-menu-item @click="goBg" style="float:right;" v-show="isLoggedIn">后台</sp-menu-item>
        <sp-menu-item @click="logout" style="float:right;" v-show="isLoggedIn">注销</sp-menu-item>
      </template>
    </sp-menu>
    <!-- 菜单 -->
    <div id="container" v-infinite-scroll="loadMore" :infinite-scroll-distance="10">
      <div class="container">
        <keep-alive>
          <router-view></router-view>
        </keep-alive>
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
import { clearAuth } from '../../lib/login';
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
          name: '读书笔记',
          click: () => {
            this.$router.push({ name: 'readingNote' });
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
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    },
    userName() {
      return this.$store.getters.userName;
    }
  },
  methods: {
    // 点击图片回到顶部方法，加计时器是为了过渡顺滑
    backTop() {
      let timer = setInterval(() => {
        let ispeed = Math.floor(-this.scrollTop / 5);
        document.querySelector('.container').scrollTop = document.documentElement.scrollTop = document.body.scrollTop = this.scrollTop + ispeed;
        if (this.scrollTop === 0) {
          clearInterval(timer);
        }
      }, 16);
    },
    // 为了计算距离顶部的高度，当高度大于60显示回顶部图标，小于60则隐藏
    scrollToTop() {
      let scrollTop =
        window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || document.querySelector('.container').scrollTop;
      this.scrollTop = scrollTop;
      this.$bus.$emit('scroll', scrollTop);
      if (this.scrollTop > 300) {
        this.btnFlag = true;
      } else {
        this.btnFlag = false;
      }
    },
    // 跳转登录页
    login() {
      if (!this.isLoggedIn) {
        this.$router.push({ name: 'login' });
      }
    },
    logout() {
      clearAuth(this.$store);
      this.$message.success('注销成功');
    },
    goBg() {
      this.$router.push({ name: 'workplace' });
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
}
.container {
  max-width: 80%;
  margin: 10px auto 0;
  padding: 0 10px 0px 10px;
}
</style>
