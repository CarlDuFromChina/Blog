<template>
  <sp-card title="链接">
    <a class="item" v-for="(item, index) in items" :key="index" @click="openLink(item.link)">
      <div class="item-start"><sp-icon :name="item.icon" :size="15" style="padding-right:10px"></sp-icon>{{ item.name }}</div>
      <div class="item-end tag">
        <span>{{ item.linkName }}</span>
      </div>
    </a>
  </sp-card>
</template>

<script>
export default {
  name: 'links',
  data() {
    return {
      items: [
        { name: 'GitHub', link: 'https://github.com/CarlDuFromChina', linkName: 'github.com', icon: 'sp-blog-github' },
        { name: 'CSDN', link: 'https://blog.csdn.net/dumiaoxin?spm=1000.2115.3001.5113', linkName: 'csdn.com', icon: 'sp-blog-csdn' }
      ]
    };
  },
  methods: {
    openLink(link) {
      window.open(link, '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
.item {
  display: flex;
  padding: 10px;
  cursor: pointer;
  align-items: center;
  justify-content: space-between;
  &-start {
    color: #000000;
  }
  &-end {
    color: #4a4a4a;
  }
  .tag {
    align-items: center;
    background-color: whitesmoke;
    border-radius: 4px;
    color: #4a4a4a;
    display: inline-flex;
    font-size: 0.75rem;
    height: 2em;
    justify-content: center;
    line-height: 1.5;
    padding-left: 0.75em;
    padding-right: 0.75em;
    white-space: nowrap;
  }
}
.item:hover {
  background: hsla(0, 0%, 85.1%, 0.1);
}
</style>
