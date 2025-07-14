<template>
  <div class="blog blog__readonly">
    <blog-menu></blog-menu>
    <div class="blog-body" style="background-color:#e9ecef">
      <div class="bodyWrapper">
        <sp-card :loading="loading">
          <div slot="title" class="bodyWrapper-title">{{ data.name }}</div>
          <div id="content" v-show="!loading"></div>
        </sp-card>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'reading-note',
  data() {
    return {
      id: this.$route.params.id,
      controllerName: 'reading_note',
      data: {},
      loading: false,
      height: null,
    };
  },
  async mounted() {
    await this.loadData();
    
  },
  methods: {
    async loadData() {
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/${this.id}`);
      } finally {
        setTimeout(() => {
          this.loading = false;
          this.$nextTick(() => {
            document.getElementById('content').innerHTML = this.data.content;
          })
        }, 200);
      }
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-layout-sider-light {
  background: #e9ecef;
}

.blog {
  height: 100%;
  &.blog__readonly {
    overflow-y: auto;
    overflow-x: hidden;
    .blog-header {
      width: 100%;
      height: 60px;
      display: inline-block;
      line-height: 60px;
      padding-left: 20px;
    }
    .blog-body {
      background-color: #e9ecef;
      color: #212529;
      padding-top: 24px;
      padding-bottom: 40px;
      .bodyWrapper {
        width: 50%; 
        min-height: 800px;
        margin: 0 auto;
        .ant-layout {
          background: transparent;
        }
        .bodyWrapper-title {
          font-size: 2.5rem;
          text-align: left;
          font-weight: 600;
          color: #000000d9;
        }
        &-background {
          max-width: 100%;
          max-width: 100%;
          width: 100%;
          height: 100%;
          margin-bottom: 20px;
        }
        .bodyWrapper-content {
          height: 100%;
          min-height: 1000px;
          img {
            max-width: 100%;
          }
        }
      }
    }
  }
}

#content {
  min-height: 600px;
  padding-top: 50px;
}
</style>
