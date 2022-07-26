<template>
  <sp-card v-if="showUser" :loading="loading">
    <div class="about">
      <img :src="lifePhoto" alt="" />
      <p>{{ name }}</p>
      <p>{{ introduction }}</p>
    </div>
  </sp-card>
</template>

<script>
export default {
  name: 'me',
  data() {
    return {
      showUser: true,
      lifePhoto: require('../../../assets/images/pic_err.png'),
      name: '',
      introduction: '',
      loading: true
    };
  },
  async created() {
    var user = await sp.get('api/post/index_user');
    if (user) {
      if (user.life_photo) {
        this.lifePhoto = sp.getDownloadUrl(user.life_photo, false);
      }
      this.introduction = user.introduction;
      this.name = user.name;
    } else {
      this.showUser = false;
    }
    setTimeout(() => {
      this.loading = false;
    }, 200);
  },
  methods: {}
};
</script>

<style lang="less" scoped>
@import url('./card');

.about {
  img {
    width: 100%;
    margin-bottom: 24px;
    border-radius: 5px;
  }
  p {
    color: #bfbfbf;
    font-size: 14px;
    line-height: 1.2;
  }
}
</style>
