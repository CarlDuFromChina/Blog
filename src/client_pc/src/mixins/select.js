export default {
  data() {
    return {
      selectParamNameList: [],
      selectEntityNameList: [],
      selectDataList: {}
    };
  },
  created() {
    this.loadSelectDataList();
  },
  methods: {
    async loadSelectDataList() {
      // 普通选项集
      if (!sp.isNullOrEmpty(this.selectParamNameList)) {
        const resp1 = await sp.get(`api/sys_param_group/options?code=${this.selectParamNameList.join(',')}`);
        this.selectParamNameList.forEach((item, index) => {
          this.$set(this.selectDataList, item, resp1[index]);
        });
      }

      // 实体选项集
      if (!sp.isNullOrEmpty(this.selectEntityNameList)) {
        const resp2 = await sp.get(`api/sys_param_group/entity_options?code=${this.selectEntityNameList.join(',')}`);
        this.selectEntityNameList.forEach((item, index) => {
          this.$set(this.selectDataList, item, resp2[index]);
        });
      }

      if (this.loadSelectDataListComplete && typeof this.loadSelectDataListComplete === 'function') {
        this.loadSelectDataListComplete();
      }
    }
  }
};
