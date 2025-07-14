<template>
  <sp-view>
    <sp-header :title="data.name"></sp-header>
    <sp-content>
      <div v-html="data.content" class="content"></div>
    </sp-content>
  </sp-view>
</template>

<script>
export default {
  name: 'reading-readonly',
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
      sp.get(`api/reading_note/${this.Id}`)
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
