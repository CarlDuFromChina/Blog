<template>
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
      <sp-menu-item style="float:right" v-show="isLoggedIn" disableHover @click="goMessageIndex">
        <a-badge :count="messageCount.total">
          <sp-icon name="sp-blog-notice" size="24"></sp-icon>
        </a-badge>
      </sp-menu-item>
    </template>
  </sp-menu>
</template>

<script>
import { clearAuth } from '../../lib/login';

export default {
  data() {
    return {
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
      ],
      showAdmin: false,
      messageCount: 0
    };
  },
  created() {
    if (this.isLoggedIn) {
      sp.get('api/MessageRemind/GetUnReadMessageCount').then(resp => {
        this.messageCount = resp.total;
      });
      sp.get('api/System/GetShowAdmin').then(resp => {
        this.showAdmin = resp;
      });
    }
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    }
  },
  methods: {
    getAvatar() {
      return `${sp.getServerUrl()}api/System/GetAvatar?id=${sp.getUserId()}`;
    },
    // 菜单切换
    menuChange() {
      document.getElementById('container').scrollIntoView();
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
    goMessageIndex() {
      this.$router.push({ name: 'messageRemind' });
    }
  }
};
</script>

<style></style>
