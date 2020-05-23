<template>
  <el-container>
    <el-aside width="60%">
      <sp-section title="关于我"> </sp-section>
    </el-aside>
    <el-aside width="30%" style="padding-left:40px;">
      <sp-section title="名片">
        <el-image src="https://fuss10.elemecdn.com/a/3f/3302e58f9a181d2509f3dc0fa68b0jpeg.jpeg" lazy></el-image>
      </sp-section>
    </el-aside>
  </el-container>
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
