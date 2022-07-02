/**
 * 模块说明
 * @module 草稿
 * @author Carl Du
 * @description 博客编辑页添加草稿功能
 */

export default {
  data() {
    return {
      draft: {},
      isDirty: false,
      seconds: 60,
      configCode: 'enable_draft',
      saveStatusValue: '',
      unwatch: null,
      statusList: [
        {
          value: 'wait',
          icon: 'redo',
          text: '60秒后备份',
          color: '#000000a6'
        },
        {
          value: 'success',
          icon: 'check-circle',
          text: '草稿保存成功',
          color: '#52c41a'
        },
        {
          value: 'fail',
          icon: 'close-circle',
          text: '草稿保存失败',
          color: '#ff4d4f'
        }
      ]
    };
  },
  created() {
    this.$on('open-watch', () => this.openWatch());
  },
  beforeDestroy() {
    this.closeWatch();
    window.clearInterval(this.secondId); // 销毁倒计时事件
  },
  async mounted() {
    const enable = await sp.get(`/api/sys_config/value?code=${this.configCode}`);

    if (enable === 'true' || enable === true) { 
      // 1、从草稿列表页进入编辑页面
      // 2、新创建的博客
      // 3、编辑博客
      if (!sp.isNullOrEmpty(this.$route.params.draftId)) {
        await this.popDraft(this.$route.params.draftId); // 打开草稿
        this.$emit('open-watch');
      } else if (this.pageState === 'create') {
        this.$emit('open-watch');
      } else {
        await this.getDraft(); // 获取草稿
      }
    }
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
    async popDraft(id) {
      return sp.get(`api/draft/${id}`).then(resp => {
        this.draft = resp;
        const { content, title } = resp;
        this.data.id = id;
        this.data.content = content;
        this.data.title = title;
      });
    },
    /**
     * 获取草稿
     **/
    async getDraft() {
      return sp.get(`api/draft/post/${this.data.id}`).then(resp => {
        if (resp) {
          this.draft = resp;
          this.$confirm({
            title: '是否恢复?',
            content: '发现您尚未保存该博客，是否恢复上次备份内容？',
            okText: '恢复',
            cancelText: '取消',
            onOk: () => {
              const { postid, content, title } = resp;
              this.data.id = postid;
              this.data.content = content;
              this.data.title = title;
              sp.delete(`/api/draft/${this.draft.id}`)
                .finally(() => {
                  this.$emit('open-watch');
                });
            },
            onCancel: () => {
              sp.delete(`/api/draft/${this.draft.id}`).then(() => {
                this.$message.info('已删除草稿');
              })
                .finally(() => {
                  this.$emit('open-watch');
                });
            }
          });
        } else {
          this.$emit('open-watch');
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
      if (!sp.isNullOrEmpty(this.data.id)) {
        this.draft.postid = this.data.id;
      }
      sp.post('api/draft/save', this.draft)
        .then(resp => {
          this.saveStatusValue = 'success';
          this.isDirty = false;
          this.draft.id = resp;
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
      this.unwatch = this.$watch(
        'data',
        () => {
          // 倒计时保存草稿
          if (!this.isDirty) {
            this.isDirty = true;
            this.saveStatusValue = 'wait';
            this.secondId = setInterval(() => {
              if (this.seconds === 0) {
                this.saveDraft();
              } else {
                this.seconds -= 1;
                this.statusList[0].text = `${this.seconds}秒后备份`;
              }
            }, 1000);
          }
        },
        {
          deep: true
        }
      );
    },
    closeWatch() {
      if (this.unwatch && typeof this.unwatch === 'function') {
        this.unwatch();
      }
    },
    /**
     * 返回前检查
     */
    preBack() {
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
          this.editVisible = true;
          this.$nextTick(() => this.save());
        },
        onCancel: () => {
          this.$router.back();
        }
      });
    }
  }
};
