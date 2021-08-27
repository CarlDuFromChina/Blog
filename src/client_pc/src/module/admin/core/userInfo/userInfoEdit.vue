<template>
  <a-form-model ref="form" :model="data">
    <a-tabs default-active-key="1" :animated="false">
      <a-tab-pane key="1" tab="个人资料">
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="昵称">
              <a-input v-model="data.name"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="编码">
              <a-input v-model="data.code" :disabled="pageState == 'edit'"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="真实姓名">
              <a-input v-model="data.realname"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="性别">
              <a-radio-group v-model="data.gender" @change="handleChangeGender">
                <a-radio
                  v-for="(item, index) in [
                    { name: '男', value: 0 },
                    { name: '女', value: 1 }
                  ]"
                  :key="index"
                  :value="item.value"
                  >{{ item.name }}</a-radio
                >
              </a-radio-group>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col>
            <a-form-model-item label="自我介绍">
              <a-input v-model="data.introduction" type="textarea"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="头像">
              <a-upload
                ref="file"
                list-type="picture-card"
                :file-list="fileList"
                @change="handleAvatarChange"
                :remove="remove"
                :before-upload="beforeUpload"
              >
                <div v-if="fileList && fileList.length === 0">
                  <a-icon type="plus" />
                </div>
              </a-upload>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-tab-pane>
      <a-tab-pane key="2" tab="账号设置">
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="邮箱">
              <a-input v-model="data.mailbox"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="手机号码">
              <a-input v-model="data.cellphone"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="角色">
              <sp-select v-model="data.roleid" :options="roles" @change="item => (data.roleidName = item.name)"></sp-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="状态">
              <a-radio-group v-model="data.stateCode" @change="handleStateCodeChange">
                <a-radio
                  v-for="(item, index) in [
                    { name: '启用', value: 1 },
                    { name: '禁用', value: 0 }
                  ]"
                  :key="index"
                  :value="item.value"
                  >{{ item.name }}</a-radio
                >
              </a-radio-group>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="密码">
              <a-button @click="editPassword">重置</a-button>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-tab-pane>
    </a-tabs>
    <edit-password ref="pwd"></edit-password>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';
import editPassword from '../editPasssword/editPassword.vue';

export default {
  name: 'userInfo-edit',
  mixins: [edit],
  components: { editPassword },
  data() {
    return {
      controllerName: 'UserInfo',
      avatarChange: false,
      baseUrl: sp.getServerUrl(),
      token: '',
      fileList: [],
      roles: []
    };
  },
  async created() {
    this.token = this.$store.getters.getToken;
    sp.get('api/SysRole/GetBasicRole').then(resp => {
      this.roles = resp;
    });
  },
  computed: {
    headers() {
      return {
        Authorization: 'Bearer ' + this.token
      };
    }
  },
  methods: {
    handleChangeGender(val) {
      this.data.genderName = this.data.gender === 0 ? '男' : '女';
    },
    handleStateCodeChange(val) {
      this.data.stateCodeName = this.data.stateCode === 1 ? '启用' : '禁用';
    },
    handleAvatarChange(file) {
      if (file) {
        this.avatarChange = true;
      }
    },
    async loadComplete() {
      if (!sp.isNullOrEmpty(this.data.avatar)) {
        const image = await sp.get(`api/SysFile/GetData?id=${this.data.avatar}`);
        this.fileList = [
          {
            uid: '-1',
            name: image.name,
            status: 'done',
            url: `${this.baseUrl}api/SysFile/Download?objectId=${image.sys_fileId}`
          }
        ];
      }
    },
    beforeUpload(file) {
      this.fileList = [...this.fileList, file];
      return false;
    },
    preSave() {
      if (!this.avatarChange) {
        return Promise.resolve(true);
      }
      // 保存后上传头像
      let url = '/api/System/UploadImage?fileType=avatar';
      // 关联实体id
      if (!sp.isNullOrEmpty(this.Id)) {
        url += `&objectId=${this.Id}`;
      }
      const formData = new FormData();
      formData.append('file', this.fileList[0]);
      return sp.post(url, formData, this.headers).then(resp => (this.data.avatar = resp.id));
    },
    remove() {
      this.data.avatar = null;
      this.fileList = [];
    },
    editPassword() {
      this.$refs.pwd.editVisible = true;
    }
  }
};
</script>
