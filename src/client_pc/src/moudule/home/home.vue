<template>
  <el-container class="container">
    <el-header class="header">
      <el-dropdown>
        <i
          class="el-icon-setting"
          style="margin-right: 15px"
        ></i>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>查看</el-dropdown-item>
          <el-dropdown-item>新增</el-dropdown-item>
          <el-dropdown-item>删除</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <span>{{ userInfo.name }}</span>
    </el-header>
    <el-container class="container__wrapper">
      <el-aside
        width="200px"
        class="menu"
      >
        <el-menu
          :default-active="$route.path"
          :default-openeds="defaultOpenedsArray"
          @open="handleOpen"
          router
        >
          <el-submenu
            v-for="(item, index) in menus"
            :key="index"
            :index="`${index}`"
          >
            <template slot="title">
              <i class="el-icon-menu"></i>{{ item.title }}
            </template>
            <el-menu-item-group
              v-for="(item2, index2) in item.subMenu"
              :key="index2"
              :title="item2.title"
            >
              <el-menu-item
                v-for="(item3, index3) in item2.menus"
                :index="`/${item.router}/${item3.router}`"
                :key="index3"
              >
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
      this.menus = [
        {
          title: '编程语言',
          router: 'home',
          subMenu: [
            {
              title: '',
              menus: [
                { title: 'JavScript', router: 'test' },
                { title: 'C#', router: 'test2' },
                { title: '数据库' }
              ]
            }
          ]
        },
        {
          title: '闲谈',
          subMenu: [
            { title: '', menus: [{ title: '想法' }, { title: '程序人生' }] }
          ]
        },
        {
          title: '系统设置',
          subMenu: [
            {
              title: '数据',
              menus: [{ title: '菜单' }, { title: '实体' }]
            },
            {
              title: '业务',
              menus: [{ title: '作业管理' }, { title: '异步' }]
            }
          ]
        }
      ];
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
