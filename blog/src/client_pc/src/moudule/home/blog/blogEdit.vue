<template>
  <sp-markdown-edit :imgAdd="imgAdd" v-model="data.content" @change="change">
    <div class="blog-header" slot="header">
      <el-button icon="el-icon-back" @click="$router.back()">返回</el-button>
      <el-button icon="el-icon-check" type="primary" @click="submit">提交</el-button>
    </div>
    <el-dialog title="发布文章" :visible.sync="editVisible" slot="dialog">
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
                <el-option :label="item.Name" :value="item.Value" v-for="(item, index) in blogType" :key="index"></el-option>
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
  </sp-markdown-edit>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'blogEdit',
  mixins: [edit],
  data() {
    return {
      html: '',
      configs: {},
      editVisible: false,
      controllerName: 'blog',
      blogType: []
    };
  },
  // computed: {
  //   blogType() {
  //     return [
  //       {
  //         label: 'JavaScript',
  //         value: 'js'
  //       },
  //       {
  //         label: 'C#',
  //         value: 'csharp'
  //       }
  //     ];
  //   }
  // },
  created() {
    if (this.$route.params.Id) {
      this.Id = this.$route.params.Id;
      this.loadData();
      sp.get('api/SysParamGroup/GetParams?code=blog_type').then(resp => {
        this.blogType = resp;
      });
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
      const arrs = this.blogType.filter(item => item.Value === value);
      if (arrs.length > 0) {
        this.data.blog_typeName = arrs[0].Name;
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
