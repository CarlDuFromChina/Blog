<template>
  <a-form-model ref="form" :layout="layout" :model="data">
    <a-form-model-item label="标题">
      <a-input v-model="data.name"></a-input>
    </a-form-model-item>
    <a-form-model-item label="分类">
      <a-select v-model="data.recommend_type" @change="handleTypeChange">
        <a-select-option v-model="item.Value" v-for="(item, index) in recommentType" :key="index">{{ item.Name }}</a-select-option>
      </a-select>
    </a-form-model-item>
    <a-form-model-item label="链接">
      <a-input v-model="data.url"></a-input>
    </a-form-model-item>
    <a-form-model-item label="图片">
      <a-upload class="upload-demo" ref="upload" action="" list-type="picture" :default-file-list="fileList">
        <a-button size="small" type="primary"> <a-icon type="upload" /> 上传</a-button>
      </a-upload>
    </a-form-model-item>
    <a-form-model-item :wrapper-col="{ span: 4, offset: 20 }">
      <a-button type="primary" @click="saveData">
        确 定
      </a-button>
      <a-button style="margin-left: 10px;" @click="$emit('close')">
        取 消
      </a-button>
    </a-form-model-item>
  </a-form-model>
</template>

<script>
import { edit } from 'vue-pc-admin';

export default {
  name: 'recommendInfoEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'RecommendInfo',
      Id: '',
      data: {},
      recommentType: [],
      fileList: [],
      layout: 'horizontal'
    };
  },
  created() {
    sp.get('api/SysParamGroup/GetParams?code=recommend_type').then(resp => {
      this.recommentType = resp;
    });
  },
  methods: {
    handleTypeChange(value) {
      const arrs = this.recommentType.filter(item => item.Value === value);
      if (arrs.length > 0) {
        this.data.recommend_typeName = arrs[0].Name;
      }
    },
    postSave() {
      if (!sp.isNull(this.fileList) && this.fileList.length > 0) {
        const headers = {
          Authorization: 'BasicAuth ' + window.localStorage.getItem('Token')
        };
        const url = '/api/DataService/UploadImage?fileType=book_surface&objectId=' + this.Id;
        const formData = new FormData();
        formData.append('file', this.fileList[0]);
        sp.post(url, formData, headers);
      }
    }
  }
};
</script>

<style></style>
