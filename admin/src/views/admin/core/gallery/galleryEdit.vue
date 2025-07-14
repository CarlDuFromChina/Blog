<template>
  <a-modal title="上传" v-model="editVisible" width="850px" destroyOnClose>
    <a-form-model ref="form" :model="data">
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
                <a-button type="primary"> <a-icon type="upload" /> 上传</a-button>
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
      controllerName: 'gallery',
      baseUrl: sp.getServerUrl(),
      smallImage: [],
      bigImage: [],
      token: this.$store.getters.getToken,
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
    },
    // 请求头
    headers() {
      return {
        Authorization: 'Bearer ' + this.token
      };
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
      this.$emit('saved');
    },
    changeTags(val) {
      this.tags = val;
    },
    upload(param) {
      const url = '/api/sys_file/upload_big_image?fileType=gallery&objectId=';
      const formData = new FormData();
      formData.append('file', param.file);
      return sp.post(url, formData, this.headers).then(resp => resp);
    },
    // 上传大图
    uploadBigImg(param) {
      this.upload(param).then(resp => {
        const image = resp[0];
        const thumbnail = resp[1];

        this.data.imageid = image.id;
        this.data.image_url = image.downloadUrl;
        this.bigImage = [
          {
            uid: '0',
            status: 'done',
            name: 'big_image',
            url: sp.getDownloadUrl(image.downloadUrl)
          }
        ];

        this.data.previewid = thumbnail.id;
        this.data.preview_url = thumbnail.downloadUrl;
        this.smallImage = [
          {
            uid: '0',
            status: 'done',
            name: 'small_image',
            url: sp.getDownloadUrl(thumbnail.downloadUrl)
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
