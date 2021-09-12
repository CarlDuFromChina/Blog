<template>
  <a-modal v-model="editVisible" title="登录" width="300px" :footer="false">
    <a-form-model ref="form" :model="data" :rules="rules">
      <a-form-model-item prop="code">
        <a-input v-model="data.code" placeholder="邮箱"></a-input>
      </a-form-model-item>
      <a-form-model-item prop="password">
        <a-input v-model="data.password" placeholder="密码" type="password" @keyup.enter.native="login"></a-input>
      </a-form-model-item>
    </a-form-model>
    <a-button type="primary" block @click="login">
      登录
    </a-button>
    <a-alert style="margin-top: 8px" message="如果邮箱不存在即注册" type="info" banner closable />
  </a-modal>
</template>

<script>
import { encrypt } from 'web-core';
import { saveAuth } from '../lib/login';
// const phoneReg = /^1[3|4|5|7|8][0-9]{9}$/;

export default {
  name: 'spLogin',
  data() {
    return {
      editVisible: false,
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
          const key = await sp.get('api/System/GetPublicKey');
          const { rsa, md5 } = encrypt;
          this.data.publicKey = key;
          this.data.password = rsa.encrypt(md5.encrypt(this.data.password), key);
          const resp = await sp.post('api/System/Login', this.data);
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

<style></style>
