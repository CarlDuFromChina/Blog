<template>
  <el-form ref="form" :model="data" :rules="rules" label-width="100px" label-position="top" class="register">
    <el-form-item class="header">
      <span>Sign Up</span>
    </el-form-item>
    <el-form-item label="昵称" prop="name">
      <a-input v-model="data.name" placeholder="昵称"></a-input>
    </el-form-item>
    <el-form-item label="邮箱" prop="email">
      <a-input v-model="data.email" placeholder="邮箱"></a-input>
    </el-form-item>
    <el-form-item label="密码" prop="password">
      <a-input v-model="data.password" placeholder="密码" type="password"></a-input>
    </el-form-item>
    <el-form-item label="重复输入密码" prop="secPassword">
      <a-input v-model="data.secPassword" placeholder="再次输入密码" type="password"></a-input>
    </el-form-item>
    <el-form-item class="buttons">
      <el-button type="primary" @click="register()" :loading="loading">注册</el-button>
      <el-button @click="reset()">重置</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
const pwdReg = /(?!^(\d+|[a-zA-Z]+|[~!@#$%^&*?]+)$)^[\w~!@#$%^&*?]{6,12}$/;
const mailReg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,})$/;

export default {
  name: 'register',
  data() {
    var validatePwd = (rule, value, callback) => {
      if (!pwdReg.test(value)) {
        callback(new Error('请重新尝试“字母+数字”的6-12位组合!'));
      } else {
        callback();
      }
    };
    var validateComfirmPwd = (rule, value, callback) => {
      if (this.data.password !== value) {
        callback(new Error('两次密码不一致'));
      } else {
        callback();
      }
    };
    return {
      data: {
        name: '',
        email: '',
        password: '',
        secPassword: ''
      },
      rules: {
        name: [
          { required: true, message: '请输入昵称', trigger: 'blur' },
          { min: 3, max: 8, message: '长度在 3 到 8 个字符', trigger: 'blur' }
        ],
        email: [
          { required: true, message: '请输入邮箱', trigger: 'blur' },
          { pattern: mailReg, message: '请输入正确的邮箱' }
        ],
        password: [
          { required: true, message: '请输入密码', trigger: ['blur', 'change'] },
          { trigger: 'blur', validator: validatePwd }
        ],
        secPassword: [
          { required: true, message: '请再次输入密码', trigger: ['blur', 'change'] },
          { trigger: 'blur', validator: validateComfirmPwd }
        ]
      },
      loading: false
    };
  },
  methods: {
    register() {
      this.$refs.form.validate(valid => {
        this.loading = true;
        if (valid) {
          setTimeout(() => {
            this.$emit('reg-success', this.data);
            this.$message.success('注册成功！');
            this.loading = false;
          }, 300);
        } else {
          this.loading = false;
        }
      });
    },
    reset() {
      this.$refs.form.resetFields();
      this.$emit('reg-reset');
    }
  }
};
</script>

<style lang="less" scoped>
.header {
  line-height: 60px;
  text-align: center;
  span {
    width: 100%;
    font-size: 40px;
  }
}

.buttons {
  text-align: center;
}

.register {
  width: 500px;
  height: 400px;
  .el-form-item {
    margin-left: 50px;
    width: 300px;
  }
}
</style>
