<template>
  <el-form ref="form" :model="data" label-width="80px">
    <el-row>
      <el-col :span="12">
        <el-form-item label="名称">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="编码">
          <el-input v-model="data.code"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">字段</el-divider>
    <el-button size="mini" type="primary" style="margin-left:20px" @click="editVisible = true">新增</el-button>
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
      <sys-attrs-edit :parentId="Id"></sys-attrs-edit>
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
      editVisible: false
    };
  },
  methods: {
    handleDelete(index, row) {
      console.log(index, row);
    },
    async loadComplete() {
      this.attrs = await sp.get(`api/SysEntity/GetEntityAttrs?id=${this.Id}`);
    }
  }
};
</script>

<style></style>
