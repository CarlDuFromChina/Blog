<template>
  <a-result status="404" title="404" sub-title="抱歉，您访问的页面不存在">
    <template #extra>
      <a-button type="primary" @click="goBack">
        返回
      </a-button>
    </template>
  </a-result>
</template>
<script>
export default {
  data() {
    return {};
  },
  methods: {
    goBack() {
      this.$router.history.go(-1);
    }
  }
};
</script>
