<template>
  <sp-card title="有趣的项目" :empty="!items || items.length == 0">
    <a class="item" v-for="(item, index) in items" :key="index" @click="openLink(item.link_url)">
      <div class="item-start">
        <sp-icon :name="getIcon(item.link_type)" :size="15" style="padding-right:10px"></sp-icon>
        <a-tooltip>
          <template slot="title">
            {{ item.brief }}
          </template>
          {{ item.name }}
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
@import url('./card');
</style>
