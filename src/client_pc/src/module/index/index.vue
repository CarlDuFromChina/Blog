<template>
  <div class="index">
    <!-- 菜单 -->
    <sp-menu :menus="menus" @menu-change="menuChange">
      <template slot="menus">
        <sp-menu-item style="float:right;" v-show="!isLoggedIn" disableHover>
          <a-button icon="login" type="primary" @click="login">登录</a-button>
        </sp-menu-item>
        <sp-menu-item style="float:right;" v-show="isLoggedIn" disableHover>
          <a-dropdown>
            <a-menu slot="overlay">
              <a-menu-item key="2" @click="goBg" v-show="showAdmin"><a-icon type="appstore" />后台</a-menu-item>
              <a-menu-item key="3" @click="() => (userInfoEditVisible = true)"><a-icon type="setting" />设置</a-menu-item>
              <a-menu-item key="4" @click="logout"><a-icon type="logout" />注销</a-menu-item>
            </a-menu>
            <a-avatar :src="getAvatar()" shape="circle" style="cursor: pointer" />
          </a-dropdown>
        </sp-menu-item>
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
    <a-modal v-model="userInfoEditVisible" title="编辑" @ok="saveUserInfo" width="60%" okText="确认" cancelText="取消">
      <user-info-edit ref="userInfoEdit" :related-attr="getUserParam()" @close="userInfoEditVisible = false"></user-info-edit>
    </a-modal>
  </div>
</template>

<script>
import infiniteScroll from 'vue-infinite-scroll';
import { clearAuth } from '../../lib/login';
import userInfoEdit from '../admin/core/userInfo/userInfoEdit.vue';

export default {
  name: 'index',
  directives: { infiniteScroll },
  components: { userInfoEdit },
  data() {
    return {
      bottom: 100,
      scrollTop: 0,
      btnFlag: false,
      activeIndex: '1',
      userInfoEditVisible: false,
      messageCount: 0,
      showAdmin: false,
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
  created() {
    if (this.isLoggedIn) {
      sp.get('api/System/GetShowAdmin').then(resp => {
        this.showAdmin = resp;
      });
    }
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
    getAvatar() {
      return `${sp.getServerUrl()}api/System/GetAvatar?id=${sp.getUserId()}`;
    },
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
    },
    saveUserInfo() {
      this.$refs.userInfoEdit.saveData();
    },
    getUserParam() {
      return {
        id: sp.getUserId()
      };
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
