<template>
  <div class="blog blog__edit">
    <div class="blog-header">
      <a-button icon="rollback" @click="goBack">返回</a-button>
      <a-button icon="check" type="primary" @click="editVisible = true">提交</a-button>
      <a-button icon="sync" type="primary" @click="syncBlog('wechat')" :disabled="!id">同步到微信</a-button>
      <a-button icon="sync" type="primary" @click="syncBlog('juejin')" :disabled="!id">同步到掘金</a-button>
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
      <a-form-model ref="form" :model="data" :rules="rules">
        <a-row>
          <a-col>
            <a-form-model-item label="标题" prop="title">
              <a-input v-model="data.title"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="分类" prop="post_type">
              <sp-select
                v-model="data.post_type"
                :options="selectDataList.category"
                @change="item => (data.post_type_name = item.name)"
              ></sp-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="文章类型" prop="article_type">
              <sp-select
                v-model="data.article_type"
                :options="selectDataList.article_type"
                @change="item => (data.article_type_name = item.name)"
              ></sp-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col>
            <a-form-model-item label="摘要">
              <a-textarea v-model="data.brief" placeholder="请输入摘要，若为空则自动生成" :auto-size="{ minRows: 2, maxRows: 4 }" allow-clear />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="6">
            <a-form-model-item label="系列">
              <a-switch v-model="data.is_series"></a-switch>
            </a-form-model-item>
          </a-col>
          <a-col :span="6">
            <a-form-model-item label="发布">
              <a-switch v-model="data.is_show"></a-switch>
            </a-form-model-item>
          </a-col>
          <a-col :span="6">
            <a-form-model-item label="禁止评论">
              <a-switch v-model="data.disable_comment"></a-switch>
            </a-form-model-item>
          </a-col>
          <a-col :span="6">
            <a-form-model-item label="置顶">
              <a-switch v-model="data.is_pop"></a-switch>
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
    <jue-jin ref="juejin" :id="data.id"></jue-jin>
  </div>
</template>

<script>
import { edit, select } from '@/mixins';
import draft from './draft';
import JueJin from './sync/juejin.vue';
import { htmlToText } from 'html-to-text';

export default {
  name: 'post-edit',
  mixins: [edit, draft, select],
  components: { JueJin },
  data() {
    return {
      html: '',
      configs: {},
      editVisible: false,
      controllerName: 'post',
      selectParamNameList: ['article_type'],
      selectEntityNameList: ['category'],
      fileList: [],
      baseUrl: sp.getServerUrl(),
      tags: [],
      data: {
        is_series: false,
        disable_comment: false
      },
      token: this.$store.getters.getToken,
      rules: {
        title: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        post_type: [{ required: true, message: '请选择分类', trigger: 'blur' }],
        article_type: [{ required: true, message: '请选择文章类型', trigger: 'blur' }]
      }
    };
  },
  computed: {
    // 请求头
    headers() {
      return {
        Authorization: 'Bearer ' + this.token
      };
    }
  },
  watch: {
    'data.post_type': {
      handler(newVal, oldVal) {
        if (this.selectDataList.category) {
          let item = this.selectDataList.category.find(item => item.Value === oldVal);
          if (item) {
            const index = this.tags.indexOf(item.Name);
            if (index !== -1) {
              this.tags.splice(index, 1);
            }
          }

          item = this.selectDataList.category.find(item => item.Value === newVal);
          if (item && new Set(this.tags.concat(item.Name)).size === this.tags.concat(item.Name).length) {
            this.tags.push(item.Name);
          }
        }
      }
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
          url: sp.getDownloadUrl(this.data.surface_url)
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
            url: sp.getDownloadUrl(this.data.surface_url)
          }
        ];
      }
      if (!sp.isNullOrEmpty(this.data.tags)) {
        this.data.tags = JSON.parse(this.data.tags);
        this.tags = this.data.tags;
      }
    },
    beforeUpload() {
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
      const url = '/api/sys_file/upload_image?fileType=blog_content&objectId=' + (this.data.id || this.draft.postid || '');
      const formData = new FormData();
      formData.append('file', file);
      sp.post(url, formData, this.headers).then(resp => {
        let oStr = `(${pos})`;
        let nStr = `(${sp.getDownloadUrl(resp.downloadUrl)})`;
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
    // 保存博客
    save() {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.editVisible = false;
          if (sp.isNullOrEmpty(this.data.brief)) {
            this.data.brief = htmlToText(this.html, { baseElement: 'p', limits: { ellipsis: '...', maxInputLength: 200 } });
          }
          if (this.tags) {
            this.data.tags = this.tags;
          }
          this.data.html_content = this.html;
          try {
            await sp.post('api/post/save', this.data);
            this.$message.success('发布成功！');
            this.$router.back();
          } catch (error) {
            this.$message.error(error);
          }
        }
      });
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
    syncBlog(dest) {
      if (sp.isNullOrEmpty(this.data.id)) {
        this.$message.error('请先保存博客，再进行同步');
        return;
      }
      if (dest === 'juejin') {
        this.$refs.juejin.visible = true;
      }
      if (dest === 'wechat') {
        this.$confirm({
          title: '博客同步',
          content: '是否同步博客到微信公众号?',
          okText: '确定',
          cancelText: '取消',
          onOk: () => {
            sp.post(`api/post/sync?id=${this.data.id}&destination=${dest}`)
              .then(() => {
                this.$message.success('同步成功');
              })
              .catch(() => {
                this.$message.error('同步异常');
              });
          },
          onCancel: () => {
            this.$message.info('已取消');
          }
        });
      }
    }
  }
};
</script>

<style lang="less" scoped>
@import 'post.less';
/deep/ .v-note-wrapper {
  z-index: 100;
}
</style>
