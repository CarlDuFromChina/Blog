<template>
  <el-form ref="form" :model="data" label-width="80px" :rules="rules">
    <el-row>
      <el-col :span="12">
        <el-form-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="编码" prop="code">
          <el-input v-model="data.code"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">字段</el-divider>
    <el-button size="mini" type="primary" style="margin-left:20px" @click="editVisible = true">新增</el-button>
    <el-button size="mini" type="success" style="margin-left:20px" @click="publishEntity">发布</el-button>
    <el-table :data="attrs" style="width: 100%;padding: 0 20px 40px 20px">
      <el-table-column label="名称" prop="name"> </el-table-column>
      <el-table-column label="类型" prop="attr_type"> </el-table-column>
      <el-table-column label="长度" prop="attr_length"> </el-table-column>
      <el-table-column label="必填" prop="isrequire">
        <template slot-scope="scope">
          <span>{{ scope.row.isrequire === 1 ? '是' : '否' }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="saveData">确 定</el-button>
        </span>
      </el-col>
    </el-row>
    <el-dialog title="编辑" :visible.sync="editVisible" width="60%" append-to-body>
      <sys-attrs-edit :parent="parentObj" @close="handleClose"></sys-attrs-edit>
    </el-dialog>
  </el-form>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';
import sysAttrsEdit from './sysAttrsEdit';

export default {
  name: 'sysEntityEdit',
  components: { sysAttrsEdit },
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysEntity',
      attrs: [],
      editVisible: false,
      rules: {
        name: [{ required: true, message: '请输入实体名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }]
      }
    };
  },
  computed: {
    parentObj() {
      return {
        id: this.Id,
        name: this.data.name
      };
    }
  },
  methods: {
    handleClose() {
      this.editVisible = false;
      this.loadAttrs();
    },
    handleDelete(index, row) {
      const id = row.sys_attrsId;
      sp.post('api/SysAttrs/DeleteData', [id]).then(resp => {
        this.$message.success('删除成功');
        this.loadAttrs();
      });
    },
    loadComplete() {
      this.loadAttrs();
    },
    loadAttrs() {
      sp.get(`api/SysEntity/GetEntityAttrs?id=${this.Id}`).then(resp => {
        this.attrs = resp;
      });
    },
    publishEntity() {
      if (sp.isNullOrEmpty(this.Id)) {
        this.$message.error('请保存实体后再发布');
        return;
      }
      this.$confirm('此操作将修改实体, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          this.$message.success('发布成功');
        })
        .catch(() => {
          this.$message.info('已取消');
        });
    }
  }
};
</script>

<style></style>
