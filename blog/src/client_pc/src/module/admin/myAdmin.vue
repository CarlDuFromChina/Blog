<template>
  <admin ref="admin"> </admin>
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
