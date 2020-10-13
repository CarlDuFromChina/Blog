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
      seconds: 60,
      configCode: 'enable_draft',
      saveStatusValue: '',
      statusList: [{
        value: 'wait',
        icon: 'redo',
        text: '60秒后备份',
        color: '#000000a6'
      }, {
        value: 'success',
        icon: 'check-circle',
        text: '草稿保存成功',
        color: '#52c41a'
      }, {
        value: 'fail',
        icon: 'close-circle',
        text: '草稿保存失败',
        color: '#ff4d4f'
      }]
    };
  },
  async mounted() {
    const enable = await sp.get(`/api/SysConfig/GetValue?code=${this.configCode}`);
    if (enable !== 'true') {
      return Promise.resolve(true);
    }
    if (!sp.isNullOrEmpty(this.$route.params.draftId)) {
      this.popDraft(this.$route.params.draftId); // 打开草稿
    } else if (this.pageState === 'create') {
      this.draft.blogId = sp.newUUID();
      this.draft.draftId = sp.newUUID();
      setTimeout(() => {
        this.openWatch(); // 打开监听
      }, 200);
    } else {
      this.getDraft(); // 获取草稿
    }
  },
  beforeDestroy() {
    window.clearInterval(this.secondId); // 销毁倒计时事件
  },
  computed: {
    showAutoSave() {
      return !!this.saveStatusValue;
    },
    saveStatus() {
      return this.statusList.find(item => item.value === this.saveStatusValue);
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
          this.$confirm({
            title: '是否恢复?',
            content: '发现您尚未保存该博客，是否恢复上次备份内容？',
            okText: '恢复',
            cancelText: '取消',
            onOk: () => {
              const { blogId, content, title } = resp;
              this.data.blogId = blogId;
              this.data.content = content;
              this.data.title = title;
              sp.post('api/Draft/DeleteData', [this.draft.Id]); // 删除草稿
            },
            onCancel: () => {
              sp.post('api/Draft/DeleteData', [this.draft.Id]).then(() => {
                this.$message({
                  type: 'info',
                  message: '已删除草稿'
                });
              });
            }
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
      this.draft.title = this.data.title || '草稿';
      this.draft.content = this.data.content;
      this.draft.images = this.data.images;
      sp.post('api/Draft/CreateOrUpdateData', this.draft).then(() => {
        this.saveStatusValue = 'success';
        this.isDirty = false;
      })
        .catch(() => {
          this.saveStatusValue = 'fail';
        })
        .finally(() => {
          this.seconds = 60;
          clearInterval(this.secondId);
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
          this.saveStatusValue = 'wait';
          alert('执行1');
          this.secondId = setInterval(() => {
            if (this.seconds === 0) {
              this.saveDraft();
            } else {
              this.seconds -= 1;
              this.statusList[0].text = `${this.seconds}秒后备份`;
            }
          }, 1000);
        }
      }, {
        deep: true
      });
    },
    /**
     * 返回前检查
     * @param {Function} save - 保存
     */
    preBack(save) {
      if (!this.isDirty) {
        this.$router.back();
        return;
      }
      this.$confirm({
        title: '是否保存修改?',
        content: '检测到未保存的内容，是否在离开页面前保存修改？',
        okText: '保存',
        cancelText: '取消',
        onOk: () => {
          save();
        },
        onCancel: () => {
          this.$router.back();
        }
      });
    }
  }
};
