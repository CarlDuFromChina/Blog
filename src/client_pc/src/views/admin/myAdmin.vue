<template>
  <admin ref="admin">
    <a-button class="button-back" icon="rollback" @click="goIndex">主页</a-button>
    <a-button type="primary" icon="edit" @click="writeBlog" style="margin-right:20px;" v-show="blogPrivilege.create">写博客</a-button>
    <!-- 悬浮菜单 -->
    <div class="hover-menu">
      <a-dropdown>
        <a-menu slot="overlay">
          <a-menu-item key="1" @click="writeIdea" v-show="ideaPrivilege.create">创建想法</a-menu-item>
        </a-menu>
        <a-button type="primary" size="large" icon="edit" shape="circle" @click="writeBlog" v-show="blogPrivilege.create"></a-button>
      </a-dropdown>
    </div>
    <!-- 悬浮菜单 -->
  </admin>
</template>

<script>
import { admin } from './core';

export default {
  name: 'myAdmin',
  components: { admin },
  data() {
    return {
      blogPrivilege: {},
      ideaPrivilege: {}
    };
  },
  created() {
    sp.get('api/post/privilege').then(resp => {
      this.blogPrivilege = resp;
    });
    sp.get('api/idea/privilege').then(resp => {
      this.ideaPrivilege = resp;
    });
  },
  methods: {
    handleClick(cmd) {
      if (cmd && typeof cmd === 'function') {
        cmd();
      }
    },
    goIndex() {
      this.$router.push({
        name: 'index'
      });
    },
    writeBlog() {
      this.$router.push({
        name: 'postEdit'
      });
    },
    writeIdea() {
      this.$router.push({
        name: 'idea'
      });
    }
  }
};
</script>

<style lang="less" scoped>
.hover-menu {
  position: absolute;
  bottom: 50px;
  right: 50px;
  z-index: 10;
}
</style>
