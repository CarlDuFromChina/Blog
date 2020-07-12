<template>
  <admin ref="admin">
    <el-dropdown @command="handleClick" class="header-dropdown" slot="custom-area">
      <el-button @click="writeBlog" type="primary">写博客<i class="el-icon-arrow-down el-icon--right"></i> </el-button>
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item :command="writeIdea">想法</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </admin>
</template>

<script>
import { admin } from 'sixpence.platform.pc.vue';

export default {
  name: 'myAdmin',
  components: { admin },
  data() {
    return {};
  },
  methods: {
    handleClick() {
      this.$refs.admin.handleClick();
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

<style></style>
