<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-tabs default-active-key="1" :animated="false">
      <a-tab-pane key="1" tab="个人资料">
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="昵称" prop="name">
              <a-input v-model="data.name"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="编码" prop="code">
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
              <a-input v-model="data.introduction" type="textarea" placeholder="请介绍一下你自己...."></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="6">
            <a-form-model-item label="头像">
              <a-upload
                ref="file"
                list-type="picture-card"
                :file-list="fileList"
                @preview="handlePreview"
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
          <a-col :span="6">
            <a-form-model-item label="生活照">
              <a-upload
                ref="file"
                list-type="picture-card"
                :file-list="lifePhotos"
                @preview="handlePreview"
                @change="handleLifePhotoChange"
                :remove="handleLifePhotoRemove"
                :before-upload="beforeLifePhotoUpload"
              >
                <div v-if="lifePhotos && lifePhotos.length === 0">
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
            <a-form-model-item label="邮箱" prop="mailbox">
              <a-input v-model="data.mailbox"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="手机号码" prop="cellphone">
              <a-input v-model="data.cellphone"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-model-item label="角色" prop="roleid">
              <sp-select v-model="data.roleid" :options="roles" @change="item => (data.roleid_name = item.name)"></sp-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="12">
            <a-form-model-item label="状态">
              <a-switch v-model="data.statecode"></a-switch>
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
      <a-tab-pane key="3" tab="第三方登录">
        <a-row>
          <a-descriptions title="已绑定"></a-descriptions>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <sp-icon name="sp-blog-github" :size="40" v-show="showGithub" :style="{ paddingRight: '16px' }"></sp-icon>
            <sp-icon name="sp-blog-gitee" :size="40" v-show="showGitee" :style="{ paddingRight: '16px' }"></sp-icon>
            <sp-icon name="sp-blog-qq" :size="40" v-show="showQQ" :style="{ paddingRight: '16px' }"></sp-icon>
          </a-col>
        </a-row>
        <a-row :style="{ paddingTop: '30px' }">
          <a-descriptions title="未绑定"></a-descriptions>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <sp-icon
              name="sp-blog-github"
              cursor="pointer"
              :size="40"
              @click="thirdPartyBind('github', data.id)"
              v-show="!showGithub"
              :style="{ paddingRight: '16px' }"
            ></sp-icon>
            <sp-icon
              name="sp-blog-gitee"
              cursor="pointer"
              :size="40"
              @click="thirdPartyBind('gitee', data.id)"
              v-show="!showGitee"
              :style="{ paddingRight: '16px' }"
            ></sp-icon>
            <sp-icon
              name="sp-blog-qq"
              cursor="pointer"
              :size="40"
              @click="thirdPartyBind('qq', data.id)"
              v-show="!showQQ"
              :style="{ paddingRight: '16px' }"
            ></sp-icon>
          </a-col>
        </a-row>
      </a-tab-pane>
    </a-tabs>
    <edit-password ref="pwd"></edit-password>
    <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
      <img alt="example" style="width: 100%" :src="previewImage" />
    </a-modal>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';
import editPassword from '../editPasssword/editPassword.vue';
import { thirdPartyBind } from '../../../../lib/login';

function getBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });
}

export default {
  name: 'userInfo-edit',
  mixins: [edit],
  components: { editPassword },
  data() {
    return {
      controllerName: 'user_info',
      thirdPartyBind: thirdPartyBind,
      avatarChange: false,
      lifePhotoChange: false,
      baseUrl: sp.getServerUrl(),
      token: '',
      fileList: [],
      lifePhotos: [],
      roles: [],
      rules: {
        name: [{ required: true, message: '请输入昵称', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }],
        gender: [{ required: true, message: '请选择性别', trigger: 'blur' }],
        mailbox: [{ required: true, message: '请输入邮箱', trigger: 'blur' }],
        cellphone: [{ required: true, message: '请输入手机号码', trigger: 'blur' }],
        roleid: [{ required: true, message: '请选择角色', trigger: 'blur' }]
      },
      previewImage: '',
      previewVisible: false
    };
  },
  async created() {
    this.token = this.$store.getters.getToken;
    sp.get('api/sys_role/role_options').then(resp => {
      this.roles = resp;
    });
  },
  computed: {
    headers() {
      return {
        Authorization: 'Bearer ' + this.token
      };
    },
    showGithub() {
      return !!this.data.github_id;
    },
    showGitee() {
      return !!this.data.gitee_id;
    },
    showQQ() {
      return !!this.data.qq_id;
    }
  },
  methods: {
    handleChangeGender() {
      this.data.gender_name = this.data.gender === 0 ? '男' : '女';
    },
    handleAvatarChange(file) {
      if (file) {
        this.avatarChange = true;
      }
    },
    handleLifePhotoChange(file) {
      if (file) {
        this.lifePhotoChange = true;
      }
    },
    async loadComplete() {
      if (!sp.isNullOrEmpty(this.data.avatar)) {
        const image = await sp.get(`api/sys_file/${this.data.avatar}`);
        this.fileList = [
          {
            uid: '-1',
            name: image.name,
            status: 'done',
            url: sp.getDownloadUrl(image.id, false)
          }
        ];
      }
      if (!sp.isNullOrEmpty(this.data.life_photo)) {
        const image = await sp.get(`api/sys_file/${this.data.life_photo}`);
        this.lifePhotos = [
          {
            uid: '-1',
            name: image.name,
            status: 'done',
            url: sp.getDownloadUrl(image.id, false)
          }
        ];
      }
    },
    beforeUpload(file) {
      this.fileList = [...this.fileList, file];
      return false;
    },
    beforeLifePhotoUpload(file) {
      this.lifePhotos = [...this.lifePhotos, file];
      return false;
    },
    preSave() {
      if (this.avatarChange) {
        // 保存后上传头像
        let url = '/api/sys_file/upload_image?fileType=avatar';
        // 关联实体id
        if (!sp.isNullOrEmpty(this.data.id)) {
          url += `&objectId=${this.data.id}`;
        }
        const formData = new FormData();
        formData.append('file', this.fileList[0]);
        return sp.post(url, formData, this.headers).then(resp => {
          this.data.avatar = resp.id;
          return true;
        });
      }
      if (this.lifePhotoChange) {
        // 保存后上传头像
        let url = '/api/sys_file/upload_image?fileType=life_photo';
        // 关联实体id
        if (!sp.isNullOrEmpty(this.data.id)) {
          url += `&objectId=${this.data.id}`;
        }
        const formData = new FormData();
        formData.append('file', this.lifePhotos[0]);
        return sp.post(url, formData, this.headers).then(resp => {
          this.data.life_photo = resp.id;
          return true;
        });
      }
      return Promise.resolve(true);
    },
    postSave() {
      location.reload();
    },
    remove() {
      this.data.avatar = null;
      this.fileList = [];
      this.avatarChange = false;
    },
    handleLifePhotoRemove() {
      this.data.life_photo = null;
      this.lifePhotos = [];
      this.lifePhotoChange = false;
    },
    editPassword() {
      this.$refs.pwd.editVisible = true;
    },
    handleCancel() {
      this.previewVisible = false;
    },
    async handlePreview(file) {
      if (!file.url && !file.preview) {
        file.preview = await getBase64(file.originFileObj);
      }
      this.previewImage = file.url || file.preview;
      this.previewVisible = true;
    }
  }
};
</script>
