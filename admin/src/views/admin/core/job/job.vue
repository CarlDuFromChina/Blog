<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :customApi="customApi"
    :columns="columns"
    :use-pagination="false"
    :use-header-click="false"
  >
    <a-modal v-model="editVisible" title="记录" width="60%" okText="确认" cancelText="取消">
      <job-history ref="history"></job-history>
    </a-modal>
  </sp-list>
</template>

<script>
import jobHistory from "./jobHistory.vue";

export default {
  name: 'job',
  components: { jobHistory },
  data() {
    return {
      controllerName: 'job',
      editVisible: false,
      searchList: [],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'status', label: '状态' },
        { prop: 'prev_fire_time', label: '上次运行时间', type: 'datetime' },
        { prop: 'next_fire_time', label: '下次运行时间', type: 'datetime' },
        { prop: 'cron_expression', label: '执行计划' },
        { prop: 'description', label: '描述' },
        {
          prop: 'action',
          label: '操作',
          type: 'actions',
          actions: [
            { name: '运行', size: 'small', method: this.start, type: 'primary' },
            { name: '暂停', size: 'small', method: this.pause, type: 'danger' },
            { name: '继续', size: 'small', method: this.resume },
            { name: '查看', size: 'small', method: (row) => this.showDetail(row) }
          ]
        }
      ]
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}`;
    }
  },
  methods: {
    start(row) {
      this.$confirm({
        title: '提示',
        content: '是否确认运行该作业?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.get(`api/${this.controllerName}/run?name=${row.name}`)
            .then(() => {
              this.$refs.list.loadData();
              this.$message.success('执行成功');
            })
            .catch(error => {
              this.$message.error(error);
              return Promise.reject;
            });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    },
    pause(row) {
      sp.get(`api/${this.controllerName}/pause?name=${row.name}`)
        .then(() => {
          this.$refs.list.loadData();
          this.$message.success('执行成功');
        })
        .catch(error => {
          this.$message.error(error);
          return Promise.reject;
        });
    },
    resume(row) {
      sp.get(`api/${this.controllerName}/resume?name=${row.name}`)
        .then(() => {
          this.$refs.list.loadData();
          this.$message.success('执行成功');
        })
        .catch(error => {
          this.$message.error(error);
          return Promise.reject;
        });
    },
    async showDetail(row) {
      this.editVisible = true;
      await this.$nextTick();
      const historyRef = this.$refs.history;
      historyRef.searchList = [{ Name: 'job_name', Value: row.name, Type: 0 }];
      await this.$nextTick();
      historyRef.loadData();
    }
  }
};
</script>

<style></style>
