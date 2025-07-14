<template>
  <div>
    <sp-editor
      ref="editor"
      v-model="data.content"
      :uploadImgParams="uploadImgParams"
      :disabledMenu="['image', 'video']"
      :preCreate="preCreate"
      @save-data="showEditModal"
    ></sp-editor>
    <a-modal title="编辑" v-model="saveModalVisible" @ok="saveData">
      <a-form-model ref="form" :model="data" style="padding: 0 20px">
        <a-row>
          <a-form-model-item label="书名">
            <a-input v-model="data.book_title"></a-input>
          </a-form-model-item>
        </a-row>
        <a-row>
          <a-form-model-item label="是否前台显示">
            <a-switch v-model="data.is_show"></a-switch>
          </a-form-model-item>
        </a-row>
        <a-row>
          <a-form-model-item label="禁止评论">
            <a-switch v-model="data.disable_comment"></a-switch>
          </a-form-model-item>
        </a-row>
        <a-row>
          <a-form-model-item label="封面">
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
          </a-form-model-item>
        </a-row>
      </a-form-model>
    </a-modal>
    <cloud-upload ref="cloudUpload" @selected="selected"></cloud-upload>
  </div>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'ideaEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'reading_note',
      baseUrl: sp.getServerUrl(),
      fileList: [],
      saveModalVisible: false,
      data: {
        is_show: false,
        disable_comment: false
      }
    };
  },
  computed: {
    uploadImgParams() {
      return {
        fileType: 'reading_note',
        objectId: this.data.id
      };
    }
  },
  mounted() {
    this.$refs.editor.editor.fullScreen();
  },
  methods: {
    preCreate(editor) {
      editor.config.zIndex = 2;
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
    },
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
    preSave() {
      this.data.name = this.data.book_title;
      return true;
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
    getText(str) {
      return str.replace(/<[^<>]+>/g, '').replace(/&nbsp;/gi, '');
    },
    showEditModal() {
      this.saveModalVisible = true;
    },
    postSave() {
      this.$router.push({ name: 'readingNote', params: { id: this.data.id } });
    }
  }
};
</script>
