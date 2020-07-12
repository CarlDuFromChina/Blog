<template>
  <admin ref="admin">
    <el-dropdown @command="handleClick" class="header-dropdown" slot="custom-area">
      <el-button @click="writeBlog" type="primary">写博客<i class="el-icon-arrow-down el-icon--right"></i> </el-button>
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item :command="writeIdea">想法</el-dropdown-item>
        <el-dropdown-item v-for="item in drafts" :command="item.click" :key="item.id">{{ item.title }}</el-dropdown-item>
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
    return {
      drafts: []
    };
  },
  created() {
    sp.get('api/Draft/GetDrafts').then(resp => {
      this.drafts = resp.map((item, index) => ({
        title: `草稿${index + 1}`,
        id: item.blogId,
        click: () => {
          this.$router.push({
            name: 'blogEdit',
            params: {
              draftId: item.blogId
            }
          });
        }
      }));
    });
  },
  methods: {
    handleClick(cmd) {
      if (cmd && typeof cmd === 'function') {
        cmd();
      }
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
