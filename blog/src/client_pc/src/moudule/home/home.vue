<template>
  <el-container class="home">
    <el-container class="home_wrapper">
      <el-aside width="200px" class="menu">
        <el-menu
          :default-active="$route.path"
          :default-openeds="defaultOpenedsArray"
          @open="handleOpen"
          router
          background-color="#6750d7"
          text-color="#fff"
          active-text-color="#ffd04b"
        >
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
      <el-dialog title="修改密码" :visible.sync="editVisible">
        <el-form ref="form" :model="data" label-width="100px">
          <el-row>
            <el-col>
              <el-form-item label="密码">
                <el-input v-model="data.password" placeholder="请输入新密码" show-password></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col>
              <el-form-item label="确认密码">
                <el-input v-model="data.password2" placeholder="请再次输入密码" show-password></el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <span slot="footer" class="dialog-footer">
          <el-button @click="editVisible = false">取 消</el-button>
          <el-button type="primary" @click="savePassword">确 定</el-button>
        </span>
      </el-dialog>
      <el-container>
        <el-header class="home-header">
          <el-dropdown @command="handleCommand">
            <el-avatar src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png" class="home-header-avatar"></el-avatar>
            <el-dropdown-menu slot="dropdown" placement="bottom-end">
              <el-dropdown-item :command="editPassword">修改密码</el-dropdown-item>
              <el-dropdown-item :command="logout">退出</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </el-header>
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
      defaultOpenedsArray: [],
      editVisible: false,
      data: {}
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
      this.editVisible = true;
    },
    savePassword() {
      if (this.data.password !== this.data.password2) {
        this.$message.error('两次密码不一致');
      } else {
        sp.post('api/AuthUser/EditPassword', `=${this.data.password}`).then(() => {
          this.$message.success('修改密码成功');
          this.editVisible = false;
        });
      }
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
.home {
  height: 100%;
  .home-header {
    margin: 0 20px;
    text-align: right;
    font-size: 12px;
    background-color: #fff;
    border-bottom-left-radius: 10px;
    border-bottom-right-radius: 10px;
  }
  .home-header-avatar {
    margin-top: 10px;
  }
  .header {
    text-align: right;
    font-size: 12px;
    background-color: #fff;
    padding: 0 10px;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    border-bottom: 1px solid #ebeef5;
  }
  .home_wrapper {
    .menu {
      background-color: #6750d7;
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
<style lang="less" scoped>
.el-container {
  background-color: #edeef2;
}
</style>
