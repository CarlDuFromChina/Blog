<template>
  <sp-list
    ref="list"
    :controller-name="controllerName"
    :columns="columns"
    :use-header-click="false"
    :customApi="customApi"
    :autoLoad="false"
  ></sp-list>
</template>

<script>
export default {
  name: 'job-history',
  data() {
    return {
      controllerName: 'job_history',
      columns: [
        { prop: 'job_name', label: '名称' },
        { prop: 'status', label: '状态' },
        { prop: 'start_time', label: '开始时间', type: 'datetime' },
        { prop: 'end_time', label: '结束时间', type: 'datetime' },
        { prop: 'error_msg', label: '错误原因' }
      ],
      searchList: []
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/search?searchList=${JSON.stringify(this.searchList)}&orderBy=created_at desc`;
    }
  },
  methods: {
    loadData() {
      this.$refs.list.loadData();
    }
  }
};
</script>

<style></style>
