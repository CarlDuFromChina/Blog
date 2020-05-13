<template>
  <div class="sp-section">
    <h3 class="title">{{ title }}</h3>
    <el-divider></el-divider>
    <div class="content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: 'spSection',
  props: {
    title: {
      type: String,
      default: ''
    }
  },
  data() {
    return {};
  }
};
</script>

<style lang="less" scoped>
.sp-section {
  min-width: 300px;
  min-height: 300px;
  title {
    font-size: 18px;
  }
  content {
    min-height: 300px;
  }
}
</style>
