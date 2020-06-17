<template>
  <sp-markdown-edit :imgAdd="imgAdd" v-model="data.content" @change="change" ref="md">
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
        <el-row>
          <el-col>
            <el-form-item label="图片">
              <el-upload
                :action="baseUrl"
                :limit="1"
                ref="files"
                :http-request="uploadImage"
                :file-list="fileList"
                :on-remove="removeImage"
                list-type="picture"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
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
      blogType: [],
      fileList: [],
      baseUrl: '',
      token: ''
    };
  },
  created() {
    if (this.$route.params.id) {
      this.Id = this.$route.params.id;
      this.loadData();
    }
    sp.get('api/SysParamGroup/GetParams?code=blog_type').then(resp => {
      this.blogType = resp;
      if (this.$route.params.blogType) {
        this.data.blog_type = this.$route.params.blogType;
        this.handleTypeChange(this.data.blog_type);
      }
    });
    this.baseUrl = window.localStorage.getItem('baseUrl');
    this.token = window.localStorage.getItem('Token');
  },
  computed: {
    headers() {
      return {
        Authorization: 'BasicAuth ' + this.token
      };
    }
  },
  methods: {
    loadComplete() {
      if (!sp.isNullOrEmpty(this.data.imageId)) {
        const arr = (this.data.imageSrc || '').split('/');
        const name = arr.length > 0 ? arr[arr.length - 1] : '';
        this.fileList = [
          {
            name: name,
            url: this.baseUrl + '/' + this.data.imageSrc
          }
        ];
      }
    },
    uploadImage(param) {
      const url = '/api/DataService/UploadImage?fileType=blog_surface';
      const formData = new FormData();
      formData.append('file', param.file);
      sp.post(url, formData, this.headers).then(resp => (this.data.imageId = resp.id));
    },
    removeImage() {
      if (sp.isNullOrEmpty(this.data.imageId)) {
        return Promise.resolve(true);
      }
      return sp.post('api/Blog/DeleteSurface', `=${this.data.imageId}`).then(() => {
        this.$message.success('删除成功！');
      });
    },
    // 将图片上传到服务器，返回地址替换到md中
    imgAdd(pos, file) {
      const url = '/api/DataService/UploadImage?fileType=blog_content&objectId=' + this.Id;
      const formData = new FormData();
      formData.append('file', file);
      sp.post(url, formData, this.headers).then(resp => {
        let oStr = `(${pos})`;
        let nStr = `(${this.baseUrl}/${resp.path})`;
        let index = this.data.content.indexOf(oStr);
        let str = this.data.content.replace(oStr, '');
        let insertStr = (soure, start, newStr) => {
          return soure.slice(0, start) + newStr + soure.slice(start);
        };
        this.data.content = insertStr(str, index, nStr);
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
