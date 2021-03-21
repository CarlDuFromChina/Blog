<template>
  <sp-view>
    <sp-header :title="data.title"></sp-header>
    <sp-content>
      <vue-markdown :source="data.content" class="content markdown-body"></vue-markdown>
    </sp-content>
  </sp-view>
</template>

<script>
import VueMarkdown from 'vue-markdown';

export default {
  name: 'blog',
  components: { VueMarkdown },
  data() {
    return {
      data: {},
      Id: this.$route.params.id || ''
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.$indicator.open('加载中...');
      sp.get(`api/blog/GetData?id=${this.Id}`)
        .then(resp => {
          this.data = resp;
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
