<template>
  <admin ref="admin">
    <a-button class="button-back" icon="rollback" @click="goIndex">首页</a-button>
    <a-button type="primary" icon="edit" @click="writeBlog" style="margin-right:20px;">写博客</a-button>
    <!-- 悬浮菜单 -->
    <div class="hover-menu">
      <a-dropdown>
        <a-menu slot="overlay">
          <a-menu-item key="1" @click="writeIdea">创建想法</a-menu-item>
        </a-menu>
        <a-button type="primary" size="large" icon="edit" shape="circle" @click="writeBlog"></a-button>
      </a-dropdown>
    </div>
    <!-- 悬浮菜单 -->
  </admin>
</template>

<script>
import { admin } from 'vue-pc';

export default {
  name: 'myAdmin',
  components: { admin },
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
        name: 'blogEdit'
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
