/**
 * 模块说明
 * @module 草稿
 * @author Carl Du
 * @description 博客编辑页添加草稿功能
 */

export default {
  data() {
    return {
      draftId: '',
      draft: {},
      isDirty: false,
      seconds: 60
    };
  },
  mounted() {
    if (!sp.isNullOrEmpty(this.$route.params.draftId)) {
      this.popDraft(this.$route.params.draftId); // 打开草稿
    } else if (this.pageState === 'create') {
      this.draft.blogId = sp.newUUID();
      this.draft.draftId = sp.newUUID();
      this.openWatch(); // 打开监听
    } else {
      this.getDraft(); // 获取草稿
    }
  },
  beforeDestroy() {
    window.clearInterval(this.secondId); // 销毁倒计时事件
  },
  computed: {
    showAutoSave() {
      return this.isDirty;
    }
  },
  methods: {
    /**
     * 打开草稿
     * @param {String} id - 博客id
     */
    popDraft(id) {
      sp.get(`api/Draft/GetDataByBlogId?id=${id}`).then(resp => {
        this.draft = resp;
        const { blogId, content, title } = resp;
        this.data.blogId = blogId;
        this.data.content = content;
        this.data.title = title;
      });
    },
    /**
     * 获取草稿
     **/
    getDraft() {
      sp.get(`api/Draft/GetDataByBlogId?id=${this.Id}`).then(resp => {
        if (!sp.isNull(resp)) {
          this.draft = resp;
          this.$confirm('发现您尚未保存该博客，是否恢复上次备份内容?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          })
            .then(() => {
              const { blogId, content, title } = resp;
              this.data.blogId = blogId;
              this.data.content = content;
              this.data.title = title;
              sp.post('api/Draft/DeleteData', [this.draft.Id]); // 删除草稿
            })
            .catch(() => {
              this.$message({
                type: 'info',
                message: '已取消删除'
              });
            });
        } else {
          this.draft.draftId = sp.newUUID();
          this.draft.blogId = this.Id;
        }
      });
    },
    /**
     * 保存草稿
     */
    saveDraft() {
      this.draft.title = this.data.title;
      this.draft.content = this.data.content;
      this.draft.images = this.data.images;
      sp.post('api/Draft/CreateOrUpdateData', this.draft).then(() => {
        this.$message('草稿保存成功');
        this.seconds = 60;
      });
    },
    /**
     * 监听页面是否修改
     */
    openWatch() {
      this.$watch('data', () => {
        // 倒计时保存草稿
        if (!this.isDirty) {
          this.isDirty = true;
          this.secondId = setInterval(() => {
            if (this.seconds === 0) {
              this.saveDraft();
            } else {
              this.seconds -= 1;
            }
          }, 1000);
        }
      }, {
        deep: true
      });
    },
    /**
     * 返回前检查
     * @param {Function} giveup - 回调
     * @param {Function} save - 保存
     */
    preBack(giveup, save) {
      if (!this.isDirty) {
        giveup();
        return;
      }
      this.$confirm('检测到未保存的内容，是否在离开页面前保存修改？', '确认信息', {
        distinguishCancelAndClose: true,
        confirmButtonText: '保存',
        cancelButtonText: '放弃修改'
      })
        .then(() => {
          save();
        })
        .catch(action => {
          this.$message({
            type: 'info',
            message: action === 'cancel'
              ? '放弃保存并离开页面'
              : '停留在当前页面'
          });
          giveup();
        });
    }
  }
};