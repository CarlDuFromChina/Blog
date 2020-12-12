<template>
  <div class="blog blog__edit">
    <div class="blog-header">
      <a-button icon="rollback" @click="goBack">返回</a-button>
      <a-button icon="check" type="primary" @click="editVisible = true">提交</a-button>
      <a-button icon="sync" type="primary" @click="sync2Wechat()" :disabled="!Id">同步到微信</a-button>
      <a-popover v-if="showAutoSave">
        <template slot="content">
          <p>{{ saveStatus.text }}</p>
        </template>
        <a-button :icon="saveStatus.icon" type="dashed" @click="saveDraft" :style="{ color: saveStatus.color }"> </a-button>
      </a-popover>
    </div>
    <div class="blog-body">
      <div class="blog-bodywrapper">
        <div class="blog-bodywrapper-markdown">
          <mavon-editor v-model="data.content" ref="md" @imgAdd="imgAdd" @change="change" @save="editVisible = true" />
        </div>
      </div>
    </div>
    <a-modal title="发布文章" v-model="editVisible" @ok="editVisible = false">
      <a-form-model ref="form" :model="data">
        <a-row>
          <a-col>
            <a-form-model-item label="标题">
              <a-input v-model="data.title"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="分类">
              <a-select v-model="data.blog_type" @change="handleTypeChange">
                <a-select-option :value="item.Value" v-for="(item, index) in blogType" :key="index">{{ item.Name }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="系列">
              <a-switch v-model="isSeries"></a-switch>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="是否前台显示">
              <a-switch v-model="isShow"></a-switch>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="允许评论">
              <a-switch v-model="enableComment"></a-switch>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="标签">
              <sp-tag :tags="tags" @change="changeTags"></sp-tag>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="封面">
              <a-spin :spinning="loading">
                <a-upload
                  :action="baseUrl"
                  ref="files"
                  :file-list="fileList"
                  :beforeUpload="beforeUpload"
                  :remove="removeSurface"
                  list-type="picture"
                  :openFileDialogOnClick="false"
                >
                  <a-button type="primary" @click="openCloudUpload" icon="upload">上传</a-button>
                </a-upload>
              </a-spin>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
      <span slot="footer" class="dialog-footer">
        <a-button @click="editVisible = false">取 消</a-button>
        <a-button type="primary" @click="save" :loading="loading">确 定</a-button>
      </span>
    </a-modal>
    <cloud-upload ref="cloudUpload" @selected="selected"></cloud-upload>
  </div>
</template>

<script>
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';
import { edit } from 'sixpence.platform.pc.vue';
import draft from './draft';

export default {
  name: 'blogEdit',
  components: { mavonEditor },
  mixins: [edit, draft],
  data() {
    return {
      html: '',
      configs: {},
      editVisible: false,
      controllerName: 'blog',
      blogType: [],
      fileList: [],
      baseUrl: sp.getBaseUrl(),
      token: '',
      tags: [],
      is_series: false,
      is_show: 1,
      enable_comment: true
    };
  },
  created() {
    // 编辑博客
    if (this.$route.params.id) {
      this.Id = this.$route.params.id;
      this.loadData();
    } else {
      this.data.is_series = 0;
      this.data.disable_comment = 0;
    }
    // 获取博客类型选项集
    sp.get('api/classification/GetDataList').then(resp => {
      this.blogType = resp.map(item => {
        return {
          Name: item.name,
          Value: item.code
        };
      });
      if (this.$route.params.blogType) {
        this.data.blog_type = this.$route.params.blogType;
        this.handleTypeChange(this.data.blog_type);
      }
    });
    // 获取token和url
    this.token = window.localStorage.getItem('Token');
  },
  computed: {
    isSeries: {
      get() {
        return this.is_series || !!this.data.is_series;
      },
      set(val) {
        this.data.is_series = val ? 1 : 0;
        this.is_series = val;
      }
    },
    isShow: {
      get() {
        return !!this.data.is_show;
      },
      set(val) {
        this.data.is_show = val ? 1 : 0;
      }
    },
    enableComment: {
      get() {
        return this.enable_comment || !this.data.disable_comment;
      },
      set(val) {
        this.data.disable_comment = val ? 0 : 1;
        this.enable_comment = val;
      }
    },
    // 请求头
    headers() {
      return {
        Authorization: 'BasicAuth ' + this.token
      };
    }
  },
  methods: {
    selected(item) {
      this.data.surfaceid = item.surfaceid;
      this.data.surface_url = item.surface_url;
      this.data.big_surfaceid = item.big_surfaceid;
      this.data.big_surface_url = item.big_surface_url;
      this.fileList = [
        {
          uid: '0',
          status: 'done',
          name: 'surface',
          url: `${this.baseUrl}${this.data.surface_url}`
        }
      ];
    },
    openCloudUpload() {
      this.$refs.cloudUpload.visible = true;
    },
    loadComplete() {
      if (!sp.isNullOrEmpty(this.data.surfaceid)) {
        this.fileList = [
          {
            uid: '0',
            status: 'done',
            name: 'surface',
            url: `${this.baseUrl}${this.data.surface_url}`
          }
        ];
      }
      if (!sp.isNullOrEmpty(this.data.tags)) {
        this.data.tags = JSON.parse(this.data.tags);
        this.tags = this.data.tags;
      }
      this.openWatch();
    },
    beforeUpload(file, fileList) {
      return false;
    },
    // 移除封页
    removeSurface() {
      this.data.surfaceid = '';
      this.data.surface_url = '';
      this.data.big_surfaceid = '';
      this.data.big_surface_url = '';
      this.fileList = [];
      this.$message.success('删除成功！');
    },
    // 将图片上传到服务器，返回地址替换到md中
    imgAdd(pos, file) {
      const url = '/api/DataService/UploadImage?fileType=blog_content&objectId=' + (this.Id || this.draft.blogId);
      const formData = new FormData();
      formData.append('file', file);
      sp.post(url, formData, this.headers).then(resp => {
        let oStr = `(${pos})`;
        let nStr = `(${this.baseUrl}/${resp.downloadUrl})`;
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
      const item = this.blogType.find(item => item.Value === value);
      if (item) {
        this.data.blog_typeName = item.Name;
      }
    },
    // 保存博客
    save() {
      this.editVisible = false;
      this.data.Id = sp.isNullOrEmpty(this.data.Id) ? this.draft.blogId : this.data.Id;
      if (this.tags) {
        this.data.tags = this.tags;
      }
      sp.post(`api/blog/${this.pageState === 'create' ? 'CreateData' : 'UpdateData'}`, this.data)
        .then(() => {
          this.$message.success('发布成功！');
          this.$router.back();
        })
        .catch(error => this.$message.error(error));
    },
    // 返回上页
    goBack() {
      if (this.preBack && typeof this.preBack === 'function') {
        this.preBack(() => this.saveData());
      } else {
        this.$router.back();
      }
    },
    changeTags(val) {
      this.tags = val;
    },
    sync2Wechat() {
      if (sp.isNullOrEmpty(this.Id)) {
        this.$message.error('请先保存博客，再同步到微信图文素材');
        return;
      }
      this.$confirm({
        title: '微信同步',
        content: '是否同步到微信图文素材库?',
        okText: '确定',
        cancelText: '取消',
        onOk: () => {
          sp.post(`api/Blog/SyncToWeChat?id=${this.data.Id}`, `=${encodeURIComponent(this.html)}`);
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .v-note-wrapper {
  z-index: 100;
}
</style>
