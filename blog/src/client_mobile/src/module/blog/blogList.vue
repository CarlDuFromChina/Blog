<template>
  <sp-view>
    <sp-header title="首页"></sp-header>
    <sp-content>
      <mt-search v-model="searchValue" placeholder="输入博客名快速搜索"></mt-search>
      <ul v-infinite-scroll="loadMore" infinite-scroll-disabled="loading" infinite-scroll-distance="10">
        <li v-for="(item, index) in list" :key="index">{{ item }}</li>
      </ul>
    </sp-content>
  </sp-view>
</template>

<script>
export default {
  name: 'index',
  data() {
    return {
      searchValue: '',
      loading: false,
      list: []
    };
  },
  methods: {
    loadMore() {
      this.loading = true;
      let last = this.list[this.list.length - 1];
      for (let i = 1; i <= 50; i++) {
        this.list.push(last + i);
      }
      this.loading = false;
    },
    handleClick() {
      this.$message.alert('Hello World!', 'Hi~');
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .mint-search {
  height: auto;
}
</style>
