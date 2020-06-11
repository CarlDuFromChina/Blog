<template>
  <div class="notFound">
    <div class="notFound-card">
      <img src="//static.hdslb.com/error/very_sorry.png" style="flex-wrap: wrap" />
    </div>
  </div>
</template>

<script>
export default {
  name: 'notFound'
};
</script>

<style lang="less" scoped>
.notFound {
  font-size: 100px;
  display: flex;
  justify-content: center;
  background: #f4f5f7;
  height: 100%;
  .notFound-card {
    padding: 0 10px;
    border-radius: 10px;
    background-color: #fff;
    height: 300px;
    margin-top: 100px;
  }
}
</style>
