<template>
  <a-modal title="修改密码" v-model="editVisible" width="40%" ok-text="确认" cancel-text="取消" @ok="savePassword">
    <a-form-model ref="form" :model="data" :rules="rules">
      <a-row>
        <a-col>
          <a-form-model-item label="密码" prop="password">
            <a-input type="password" v-model="data.password" placeholder="请输入新密码"></a-input>
          </a-form-model-item>
        </a-col>
      </a-row>
      <a-row>
        <a-col>
          <a-form-model-item label="确认密码" prop="password2">
            <a-input type="password" v-model="data.password2" placeholder="请再次输入密码"></a-input>
          </a-form-model-item>
        </a-col>
      </a-row>
    </a-form-model>
  </a-modal>
</template>

<script>
import { encrypt } from '@sixpence/js-utils';

export default {
  name: 'edit-password',
  data() {
    return {
      editVisible: false,
      data: {
        password: '',
        password2: ''
      },
      rules: {
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
        password2: [{ required: true, message: '请再次输入密码', trigger: 'blur' }]
      }
    };
  },
  methods: {
    savePassword() {
      this.$refs.form.validate(resp => {
        if (resp) {
          if (this.data.password !== this.data.password2) {
            this.$message.error('两次密码不一致');
          } else {
            var pwd = encrypt.md5.encrypt(this.data.password);
            sp.put('api/system/password', `"${pwd}"`).then(() => {
              this.$message.success('修改密码成功');
              this.editVisible = false;
              this.reset();
            });
          }
        } else {
          this.$message.error('请检查表单必填项');
        }
      });
    },
    reset() {
      this.data.password = '';
      this.data.password2 = '';
    }
  }
};
</script>

<style></style>
