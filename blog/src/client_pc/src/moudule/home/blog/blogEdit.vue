<template>
  <div class="blog">
    <div class="__header">
      <el-button icon="el-icon-back" @click="$router.back()">返回</el-button>
      <el-button icon="el-icon-check" type="primary" @click="submit">提交</el-button>
    </div>
    <div class="__body">
      <div class="__body__wrapper">
        <div class="markdown">
          <div class="container">
            <mavon-editor v-model="content" ref="md" @imgAdd="imgAdd" @change="change" style="min-height: 600px;height:100%" />
          </div>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';

export default {
  name: 'blogEdit',
  components: { mavonEditor },
  data() {
    return {
      content: '',
      html: '',
      configs: {}
    };
  },
  methods: {
    // 将图片上传到服务器，返回地址替换到md中
    imgAdd(pos, file) {
      sp.post('api/DataService/UploadAttachment?id=', file).then(res => {
        this.$refs.md.$img2Url(pos, res.data);
      }).catch(err => {
        this.$message.error(err);
      });
    },
    // 所有操作都会被解析重新渲染
    change(value, render) {
      // render 为 markdown 解析后的结果[html]
      this.html = render;
    },
    // 提交
    submit() {
      const data = {
        id: sp.newUUID(),
        content: this.content
      };
      sp.post('api/blog/SaveData', data);
      this.$message.success('提交成功，已打印至控制台！');
    }
  }
};
</script>

<style lang="less" scoped>
.blog {
  height: 100%;
  .__header {
    width: 100%;
    height: 60px;
    display: inline-block;
    line-height: 60px;
    padding-left: 20px;
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
  }
  .__body {
    height: calc(100% - 60px);
    background-color: #e9ecef;
    color: #212529;
    padding-top: 24px;
    .__body__wrapper {
      width: calc(100% - 40px);
      height: calc(100% - 120px);
      padding: 0px;
      margin: 0px 20px;
      background-color: #fff;
      .markdown {
        height: 100%;
        .container {
          height: 100%;
        }
      }
    }
  }
}
</style>
