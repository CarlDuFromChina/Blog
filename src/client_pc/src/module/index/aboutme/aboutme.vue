<template>
  <a-layout>
    <a-layout-sider width="60%" theme="light">
      <sp-section title="关于我">
        <a-empty />
      </sp-section>
    </a-layout-sider>
    <a-layout-sider width="40%" style="padding-left:40px;" theme="light">
      <sp-section title="名片">
        <a-empty />
      </sp-section>
    </a-layout-sider>
  </a-layout>
</template>

<script>
export default {
  name: 'aboutme',
  data() {
    return {
      title: '关于我'
    };
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-layout-sider-light {
  background: transparent;
}
</style>
