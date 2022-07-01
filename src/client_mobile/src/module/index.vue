<template>
  <sp-view>
    <sp-content>
      <keep-alive>
        <router-view :key="$route.fullPath" />
      </keep-alive>
    </sp-content>
    <sp-footer>
      <mt-tabbar v-model="selected">
        <mt-tab-item id="0">
          <sp-icon slot="icon" size="20" name="sp-blog-blog"></sp-icon>
          博客
        </mt-tab-item>
        <mt-tab-item id="1">
          <sp-icon slot="icon" size="20" name="sp-blog-reading"></sp-icon>
          读书笔记
        </mt-tab-item>
        <mt-tab-item id="2">
          <sp-icon slot="icon" size="20" name="sp-blog-idea"></sp-icon>
          想法
        </mt-tab-item>
      </mt-tabbar>
    </sp-footer>
  </sp-view>
</template>

<script>
export default {
  name: 'index',
  data() {
    return {
      selected: '0',
      routeNameList: ['post-list', 'reading-list', 'idea-list']
    };
  },
  created() {
    this.selected = this.routeNameList.findIndex(item => item === this.$route.name).toString();
  },
  watch: {
    selected() {
      this.$router.push({ name: this.routeNameList[this.selected] });
    }
  }
};
</script>

<style lang="less" scoped>
// 绝对定位导致无法计算footer高度
/deep/ .mint-tabbar {
  position: static;
}
</style>
