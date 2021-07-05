<template>
  <a-spin :spinning="loading" :delay="500">
    <div class="sp-section">
      <div class="sp-section-header">
        <h3 class="title">{{ title }}</h3>
        <h3 v-if="showMore" @click="more">更多...</h3>
      </div>
      <div class="content">
        <slot></slot>
      </div>
    </div>
  </a-spin>
</template>

<script>
export default {
  name: 'spSection',
  props: {
    title: {
      type: String,
      default: ''
    },
    // 展示更多
    showMore: {
      type: Boolean,
      default: false
    },
    loading: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {};
  },
  methods: {
    more() {
      this.$emit('more');
    }
  }
};
</script>

<style lang="less" scoped>
.sp-section {
  min-width: 300px;
  min-height: 300px;
  .sp-section-header {
    display: flex;
    line-height: 25px;
    justify-content: space-between;
    border-bottom: 1px solid #dcdfe6;
    title {
      font-size: 18px;
    }
  }
  content {
    min-height: 300px;
  }
}
</style>
