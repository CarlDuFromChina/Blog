<template>
  <a-layout class="layout-home">
    <a-layout-sider breakpoint="lg" collapsed-width="0">
      <a-menu theme="dark" mode="inline" :open-keys="openKeys" @openChange="onOpenChange">
        <a-sub-menu v-for="(item, index) in menus" :key="index">
          <span slot="title">
            <a-icon :type="item.icon" /><span>{{ item.title }}</span>
          </span>
          <a-menu-item v-for="item2 in item.subMenu[0].menus" :key="`/admin/${item2.router}`" @click="handleClick">
            {{ item2.title }}
          </a-menu-item>
        </a-sub-menu>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header :style="{ background: '#fff', padding: '0 20px 0 0', textAlign: 'right' }">
        <slot></slot>
        <a-dropdown>
          <a-menu slot="overlay">
            <a-menu-item key="1" @click="editPassword">修改密码</a-menu-item>
            <a-menu-item key="2" @click="logout">退出</a-menu-item>
          </a-menu>
          <a-button icon="user" shape="circle"></a-button>
        </a-dropdown>
      </a-layout-header>
      <a-layout-content :style="{ margin: '24px 16px 0' }">
        <div :style="{ background: '#fff', minHeight: '800px' }">
          <router-view :key="$route.path"></router-view>
        </div>
      </a-layout-content>
    </a-layout>
    <edit-password ref="pwd"></edit-password>
  </a-layout>
</template>

<script>
import editPassword from './editPasssword/editPassword';
import { clearAuth } from '@/lib/login';

export default {
  name: 'admin',
  components: { editPassword },
  data() {
    return {
      menus: [],
      defaultOpenedsArray: [],
      openKeys: [],
      data: {},
      rules: {
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
        password2: [{ required: true, message: '请再次输入密码', trigger: 'blur' }]
      },
      imageUrl: 'http://karldu.cn//api/SysFile/Download?objectId=13c5929e-cfca-406b-979b-d7a102a7ed10' // 头像
    };
  },
  created() {
    this.getMenu();
  },
  methods: {
    goHome() {
      this.$router.push({
        name: 'home'
      });
    },
    getMenu() {
      const searchList = [
        {
          Name: 'stateCode',
          Value: 1
        }
      ];
      sp.get(`api/sysmenu/GetDataList?searchList=${JSON.stringify(searchList)}`)
        .then(resp => {
          resp.forEach(e => {
            const menu = {
              title: e.name,
              router: e.router,
              subMenu: [{ title: '', menus: [] }],
              icon: e.icon
            };
            if (e.children && e.children.length > 0) {
              menu.subMenu[0].menus = e.children.map(item => ({
                title: item.name,
                router: item.router,
                icon: item.icon
              }));
            }
            this.menus.push(menu);
          });
        })
        .catch(resp => {
          this.$message.error(resp);
        });
    },
    handleClick({ item, key, keyPath }) {
      if (sp.isNullOrEmpty(keyPath[0])) {
        this.$message.error('发生错误，请检查菜单地址是否正确！');
        return;
      }
      if (keyPath[0] !== this.$route.path) {
        this.$router.push({ path: keyPath[0] });
      }
    },
    editPassword() {
      this.$refs.pwd.editVisible = true;
    },
    logout() {
      this.$message.success('退出成功');
      clearAuth(this.$store);
      this.$router.replace('/login');
    },
    onOpenChange(openKeys) {
      const latestOpenKey = openKeys.find(key => this.openKeys.indexOf(key) === -1);
      const rootSubmenuKeys = this.menus.map((item, index) => index);
      if (rootSubmenuKeys.indexOf(latestOpenKey) === -1) {
        this.openKeys = openKeys;
      } else {
        this.openKeys = latestOpenKey ? [latestOpenKey] : [];
      }
    }
  }
};
</script>

<style lang="less">
body,
html {
  margin: 0px;
  width: 100%;
  height: 100%;
}
</style>
<style lang="less" scoped>
.layout-home {
  height: 100%;
}
</style>
