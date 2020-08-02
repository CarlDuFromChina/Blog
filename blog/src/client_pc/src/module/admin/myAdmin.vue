<template>
  <admin ref="admin">
    <a-button type="primary" icon="edit" @click="writeBlog" style="margin-right:20px;">写博客</a-button>
  </admin>
</template>

<script>
import { admin } from 'sixpence.platform.pc.vue';

export default {
  name: 'myAdmin',
  components: { admin },
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
