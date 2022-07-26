<template>
  <div class="github-box">
    <sp-icon name="sp-blog-github" :size="100"></sp-icon>
    <a-spin size="large" style="padding-top: 20px" tip="登录验证中..." />
  </div>
</template>

<script>
import { saveAuth } from '../../lib/login';

export default {
  name: 'github-login',
  mounted() {
    var { code } = this.$route.query;
    var { id } = this.$route.params;

    if (!sp.isNullOrEmpty(id)) {
      this.bindUser(code, id);
      return;
    }

    if (!sp.isNullOrEmpty(code)) {
      this.login(code);
    }
  },
  methods: {
    login(code) {
      var param = {
        third_party_login: {
          param: code,
          type: 0
        }
      };
      sp.post('/api/system/login', param).then(resp => {
        if (resp.result) {
          saveAuth(this.$store, resp);
          this.$router.push({ name: 'index' });
          this.$message.success(resp.message);
        } else {
          this.$message.error(resp.message);
          setTimeout(() => {
            this.$router.push({ name: 'login' });
          }, 2000);
        }
      });
    },
    bindUser(code, id) {
      sp.post(`/api/auth_user/bind?type=0&userid=${id}&code=${code}`)
        .then(() => {
          this.$message.success('绑定成功！');
          this.$router.push({ name: 'index' });
        })
        .catch(err => {
          this.$message.error(err);
        });
    }
  }
};
</script>

<style lang="less" scoped>
.github-box {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}
</style>
