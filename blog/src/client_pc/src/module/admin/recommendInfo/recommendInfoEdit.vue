<template>
  <el-form ref="form" :model="data" label-width="80px">
    <el-row>
      <el-col :span="12">
        <el-form-item label="标题">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="分类">
          <el-select v-model="data.recommend_type" @change="handleTypeChange">
            <el-option :label="item.Name" :value="item.Value" v-for="(item, index) in recommentType" :key="index"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="链接">
          <el-input v-model="data.url"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-form-item label="图片">
          <el-upload class="upload-demo" ref="upload" action="" list-type="picture" :file-list="fileList" :auto-upload="false">
            <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
            <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
          </el-upload>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="saveData">确 定</el-button>
        </span>
      </el-col>
    </el-row>
  </el-form>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'recommendInfoEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'RecommendInfo',
      Id: '',
      data: {},
      recommentType: [],
      fileList: []
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
