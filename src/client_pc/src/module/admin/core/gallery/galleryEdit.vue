<template>
  <a-modal title="上传" v-model="editVisible" width="850px">
    <a-form-model ref="form" :model="data">
      <a-row>
        <a-col>
          <a-form-model-item label="小图">
            <a-spin :spinning="loading">
              <a-upload
                :action="baseUrl"
                ref="files"
                :customRequest="uploadSmallImg"
                :file-list="smallImage"
                :beforeUpload="beforeUpload"
                :remove="removeSmallImg"
                list-type="picture"
                key="small-image-upload"
              >
                <a-button type="primary"> <a-icon type="upload" /> 上传小图</a-button>
              </a-upload>
            </a-spin>
          </a-form-model-item>
        </a-col>
      </a-row>
      <a-row>
        <a-col>
          <a-form-model-item label="大图">
            <a-spin :spinning="loading">
              <a-upload
                :action="baseUrl"
                ref="files"
                :customRequest="uploadBigImg"
                :file-list="bigImage"
                :beforeUpload="beforeUpload"
                :remove="removeBigImg"
                list-type="picture"
                key="big-image-upload"
              >
                <a-button type="primary"> <a-icon type="upload" /> 上传大图</a-button>
              </a-upload>
            </a-spin>
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
    </a-form-model>
    <template slot="footer">
      <a-button type="primary" @click="saveData">保存</a-button>
    </template>
  </a-modal>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'galleryEdit',
  mixins: [edit],
  model: {
    prop: 'visible',
    event: 'input'
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      controllerName: 'Gallery',
      baseUrl: sp.getServerUrl(),
      smallImage: [],
      bigImage: [],
      tags: []
    };
  },
  computed: {
    editVisible: {
      get() {
        return this.visible;
      },
      set(value) {
        this.$emit('input', value);
      }
    }
  },
  methods: {
    preSave() {
      if (!this.smallImage || this.smallImage.length === 0) {
        this.$message.error('图片不能为空');
        return false;
      }
      if (!this.bigImage || this.bigImage.length === 0) {
        this.$message.error('图片不能为空');
        return false;
      }
      if (this.tags) {
        this.data.tags = this.tags.join(', ');
      }
      return true;
    },
    postSave() {
      this.$emit('input', false);
    },
    changeTags(val) {
      this.tags = val;
    },
    upload(param) {
      const url = '/api/DataService/UploadImage?fileType=gallery';
      const formData = new FormData();
      formData.append('file', param.file);
      return sp.post(url, formData, this.headers).then(resp => resp);
    },
    // 上传小图
    uploadSmallImg(param) {
      this.upload(param).then(resp => {
        this.data.previewid = resp.id;
        this.data.preview_url = resp.downloadUrl;
        this.smallImage = [
          {
            uid: '0',
            status: 'done',
            name: 'small_image',
            url: `${this.baseUrl}${this.data.preview_url}`
          }
        ];
      });
    },
    // 上传大图
    uploadBigImg(param) {
      this.upload(param).then(resp => {
        this.data.imageid = resp.id;
        this.data.image_url = resp.downloadUrl;
        this.bigImage = [
          {
            uid: '0',
            status: 'done',
            name: 'big_image',
            url: `${this.baseUrl}${this.data.image_url}`
          }
        ];
      });
    },
    beforeUpload(file, fileList) {
      if (fileList && fileList.length > 1) {
        fileList = fileList.slice(-1);
      }
      return true;
    },
    removeSmallImg() {
      this.data.previewid = '';
      this.data.preview_url = '';
      this.smallImage = [];
    },
    removeBigImg() {
      this.data.imageid = '';
      this.data.image_url = '';
      this.bigImage = [];
    }
  }
};
</script>

<style></style>
