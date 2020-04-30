<template>
  <el-container class="container">
    <el-header class="header">
      <el-dropdown @command="handleCommand">
        <el-avatar src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png" style="margin-top: 10px;"></el-avatar>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item :command="editPassword">修改密码</el-dropdown-item>
          <el-dropdown-item :command="logout">退出</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-header>
    <el-container class="container__wrapper">
      <el-aside width="200px" class="menu">
        <el-menu :default-active="$route.path" :default-openeds="defaultOpenedsArray" @open="handleOpen" router>
          <el-submenu v-for="(item, index) in menus" :key="index" :index="`${index}`">
            <template slot="title"> <i class="el-icon-menu"></i>{{ item.title }} </template>
            <el-menu-item-group v-for="(item2, index2) in item.subMenu" :key="index2" :title="item2.title">
              <el-menu-item v-for="(item3, index3) in item2.menus" :index="`/home/${item3.router}`" :key="index3">
                {{ item3.title }}
              </el-menu-item>
            </el-menu-item-group>
          </el-submenu>
        </el-menu>
      </el-aside>

      <el-container>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </el-container>
</template>

<script>
export default {
  name: 'home',
  data() {
    return {
      menus: [],
      defaultOpenedsArray: []
    };
  },
  created() {
    this.getUserInfo();
    this.getMenu();
  },
  methods: {
    getUserInfo() {
      this.userInfo = {
        date: '2016-05-02',
        name: 'Karl'
      };
    },
    getMenu() {
      const searchList = [
        {
          Name: 'stateCode',
          Value: 1
        }
      ];
      sp.get(`api/sysmenu/getdatalist?searchList=${JSON.stringify(searchList)}`).then(resp => {
        resp.forEach(e => {
          const menu = {
            title: e.name,
            router: e.router,
            subMenu: [{ title: '', menus: [] }]
          };
          if (e.ChildMenus && e.ChildMenus.length > 0) {
            menu.subMenu[0].menus = e.ChildMenus.map(item => ({
              title: item.name,
              router: item.router
            }));
          }
          this.menus.push(menu);
        });
      });
    },
    handleCommand(command) {
      if (command && typeof command === 'function') {
        command();
      }
    },
    editPassword() {
      this.$message.success('修改密码成功');
    },
    logout() {
      this.$message.success('退出成功');
      this.$router.replace('/login');
    },
    handleOpen(key) {
      this.defaultOpenedsArray.push(key);
    }
  }
};
</script>

<style lang="less">
.container {
  height: 100%;
  border: 1px solid #eee;
  .header {
    text-align: right;
    font-size: 12px;
  }
  .container__wrapper {
    .menu {
      background-color: rgb(238, 241, 246);
    }
  }
}
.el-header {
  background-color: #b3c0d1;
  color: #333;
  line-height: 60px;
}

.el-aside {
  color: #333;
}
body,
html {
  margin: 0px;
  width: 100%;
  height: 100%;
}
</style>
