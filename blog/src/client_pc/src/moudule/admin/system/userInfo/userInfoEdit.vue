<template>
  <el-form
    ref="form"
    :model="data"
    label-width="80px"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
  >
    <el-row>
      <el-col :span="12">
        <el-form-item label="昵称">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="编码">
          <el-input v-model="data.code" disabled></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="真实姓名">
          <el-input v-model="data.realname"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="性别">
          <el-select v-model="data.gender" placeholder="请选择状态" @change="handleChangeGender">
            <el-option
              v-for="(item, index) in [
                { name: '男', value: 0 },
                { name: '女', value: 1 }
              ]"
              :key="index"
              :label="item.name"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="邮箱">
          <el-input v-model="data.mailbox"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="手机号码">
          <el-input v-model="data.cellphone"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-form-item label="自我介绍">
          <el-input v-model="data.introduction" type="textarea"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="状态">
          <el-select v-model="data.stateCode" placeholder="请选择状态" @change="handleChange">
            <el-option
              v-for="(item, index) in [
                { name: '启用', value: 1 },
                { name: '禁用', value: 0 }
              ]"
              :key="index"
              :label="item.name"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-form-item label="头像">
          <el-upload ref="file" class="avatar-uploader" action="" :file-list="fileList" :on-change="change" :on-remove="remove" :auto-upload="false">
            <img v-if="imageUrl" :src="imageUrl" class="avatar" />
            <i v-else class="el-icon-plus avatar-uploader-icon" slot="trigger"></i>
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
  name: 'userInfo-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'UserInfo',
      file: null,
      imageUrl: '',
      baseUrl: '',
      token: ''
    };
  },
  created() {
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
    handleChange(val) {
      this.data.stateCodeName = val === 1 ? '启用' : '禁用';
    },
    handleChangeGender(val) {
      this.data.genderName = val === 0 ? '男' : '女';
    },
    async loadComplete() {
      if (!sp.isNullOrEmpty(this.data.avatar)) {
        const image = await sp.get(`api/SysFile/GetData?id=${this.data.avatar}`);
        this.imageUrl = this.baseUrl + '/temp/' + image.name;
        this.fileList = [
          {
            name: image.name,
            url: this.imageUrl
          }
        ];
      }
    },
    // 加载头像
    change() {
      const reader = new FileReader();
      this.file = this.$refs.file.uploadFiles[0].raw;
      reader.readAsDataURL(this.file);
      reader.onloadend = e => {
        this.imageUrl = e.target.result;
      };
    },
    preSave() {
      // 保存后上传头像
      let url = '/api/DataService/UploadImage?fileType=avatar';
      // 关联实体id
      if (!sp.isNullOrEmpty(this.Id)) {
        url += `&objectId=${this.Id}`;
      }
      const formData = new FormData();
      formData.append('file', this.file);
      return sp.post(url, formData, this.headers).then(resp => (this.data.avatar = resp.id));
    },
    remove() {
      this.data.avatar = null;
      this.imageUrl = '';
    }
  }
};
</script>

<style lang="less" scoped>
.avatar-uploader {
  /deep/ .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  /deep/ .el-upload:hover {
    border-color: #409eff;
  }
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
