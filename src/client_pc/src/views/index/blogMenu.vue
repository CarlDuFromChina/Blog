<template>
  <sp-menu ref="menu" :menus="menus" @menu-change="menuChange">
    <template slot="menus">
      <sp-menu-item style="float:right;" v-show="!isLoggedIn" disableHover>
        <a-button icon="login" type="primary" @click="login" style="vertical-align: middle">登录</a-button>
      </sp-menu-item>
      <sp-menu-item style="float:right;" v-show="isLoggedIn" disableHover>
        <a-dropdown>
          <a-menu slot="overlay">
            <a-menu-item key="3" @click="openUserEdit"><a-icon type="setting" />个人中心</a-menu-item>
            <a-menu-item key="2" @click="goBg" v-show="showAdmin"><a-icon type="appstore" />后台管理</a-menu-item>
            <a-menu-item key="4" @click="logout"><a-icon type="logout" />退出</a-menu-item>
          </a-menu>
          <a-avatar :src="getAvatar()" shape="circle" style="cursor: pointer" />
        </a-dropdown>
      </sp-menu-item>
      <sp-menu-item style="float:right" v-show="isLoggedIn" disableHover @click="goMessageIndex">
        <a-badge :count="messageCount">
          <sp-icon name="sp-blog-notice" size="24"></sp-icon>
        </a-badge>
      </sp-menu-item>
      <sp-menu-item style="float:right;">
        <a-input-search
          ref="searchInput"
          @focus="searchFocus"
          @blur="searchBlur"
          v-model="searchValue"
          placeholder="输入博客名快速搜索"
          class="search"
          enter-button
          @search="onSearch"
        />
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
          name: '主页',
          route: 'home'
        },
        {
          name: '分类',
          route: 'categories'
        },
        {
          name: '读书笔记',
          route: 'reading'
        }
      ],
      showAdmin: false,
      messageCount: 0,
      searchValue: '',
      getAvatar: sp.getAvatar
    };
  },
  created() {
    if (this.isLoggedIn) {
      sp.get('api/message_remind/unread_message_count').then(resp => {
        this.messageCount = resp.total;
      });
      sp.get('api/system/is_show_admin').then(resp => {
        this.showAdmin = resp;
      });
    }
    if (this.$route.query.search) {
      this.searchValue = this.$route.query.search;
      this.onSearch(this.searchValue);
    }
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    }
  },
  methods: {
    openUserEdit() {
      this.$emit('openUserEdit');
    },
    // 菜单切换
    menuChange() {
      document.getElementById('index').scrollIntoView();
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
    },
    onSearch(value) {
      this.$router.push({ name: 'home', query: { search: value } }, () => this.$refs.menu.setCurrentMenu());
    },
    searchFocus() {
      this.$refs.searchInput.$el.style.width = '300px';
    },
    searchBlur() {
      this.$refs.searchInput.$el.style.width = '200px';
    }
  }
};
</script>

<style lang="less" scoped>
.search {
  width: 200px;
  vertical-align: middle;
  transition: width 0.3s;
}
</style>
