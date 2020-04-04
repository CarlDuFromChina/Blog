<template>
  <div class="login">
    <el-form ref="form" :model="data" :rules="rules" class="login__wrapper">
      <el-form-item>
        <span class="header">{{header}}</span>
      </el-form-item>
      <el-form-item prop="account">
        <el-input v-model="data.account" placeholder="邮箱或者手机号"></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input v-model="data.password" placeholder="密码" show-password></el-input>
      </el-form-item>
      <el-form-item>
        <el-button style="width:100%" type="primary" @click="signIn" :loading="loading">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'login',
  data() {
    return {
      header: 'Sign In',
      data: {
        account: '',
        password: ''
      },
      loading: false,
      rules: {
        account: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      }
    };
  },
  methods: {
    async signIn() {
      this.loading = true;
      try {
        const valid = await this.$refs.form.validate();
        if (valid) {
          const url = 'api/user/login';
          const user = {
            account: this.account,
            password: this.password
          };
          try {
            sp.post(url, user).then(resp => {
              this.$message.success('登录成功');
            });
          } catch (error) {
            this.$message.error(error);
          } finally {
            this.loading = false;
          }
        } else {
          this.loading = false;
        }
      } catch (error) {
        this.$message.error('请检查必填项');
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style lang="less" scoped>
.login {
  width: 400px;
  margin: 200px auto 0 auto;
  border: 5px solid #d1d1d1;
  border-radius: 5%;
  box-shadow: 10px 10px 20px 10px #d1d1d1,
    -10px 10px 10px 10px rgba(255, 255, 255, 0.5);
  .login__wrapper {
    width: 100%;
    height: 100%;
    text-align: center;
    .header {
      line-height: 60px;
      font-size: 40px;
    }
  }
}
/deep/.el-form-item__content {
  margin: 5px 10px;
}
</style>
