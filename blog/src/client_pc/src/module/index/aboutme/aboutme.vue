<template>
  <el-container>
    <el-aside width="60%">
      <sp-section title="关于我">
        <el-card>
          <div class="about">
            <p><span>我的名字是：</span>杜淼鑫</p>
            <p><span>职业：</span>程序员</p>
            <p><span>爱好：</span>写代码、读科幻小说、跑步</p>
            <p><span>座右铭：</span>既往不恋，当下不杂，未来不迎</p>
          </div>
        </el-card>
      </sp-section>
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

<style lang="less" scoped>
.about {
  text-align: center;
  min-height: 500px;
  > p {
    color: #606266;
    > span {
      color: #303133;
    }
  }
}
</style>
