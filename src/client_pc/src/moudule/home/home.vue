<template>
  <el-container class="container">
    <el-header class="header">
      <el-dropdown>
        <i class="el-icon-setting" style="margin-right: 15px"></i>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>查看</el-dropdown-item>
          <el-dropdown-item>新增</el-dropdown-item>
          <el-dropdown-item>删除</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <span>{{ userInfo.name }}</span>
    </el-header>
    <el-container class="container__wrapper">
      <el-aside width="200px" class="menu">
        <el-menu :default-openeds="['1']">
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
                :index="`${index}-${(index2 + 1) * (index3 + 1)}`"
                :key="index3"
                >{{ item3.title }}</el-menu-item
              >
            </el-menu-item-group>
          </el-submenu>
        </el-menu>
      </el-aside>

      <el-container>
        <el-main>
          <el-table :data="tableData">
            <el-table-column
              prop="date"
              label="日期"
              width="140"
            ></el-table-column>
            <el-table-column
              prop="name"
              label="姓名"
              width="120"
            ></el-table-column>
            <el-table-column prop="address" label="地址"></el-table-column>
          </el-table>
        </el-main>
      </el-container>
    </el-container>
  </el-container>
</template>

<script>
const item = {
  date: '2016-05-02',
  name: '王小虎',
  address: '上海市普陀区金沙江路 1518 弄'
};
export default {
  name: 'home',
  data() {
    return {
      tableData: Array(4).fill(item),
      menus: []
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
          subMenu: [
            {
              title: '',
              menus: [
                { title: 'JavScript' },
                { title: 'C#' },
                { title: '数据库' }
              ]
            }
          ]
        },
        {
          title: '闲谈',
          subMenu: [
            { title: '', menus: [{ title: '想法' }, { title: '程序人生#' }] }
          ]
        },
        {
          title: '系统设置',
          subMenu: [
            {
              title: '数据',
              menus: [{ title: '想法' }, { title: '程序人生#' }]
            },
            {
              title: '数据',
              menus: [{ title: '想法' }, { title: '程序人生#' }]
            }
          ]
        }
      ];
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
