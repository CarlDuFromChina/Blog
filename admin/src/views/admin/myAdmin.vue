<template>
  <admin ref="admin">
    <div class="admin-nav-actions">
      <sp-icon name="sp-blog-tools" :size="18" tooltip="网页在线小工具" @click="openExternalLink('OnlineTool')"></sp-icon>
      <sp-icon name="sp-blog-wechat" :size="18" tooltip="微信平台" @click="openExternalLink('WeChatPlatform')"></sp-icon>
      <sp-icon name="sp-blog-extricator" :size="18" tooltip="Extricator" @click="openExternalLink('Extricator')"></sp-icon>
      <sp-icon name="sp-blog-portainer" :size="18" tooltip="Portainer" @click="openExternalLink('Portainer')"></sp-icon>
    </div>
    <!-- 悬浮菜单 -->
    <div class="hover-menu">
      <div class="button-box">
        <a-button size="large" icon="home" shape="circle" class="green-btn" @click="goIndex"></a-button>
      </div>
      <div class="button-box">
        <a-dropdown>
          <a-menu slot="overlay">
            <a-menu-item key="1" @click="$router.push({ name: 'idea' })" v-show="ideaPrivilege.create">新建想法</a-menu-item>
            <a-menu-item key="2" @click="$router.push({ name: 'readingNoteEdit' })" v-show="readingNotePrivilege.create">新建读书笔记</a-menu-item>
            <a-menu-item key="3" @click="$router.push({ name: 'postEdit' })" v-show="blogPrivilege.create">新建博客</a-menu-item>
          </a-menu>
          <a-button type="primary" size="large" icon="edit" shape="circle"></a-button>
        </a-dropdown>
      </div>
    </div>
    <!-- 悬浮菜单 -->
  </admin>
</template>

<script>
import { admin } from './core';

export default {
  name: 'myAdmin',
  components: { admin },
  data() {
    return {
      blogPrivilege: {},
      ideaPrivilege: {},
      readingNotePrivilege: {},
      sites: {}
    };
  },
  created() {
    sp.get('api/post/privilege').then(resp => this.blogPrivilege = resp);
    sp.get('api/idea/privilege').then(resp => this.ideaPrivilege = resp);
    sp.get('api/reading_note/privilege').then(resp => this.readingNotePrivilege = resp);
    sp.get('api/external_site/sites').then(resp => {
      resp.forEach(item => {
        this.sites[item.Name] = item.Value;
      })
    });
  },
  methods: {
    handleClick(cmd) {
      if (cmd && typeof cmd === 'function') {
        cmd();
      }
    },
    goIndex() {
      window.open(process.env.VUE_APP_INDEX_URL, '_blank');
    },
    openExternalLink(name) {
      if (this.sites[name])
        window.open(this.sites[name], '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
.hover-menu {
  position: absolute;
  bottom: 50px;
  right: 50px;
  z-index: 10;

  .button-box {
    height: 40px;
    margin-bottom: 8px;
  }
  .green-btn {
    color: #fff;
    background-color: #52c41a;
    border-color: #52c41a;
  }
}
</style>
