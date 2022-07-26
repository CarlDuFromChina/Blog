<template>
  <div id="index" class="index">
    <blog-menu @openUserEdit="() => (this.userInfoEditVisible = true)"></blog-menu>
    <div id="container" v-infinite-scroll="loadMore" :infinite-scroll-distance="10">
      <div class="container">
        <keep-alive>
          <router-view></router-view>
        </keep-alive>
      </div>
    </div>
    <transition name="hehe">
      <div class="ant-back-top" v-if="btnFlag">
        <div class="ant-back-top-content" @click="backTop">
          <div class="ant-back-top-icon"></div>
        </div>
      </div>
    </transition>
    <a-modal v-model="userInfoEditVisible" title="编辑" @ok="saveUserInfo" width="60%" okText="确认" cancelText="取消">
      <user-info-edit ref="userInfoEdit" :related-attr="getUserParam()" @close="userInfoEditVisible = false"></user-info-edit>
    </a-modal>
  </div>
</template>

<script>
import infiniteScroll from 'vue-infinite-scroll';
import userInfoEdit from '../admin/core/userInfo/userInfoEdit.vue';
import blogMenu from './blogMenu.vue';

export default {
  name: 'index',
  directives: { infiniteScroll },
  components: { userInfoEdit, blogMenu },
  data() {
    return {
      bottom: 100,
      scrollTop: 0,
      btnFlag: false,
      activeIndex: '1',
      userInfoEditVisible: false
    };
  },
  created() {
    if (this.isLoggedIn) {
      sp.get('api/user_info/is_incomplete').then(resp => {
        if (resp) {
          const key = `open${Date.now()}`;
          this.$notification.open({
            message: '系统提醒',
            description: '请完善个人信息',
            icon: <a-icon type="info-circle" style="color: #108ee9" />,
            key,
            btn: h => {
              return h(
                'a-button',
                {
                  props: {
                    type: 'primary',
                    size: 'small'
                  },
                  on: {
                    click: () => {
                      this.$notification.close(key);
                      this.userInfoEditVisible = true;
                    }
                  }
                },
                '立即前往'
              );
            }
          });
        }
      });
    }
    sp.get('api/sys_config/is_enable_comment').then(resp => {
      this.$store.commit('updateShowComment', resp);
    });
  },
  mounted() {
    window.addEventListener('scroll', this.scrollToTop, true);
  },
  destroyed() {
    window.removeEventListener('scroll', this.scrollToTop, true);
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    },
    userName() {
      return this.$store.getters.userName;
    }
  },
  methods: {
    // 点击图片回到顶部方法，加计时器是为了过渡顺滑
    backTop() {
      let timer = setInterval(() => {
        let ispeed = Math.floor(-this.scrollTop / 5);
        document.querySelector('.container').scrollTop = document.documentElement.scrollTop = document.body.scrollTop = this.scrollTop + ispeed;
        if (this.scrollTop === 0) {
          clearInterval(timer);
        }
      }, 16);
    },
    // 为了计算距离顶部的高度，当高度大于60显示回顶部图标，小于60则隐藏
    scrollToTop() {
      let scrollTop =
        window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || document.querySelector('.container').scrollTop;
      this.scrollTop = scrollTop;
      this.$bus.$emit('scroll', scrollTop);
      if (this.scrollTop > 300) {
        this.btnFlag = true;
      } else {
        this.btnFlag = false;
      }
    },
    loadMore() {
      this.$bus.$emit('load-more');
    },
    saveUserInfo() {
      this.$refs.userInfoEdit.saveData();
    },
    getUserParam() {
      return {
        id: sp.getUserId()
      };
    }
  }
};
</script>

<style lang="less" scoped>
.hehe-enter,
.hehe-leave-to {
  opacity: 0;
}
.hehe-enter-to,
.hehe-leave {
  opacity: 1;
}
.hehe-enter-active,
.hehe-leave-active {
  transition: all 1s;
}
.index {
  background: #edeef2;
  height: 100%;
}
.container {
  max-width: 60%;
  margin: 10px auto 0;
  padding: 0 10px 0px 10px;
}
</style>
