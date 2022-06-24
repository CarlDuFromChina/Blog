<template>
  <a-modal v-model="visible" title="掘金文章同步" destroyOnClose>
    <a-form-model ref="form" :model="data" :rules="rules">
      <a-row>
        <a-col :span="12">
          <a-form-model-item label="分类" prop="category_id">
            <sp-select
              v-model="data.category_id"
              :options="options"
            ></sp-select>
          </a-form-model-item> 
        </a-col>
      </a-row>
    </a-form-model>
    <span slot="footer" class="dialog-footer">
      <a-button @click="visible = false">取 消</a-button>
      <a-button type="primary" @click="handleOk" :loading="loading">确 定</a-button>
    </span>
  </a-modal>
</template>

<script>
export default {
  name: 'juejin-dialog',
  props: ['id'],
  data() {
    return {
      visible: false,
      loading: false,
      data: {
        category_id: ''
      },
      options: [],
      rules: {
        category_id: [{ required: true, message: '请选择分类', trigger: 'blur' }]
      }
    }
  },
  created() {
    sp.get('/api/juejin/categories').then(resp => {
      if (!resp) {
        return [];
      }
      this.options = resp.map(item => {
        return {
          Name: item.category.category_name,
          Value: item.category_id
        }
      })
    })
  },
  methods: {
    handleOk() {
      this.loading = true;
      this.$refs.form.validate(valid => {
        if (valid) {
          this.$confirm({
            title: '博客同步',
            content: '是否同步博客到第三方?',
            okText: '确定',
            cancelText: '取消',
            onOk: () => {
              sp.post(`api/post/sync?id=${this.id}&destination=juejin`, this.data)
                .then(() => {
                  this.$message.success('同步成功');
                })
                .catch(() => {
                  this.$message.error('同步异常');
                })
                .finally(() => {
                  this.visible = false;
                });
            },
            onCancel: () => {
              this.$message.info('已取消');
            }
          });
        }
        this.loading = false;
      });
    }
  }
}
</script>

<style>

</style>