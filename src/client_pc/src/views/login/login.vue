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
          <a-input v-model="data.code" placeholder="邮箱或者手机号" allowClear>
            <a-icon slot="prefix" type="user" style="color: rgba(0, 0, 0, 0.25)" />
          </a-input>
        </a-form-model-item>
        <a-form-model-item prop="password">
          <a-input-password v-model="data.password" placeholder="密码" @keyup.enter.native="signIn">
            <a-icon slot="prefix" type="lock" style="color: rgba(0, 0, 0, 0.25)" />
          </a-input-password>
        </a-form-model-item>
        <a-form-model-item>
          <a @click="() => (signupVisible = true)" class="signup">注册</a>
          <a @click="() => (forgetVisible = true)" class="forget-pwd">忘记密码</a>
        </a-form-model-item>
        <a-form-model-item>
          <a-button style="width: 100%" type="primary" @click="signIn" :loading="loading">登录</a-button>
        </a-form-model-item>
        <a-form-model-item :style="{ textAlign: 'center' }">
          <span style="color: #999aaa">其他登录方式</span><br>
          <sp-icon name="sp-blog-github" :size="20" @click="thirdPartyLogin('github')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
          <sp-icon name="sp-blog-gitee" :size="20" @click="thirdPartyLogin('gitee')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
          <sp-icon name="sp-blog-qq" :size="20" @click="thirdPartyLogin('qq')" cursor="pointer" :style="{ paddingRight: '8px' }"></sp-icon>
        </a-form-model-item>
      </a-form-model>
    </div>
    <a-modal v-model="visible" :destroyOnClose="true" title="验证" :footer="false">
      <div ref="mask" id="mask"></div>
    </a-modal>
    <a-modal v-model="signupVisible" title="注册" :footer="false" destroyOnClose :width="300">
      <a-form-model ref="signupForm" :model="signupData" :rules="signupRules">
        <a-form-model-item prop="code">
          <a-input v-model="signupData.code" placeholder="请输入邮箱" allowClear :disabled="signupLoading"></a-input>
        </a-form-model-item>
        <a-form-model-item prop="password">
          <a-input
            v-model="signupData.password"
            placeholder="请输入密码"
            type="password"
            :disabled="signupLoading"
            @keyup.enter.native="signup"
          ></a-input>
        </a-form-model-item>
      </a-form-model>
      <a-button type="primary" block @click="signup" :loading="signupLoading">
        注册
      </a-button>
    </a-modal>
    <a-modal v-model="forgetVisible" title="忘记密码" :footer="false" destroyOnClose :width="300">
      <a-form-model ref="forgetForm" :model="forgetData" :rules="forgetRules">
        <a-form-model-item prop="code">
          <a-input v-model="forgetData.code" placeholder="请输入用户注册邮箱" allowClear :disabled="forgetLoading"></a-input>
        </a-form-model-item>
      </a-form-model>
      <a-button type="primary" block @click="forgetPwd" :loading="forgetLoading">
        确定
      </a-button>
    </a-modal>
  </div>
</template>

<script>
import { saveAuth, clearAuth, thirdPartyLogin } from '../../lib/login.js';
import { encrypt } from '@sixpence/web-core';

var header = process.env.VUE_APP_TITLE;

export default {
  name: 'login',
  data() {
    return {
      header,
      data: {
        code: '',
        password: ''
      },
      signupData: {
        code: '',
        password: ''
      },
      forgetData: {
        code: ''
      },
      loading: false,
      signupLoading: false,
      forgetLoading: false,
      rules: {
        code: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      signupRules: {
        code: [{ required: true, message: '请输入邮箱', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      forgetRules: {
        code: [{ required: true, message: '请输入用户注册邮箱', trigger: 'blur' }]
      },
      visible: false,
      signupVisible: false,
      isLoginFailed: false,
      isPassCheck: false,
      forgetVisible: false,
      thirdPartyLogin: thirdPartyLogin
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
      sp.get('api/system/test').then(resp => {
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
          const key = await sp.get('api/system/public_key');
          const url = 'api/system/Login';
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
                that.$router.push({ name: 'index' });
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
    },
    forgetPwd() {
      if (this.forgetLoading) {
        return;
      }
      this.forgetLoading = true;
      this.$refs.forgetForm.validate(resp => {
        if (resp) {
          sp.get(`api/system/password/forget?code=${this.forgetData.code}`)
            .then(() => {
              this.$message.error('重置密码邮件已发送');
            })
            .catch(err => {
              this.$message.error(err);
            })
            .finally(() => {
              this.forgetLoading = false;
            });
        } else {
          this.forgetLoading = false;
        }
      });
    },
    async signup() {
      if (this.signupLoading) {
        return;
      }
      this.signupLoading = true;
      const originPwd = this.signupData.password;
      try {
        const valid = await this.$refs.signupForm.validate();
        if (valid) {
          const key = await sp.get('api/system/public_key');
          const { rsa, md5 } = encrypt;
          this.signupData.publicKey = key;
          this.signupData.password = rsa.encrypt(md5.encrypt(this.signupData.password), key);
          const resp = await sp.post('api/system/signup', this.signupData);
          if (resp.level === 'Warning') {
            this.$message.warning(resp.message);
            this.signupVisible = false;
            this.$set(this.signupData, 'password', null);
          } else {
            this.$message.error(resp.message);
            this.$set(this.signupData, 'password', originPwd);
          }
        }
      } catch (error) {
        this.$set(this.signupData, 'password', originPwd);
        this.$message.error('注册失败');
      } finally {
        this.signupLoading = false;
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
  background: #f0f2f5 no-repeat 50%;
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
    .signup {
      float: left;
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
