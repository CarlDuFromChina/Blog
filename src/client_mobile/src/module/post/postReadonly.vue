<template>
  <sp-view>
    <sp-header :title="data.title"></sp-header>
    <sp-content>
      <div v-html="content" class="content markdown-body" v-highlight></div>
    </sp-content>
  </sp-view>
</template>

<script>
import marked from 'marked';

export default {
  name: 'post',
  data() {
    return {
      data: {},
      content: '',
      Id: this.$route.params.id || ''
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.$indicator.open('加载中...');
      sp.get(`api/post/${this.Id}`)
        .then(resp => {
          this.data = resp;
          this.content = marked(resp.content, {
            sanitize: true
          });
        })
        .finally(() => {
          setTimeout(() => {
            this.$indicator.close();
          }, 500);
        });
    }
  }
};
</script>

<style lang="less" scoped>
.content {
  text-align: left;
  padding: 8px;
}

h2 {
  font-size: 16px;
}
</style>
