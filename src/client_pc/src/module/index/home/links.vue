<template>
  <sp-card title="链接" :empty="!items || items.length == 0">
    <a class="item" v-for="(item, index) in items" :key="index" @click="openLink(item.link_url)">
      <div class="item-start"><sp-icon :name="getIcon(item.link_type)" :size="15" style="padding-right:10px"></sp-icon>{{ item.name }}</div>
      <div class="item-end tag" v-if="item.brief">
        <a-tooltip>
          <template slot="title">
            {{ item.brief }}
          </template>
          <span class="item-brief">{{ item.brief }}</span>
        </a-tooltip>
      </div>
    </a>
  </sp-card>
</template>

<script>
export default {
  name: 'links',
  data() {
    return {
      items: []
    };
  },
  created() {
    sp.get('api/Link/GetDataList').then(resp => (this.items = resp));
  },
  methods: {
    getIcon(type) {
      const mapper = { github: 'sp-blog-github' };
      return mapper[type];
    },
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
  &-brief {
    width: 180px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
}
.item:hover {
  background: hsla(0, 0%, 85.1%, 0.1);
}
</style>
