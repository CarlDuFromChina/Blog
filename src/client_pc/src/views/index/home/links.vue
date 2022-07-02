<template>
  <sp-card title="有趣的项目" :empty="!items || items.length == 0" :loading="loading">
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
      items: [],
      loading: true
    };
  },
  created() {
    sp.get('api/link/search').then(resp => {
      this.items = resp.DataList;
      setTimeout(() => {
        this.loading = false;
      }, 200);
    });
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
