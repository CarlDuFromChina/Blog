export default {
  props: {
    relatedAttr: {
      type: Object
    }
  },
  data() {
    return {
      id: '',
      controllerName: '',
      data: {},
      loading: false,
      privilege: {}
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.data.id = this.relatedAttr.id;
      this.loadData();
    } else if (this.$route.params && this.$route.params.id) {
      this.data.id = this.$route.params.id;
      this.loadData();
    }
    if (!sp.isNullOrEmpty(this.controllerName)) {
      sp.get(`api/${this.controllerName}/privilege`).then(resp => {
        this.privilege = resp;
      });
    }
  },
  computed: {
    pageState() {
      return sp.isNullOrEmpty(this.data.id) ? 'create' : 'edit';
    }
  },
  methods: {
    async loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/${this.data.id}`);
        if (this.loadComplete && typeof this.loadComplete === 'function') {
          await this.loadComplete();
        }
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    async saveData() {
      if (this.preSave && typeof this.preSave === 'function') {
        const result = await this.preSave();
        if (!result) {
          return;
        }
      }
      this.$refs.form.validate(async valid => {
        if (valid) {
          const operateName = sp.isNullOrEmpty(this.data.id) ? 'CreateData' : 'UpdateData';
          try {
            if (operateName === 'CreateData') {
              await sp.post(`api/${this.controllerName}`, this.data);
            } else {
              await sp.put(`api/${this.controllerName}`, this.data);
            }
            this.$message.success(operateName === 'CreateData' ? '添加成功' : '更新成功');
            if (this.postSave && typeof this.postSave === 'function') {
              await this.postSave();
            }
          } catch (error) {
            this.$message.error(error);
          } finally {
            this.$emit('close');
            this.$emit('load-data');
          }
        } else {
          this.$message.error('请检查表单必填项');
        }
      });
    }
  }
};
