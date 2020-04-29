<template>
  <div class="blog">
    <div class="__header">
      <el-button icon="el-icon-back" @click="$router.back()">返回</el-button>
      <el-button icon="el-icon-check" type="primary" @click="submit">提交</el-button>
    </div>
    <div class="__body">
      <div class="body__wrapper">
        <div class="markdown">
          <div class="container">
            <mavon-editor v-model="data.content" ref="md" @imgAdd="imgAdd" @change="change" style="min-height: 600px;height:100%" />
          </div>
        </div>
      </div>
    </div>
    <el-dialog title="发布文章" :visible.sync="editVisible">
      <el-form ref="form" :model="data" label-width="50px">
        <el-row>
          <el-col>
            <el-form-item label="标题">
              <el-input type="text" v-model="data.title"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col>
            <el-form-item label="分类">
              <el-select v-model="data.blog_type" @change="handleTypeChange">
                <el-option :label="item.label" :value="item.value" v-for="(item, index) in blogType" :key="index"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col>
            <el-form-item label="标签">
              <el-tag type="success">标签二</el-tag>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="editVisible = false">取 消</el-button>
        <el-button type="primary" @click="save">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'blogEdit',
  mixins: [edit],
  components: { mavonEditor },
  data() {
    return {
      html: '',
      configs: {},
      editVisible: false,
      controllerName: 'blog'
    };
  },
  computed: {
    blogType() {
      return [
        {
          label: 'JavaScript',
          value: 'js'
        },
        {
          label: 'C#',
          value: 'csharp'
        }
      ];
    }
  },
  created() {
    if (this.$route.params.Id) {
      this.Id = this.$route.params.Id;
      this.loadData();
    }
  },
  methods: {
    // 将图片上传到服务器，返回地址替换到md中
    imgAdd(pos, file) {
      sp.post('api/DataService/UploadAttachment?id=', file)
        .then(res => {
          this.$refs.md.$img2Url(pos, res.data);
        })
        .catch(err => {
          this.$message.error(err);
        });
    },
    // 所有操作都会被解析重新渲染
    change(value, render) {
      this.html = render; // render 为 markdown 解析后的结果[html]
    },
    handleTypeChange(value) {
      const arrs = this.blogType.filter(item => item.value === value);
      if (arrs.length > 0) {
        this.data.blog_typeName = arrs[0].label;
      }
    },
    // 提交
    submit() {
      this.editVisible = true;
    },
    save() {
      this.editVisible = false;
      this.data.Id = sp.isNullOrEmpty(this.data.Id) ? sp.newUUID() : this.data.Id;
      sp.post(`api/blog/${this.pageState === 'create' ? 'CreateData' : 'UpdateData'}`, this.data)
        .then(() => {
          this.$message.success('发布成功！');
          this.$router.back();
        })
        .catch(error => this.$message.error(error));
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
    .body__wrapper {
      width: calc(100% - 40px);
      height: calc(100% - 120px);
      padding: 0px;
      margin: 0px 20px;
      background-color: #fff;
      .form {
        height: 100px;
      }
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
