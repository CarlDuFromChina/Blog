<template>
  <a-form-model ref="form" :model="data" label-width="80px" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="编码" prop="code">
          <a-input v-model="data.code" :disabled="pageState == 'edit'"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="是否系统实体">
          <a-switch v-model="data.is_sys"></a-switch>
        </a-form-model-item>
      </a-col>
    </a-row>
    <template v-if="pageState == 'edit'">
      <a-button type="primary" @click="editVisible = true">新增</a-button>
      <a-button type="primary" style="margin-left: 20px" @click="exportData">导出</a-button>
      <a-table :columns="columns" :data-source="attrs" style="padding-top:20px">
        <span slot="isrequire" slot-scope="text">{{ text == '1' ? '是' : '否' }}</span>
        <span slot="action" slot-scope="text, record">
          <a-button size="small" type="danger" @click="handleDelete(record)">删除</a-button>
        </span>
      </a-table>
    </template>
    <a-modal title="编辑" v-model="editVisible" width="60%" @ok="saveAttr" okText="确认" cancelText="取消">
      <sys-attrs-edit ref="attrEdit" :parent="parentObj" @close="handleClose"></sys-attrs-edit>
    </a-modal>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';
import sysAttrsEdit from './sysAttrsEdit';

const columns = [
  {
    title: '名称',
    dataIndex: 'name'
  },
  {
    title: '编码',
    dataIndex: 'code'
  },
  {
    title: '类型',
    dataIndex: 'attr_type'
  },
  {
    title: '长度',
    dataIndex: 'attr_length'
  },
  {
    title: '描述',
    dataIndex: 'description'
  },
  {
    title: '必填',
    dataIndex: 'isrequire',
    scopedSlots: {
      customRender: 'isrequire'
    }
  },
  {
    title: '操作',
    key: 'action',
    scopedSlots: { customRender: 'action' }
  }
];
export default {
  name: 'sysEntityEdit',
  components: { sysAttrsEdit },
  mixins: [edit],
  data() {
    return {
      controllerName: 'sys_entity',
      attrs: [],
      editVisible: false,
      rules: {
        name: [{ required: true, message: '请输入实体名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }]
      },
      columns
    };
  },
  computed: {
    parentObj() {
      return {
        id: this.data.id,
        name: this.data.name,
        entityCode: this.data.code
      };
    }
  },
  methods: {
    exportData() {
      sp.get(`api/sys_entity/export?entityid=${this.data.id}`);
    },
    handleClose() {
      this.editVisible = false;
      this.loadAttrs();
    },
    handleDelete(row) {
      const id = row.sys_attrsId;
      sp.delete(`api/sys_attrs/${id}`).then(() => {
        this.$message.success('删除成功');
        this.loadAttrs();
      });
    },
    saveAttr() {
      this.$refs.attrEdit.saveData();
    },
    loadComplete() {
      this.loadAttrs();
    },
    loadAttrs() {
      sp.get(`api/sys_entity/attrs?id=${this.data.id}`).then(resp => {
        this.attrs = resp;
      });
    }
  }
};
</script>

<style></style>
