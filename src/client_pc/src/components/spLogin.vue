<template>
  <a-modal v-model="editVisible" title="登录" @ok="save" @cancel="cancel" width="30%" okText="确认" cancelText="取消">
    <a-form-model ref="form" :model="data" :rules="rules">
      <a-form-model-item label="邮箱" prop="code">
        <a-input v-model="data.code" placeholder="邮箱"></a-input>
      </a-form-model-item>
      <a-form-model-item label="密码" prop="password">
        <a-input v-model="data.password" placeholder="密码" type="password"></a-input>
      </a-form-model-item>
    </a-form-model>
  </a-modal>
</template>

<script>
import { encrypt } from 'web-core';
import { saveAuth } from '../lib/login';
const mailReg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,})$/;
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
        code: [
          { required: true, message: '请输入邮箱', trigger: 'blur' },
          { pattern: mailReg, message: '请输入正确的邮箱' }
        ]
      }
    };
  },
  methods: {
    save() {
      this.$refs.form.validate().then(async () => {
        const key = await sp.get('api/DataService/GetPublicKey');
        const { rsa, md5 } = encrypt;
        this.data.publicKey = key;
        this.data.password = rsa.encrypt(md5.encrypt(this.data.password), key);
        const resp = await sp.post('api/Blog/SignUp', this.data);
        if (resp.result) {
          saveAuth(this.$store, resp);
        }
        this.editVisible = false;
        this.$message.success('登录成功');
        this.$emit('closed');
      });
    },
    cancel() {
      this.$emit('closed');
    }
  }
};
</script>

<style></style>
