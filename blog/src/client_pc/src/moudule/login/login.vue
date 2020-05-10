<template>
  <div class="background" :style="{ 'background-image': `url(${imageUrl})` }">
    <div class="login">
      <el-form ref="form" :model="data" :rules="rules" class="login__wrapper">
        <el-form-item>
          <span class="header">{{ header }}</span>
        </el-form-item>
        <el-form-item prop="code">
          <el-input v-model="data.code" placeholder="邮箱或者手机号"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="data.password" placeholder="密码" @keyup.enter.native="signIn" show-password></el-input>
        </el-form-item>
        <el-form-item>
          <el-button style="width:100%" type="primary" @click="signIn" :loading="loading">登录</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'login',
  data() {
    return {
      header: 'Sign In',
      data: {
        code: '',
        password: ''
      },
      loading: false,
      rules: {
        code: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      imageUrl: ''
    };
  },
  created() {
    this.test();
    this.loadBackground();
  },
  methods: {
    test() {
      sp.get('api/DataService/test').then(resp => {
        this.$router.push('home');
        sp.refreshRouter.call({ router: this.$router });
      });
    },
    loadBackground() {
      sp.get('api/DataService/GetRandomImage')
        .then(image => {
          this.imageUrl = JSON.parse(image).imgurl;
        })
        .catch(() => this.$message.error('背景加载失败'));
    },
    async signIn() {
      this.loading = true;
      try {
        const valid = await this.$refs.form.validate();
        if (valid) {
          const url = 'api/AuthUser/login';
          try {
            sp.post(url, this.data).then(resp => {
              if (resp.result) {
                localStorage.setItem('Token', resp.Ticket);
                this.$router.push('home');
                sp.refreshRouter.call({ router: this.$router });
                this.$message.success('登录成功');
              } else {
                this.$message.error('账号密码错误');
              }
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
.background {
  width: 100%;
  height: 100%;
  background-size: 100% 100%;
}
.login {
  width: 400px;
  position: absolute;
  left: calc(~'50%' - 200px);
  top: 200px;
  border: 1px solid #d1d1d1;
  border-radius: 5%;
  background: #f7f1f5;
  filter: alpha(Opacity=60);
  -moz-opacity: 0.6;
  opacity: 0.6;
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
.login:hover {
  opacity: 1;
  transition: all 800ms;
}
/deep/.el-form-item__content {
  margin: 5px 10px;
}
</style>
