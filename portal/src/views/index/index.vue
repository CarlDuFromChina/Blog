<template>
  <div id="index" class="index">
    <blog-menu @openUserEdit="() => (this.userInfoEditVisible = true)"></blog-menu>
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

export default {
  name: 'index',
  directives: { infiniteScroll },
  data() {
    return {
      bottom: 100,
      scrollTop: 0,
      btnFlag: false,
      activeIndex: '1',
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
  max-width: 60%;
  margin: 10px auto 0;
  padding: 0 10px 0px 10px;
}
</style>
