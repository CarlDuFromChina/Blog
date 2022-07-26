<template>
  <a-modal v-model="editVisible" title="登录" width="300px" :footer="false">
    <a-form-model ref="form" :model="data" :rules="rules">
      <a-form-model-item prop="code">
        <a-input v-model="data.code" placeholder="邮箱，如果邮箱不存在即注册"></a-input>
      </a-form-model-item>
      <a-form-model-item prop="password">
        <a-input v-model="data.password" placeholder="密码" type="password" @keyup.enter.native="login"></a-input>
      </a-form-model-item>
    </a-form-model>
    <a-button type="primary" block @click="login">
      登录
    </a-button>
    <a-form-model-item :style="{ textAlign: 'center', paddingTop: '20px' }">
      <span style="color: #999aaa">其他登录方式</span><br>
      <sp-icon name="sp-blog-github" :size="20" @click="thirdPartyLogin('github')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
      <sp-icon name="sp-blog-gitee" :size="20" @click="thirdPartyLogin('gitee')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
      <sp-icon name="sp-blog-qq" :size="20" @click="thirdPartyLogin('qq')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
    </a-form-model-item>
  </a-modal>
</template>

<script>
import { encrypt } from '@sixpence/web-core';
import { saveAuth, thirdPartyLogin } from '../lib/login';
// const phoneReg = /^1[3|4|5|7|8][0-9]{9}$/;

export default {
  name: 'spLogin',
  data() {
    return {
      editVisible: false,
      thirdPartyLogin: thirdPartyLogin,
      data: {
        code: '',
        password: null
      },
      rules: {
        password: [{ required: true, message: '请输入密码', trigger: ['blur', 'change'] }],
        code: [{ required: true, message: '请输入邮箱', trigger: 'blur' }]
      }
    };
  },
  methods: {
    login() {
      this.$refs.form.validate().then(async () => {
        try {
          const key = await sp.get('api/system/public_key');
          const { rsa, md5 } = encrypt;
          this.data.publicKey = key;
          this.data.password = rsa.encrypt(md5.encrypt(this.data.password), key);
          const resp = await sp.post('api/system/fast_signin', this.data);
          if (resp.result) {
            saveAuth(this.$store, resp);
            this.editVisible = false;
            this.$message.success('登录成功');
            this.$emit('closed');
          } else {
            this.$set(this.data, 'password', null);
            if (resp.level === 'Warning') {
              this.$message.warning(resp.message);
            } else {
              this.$message.error(resp.message);
            }
          }
        } catch (error) {
          this.$set(this.data, 'password', null);
          this.$message.error('登录失败');
        }
      });
    },
    cancel() {
      this.$emit('closed');
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-modal-body {
  padding: 24px 24px 0 24px;
}
</style>
