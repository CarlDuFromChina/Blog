<template>
  <div class="background">
    <div class="login">
      <a-form-model ref="form" :model="data" :rules="rules" class="login__wrapper">
        <a-form-model-item style="margin: 0">
          <span class="header">{{ header }}</span>
        </a-form-model-item>
        <a-form-model-item style="margin-bottom: 20px">
          <span class="desc">{{ '我想给世界留下有价值的东西' }}</span>
        </a-form-model-item>
        <a-form-model-item prop="code">
          <a-input v-model="data.code" placeholder="邮箱或者手机号">
            <a-icon slot="prefix" type="user" style="color: rgba(0, 0, 0, 0.25)" />
          </a-input>
        </a-form-model-item>
        <a-form-model-item prop="password">
          <a-input-password v-model="data.password" placeholder="密码" @keyup.enter.native="signIn">
            <a-icon slot="prefix" type="lock" style="color: rgba(0, 0, 0, 0.25)" />
          </a-input-password>
        </a-form-model-item>
        <a-form-model-item>
          <a href="#/login/forget" class="forget-pwd">忘记密码</a>
        </a-form-model-item>
        <a-form-model-item>
          <a-button style="width: 100%" type="primary" @click="signIn" :loading="loading">登录</a-button>
        </a-form-model-item>
      </a-form-model>
    </div>
    <a-modal v-model="visible" :destroyOnClose="true" title="验证" :footer="false">
      <div ref="mask" id="mask"></div>
    </a-modal>
  </div>
</template>

<script>
import { saveAuth, clearAuth } from '../../lib/login.js';
import { encrypt } from 'web-core';

export default {
  name: 'login',
  data() {
    return {
      header: 'Sixpence Blog',
      data: {
        code: '',
        password: ''
      },
      loading: false,
      rules: {
        code: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      visible: false,
      isLoginFailed: false,
      isPassCheck: false
    };
  },
  created() {
    this.test();
  },
  methods: {
    generateCode() {
      const that = this;
      window.jigsaw.init({
        el: document.getElementById('mask'),
        width: 310, // 可选, 默认310
        height: 155, // 可选, 默认155
        onSuccess: () => {
          that.visible = false;
          that.isPassCheck = true;
          that.signIn();
        },
        onFail: () => {
          this.$message.warn('再试一次');
          this.isPassCheck = false;
        }
      });
    },
    test() {
      sp.get('api/DataService/test').then(resp => {
        if (resp) {
          this.$router.push({ name: 'workplace' });
        } else {
          clearAuth(this.$store);
        }
      });
    },
    async validate() {
      return this.$refs.form
        .validate()
        .then(async valid => {
          if (valid) {
            return Promise.resolve(true);
          }
          return Promise.resolve(false);
        })
        .catch(() => {
          return Promise.resolve(false);
        });
    },
    async signIn() {
      if (this.loading) {
        return;
      }
      this.loading = true;

      // 上次登录失败且未通过人机检查
      if (this.isLoginFailed && !this.isPassCheck) {
        this.visible = true;
        await this.$nextTick();
        this.generateCode();
        this.loading = false;
        return;
      }

      try {
        const valid = await this.validate();
        if (valid) {
          const key = await sp.get('api/DataService/GetPublicKey');
          const url = 'api/AuthUser/login';
          const { rsa, md5 } = encrypt;
          const data = {
            code: this.data.code,
            password: rsa.encrypt(md5.encrypt(this.data.password), key),
            publicKey: key
          };
          var that = this;
          sp.post(url, data)
            .then(resp => {
              if (resp.result) {
                saveAuth(this.$store, resp);
                that.$router.push({ name: 'workplace' });
                that.$message.success(resp.message);
              } else {
                this.isLoginFailed = true;
                this.isPassCheck = false;
                that.$message.error(resp.message);
              }
            })
            .catch(error => {
              this.$message.error(error);
            });
        } else {
          this.$message.error('请检查账号密码是否输入');
        }
      } catch (error) {
        this.$message.error(error);
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
  background: #f0f2f5 url(../../../src/assets/background.svg) no-repeat 50%;
}
.login {
  width: 400px;
  position: absolute;
  left: calc(~'50%' - 200px);
  top: 200px;
  .login__wrapper {
    width: 100%;
    height: 100%;
    text-align: center;
    padding: 0 20px;
    .header {
      font-size: 40px;
      color: rgba(0, 0, 0, 0.85);
      font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
      font-weight: 600;
      line-height: 60px;
    }
    .desc {
      font-size: 14px;
      color: rgba(0, 0, 0, 0.45);
    }
    .forget-pwd {
      float: right;
      color: #52c41a;
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

/deep/ .ant-row {
  .ant-form-explain {
    text-align: left;
  }
}
</style>
