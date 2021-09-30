<template>
  <sp-card title="想法" :loading="loading" :empty="!dataList || dataList.length == 0">
    <a-timeline>
      <a-timeline-item v-for="(item, index) in dataList" :key="index">
        <span>{{ item.createdOn | moment('YYYY-MM-DD HH:mm') }}</span>
        <span v-html="item.content"></span>
      </a-timeline-item>
    </a-timeline>
  </sp-card>
</template>

<script>
import { pagination } from '@/mixins';

export default {
  name: 'idea',
  mixins: [pagination],
  data() {
    return {
      dataList: [],
      controllerName: 'idea',
      pageIndex: 1,
      pageSize: 5,
      loading: false
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      let url = `api/${this.controllerName}/GetViewData?searchList=&orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}`;
      sp.get(url)
        .then(resp => {
          if (resp && resp.DataList) {
            this.dataList = resp.DataList;
            this.total = resp.RecordCount;
          } else {
            this.dataList = resp;
          }
        })
        .catch(error => {
          this.$message.error(error);
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>
