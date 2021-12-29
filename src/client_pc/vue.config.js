const path = require('path');
const CompressionPlugin = require("compression-webpack-plugin")

function resolve(dir) {
  return path.join(__dirname, '.', dir);
}

var isProd = process.env.NODE_ENV === 'production';
const cdn = {
  css: [
    'https://lib.baomitu.com/ant-design-vue/1.7.6/antd.min.css'
  ],
  js: [
    'https://cdn.bootcdn.net/ajax/libs/moment.js/2.27.0/moment.min.js',
    'https://cdn.bootcdn.net/ajax/libs/moment.js/2.27.0/locale/zh-cn.js',
    'https://cdn.bootcdn.net/ajax/libs/marked/2.1.3/marked.min.js',
    'https://lib.baomitu.com/echarts/5.0.2/echarts.min.js',
    'https://cdn.jsdelivr.net/npm/wangeditor@4.7.8/dist/wangEditor.min.js',
    'https://lib.baomitu.com/vue/2.6.14/vue.js',
    'https://lib.baomitu.com/vue-router/3.5.2/vue-router.min.js',
    'https://lib.baomitu.com/vuex/3.6.2/vuex.min.js',
    'https://lib.baomitu.com/ant-design-vue/1.7.6/antd.min.js'
  ],
  externals: {
    moment: 'moment',
    marked: 'marked',
    echarts: 'echarts',
    wangeditor: 'wangEditor',
    vue: 'Vue',
    'vue-router': 'VueRouter',
    'vuex':'Vuex',
    'ant-design-vue': 'antd'
  }
};

module.exports = {
  runtimeCompiler: true,
  pluginOptions: {
    'style-resources-loader': {
      preProcessor: 'less',
      patterns: [
        // 这个是加上自己的路径,不能使用(如下:alias)中配置的别名路径
        path.resolve(__dirname, './src/style/index.less')
      ]
    }
  },
  configureWebpack: {
    externals: isProd ? cdn.externals : [],
    plugins: [
      new CompressionPlugin({
        test:/\.js$|\.html$|.\css/, //匹配文件名
        threshold: 10240,//对超过10k的数据压缩
        deleteOriginalAssets: false //不删除源文件
      })
    ]
  },
  chainWebpack: config => {
    if (isProd) {
      config.plugin('html').tap(args => {
        args[0].cdn = cdn;
        args[0].title = process.env.VUE_APP_TITLE;
        return args;
      });  
    }
    config.module.rules.delete('svg');
    config.module
      .rule('svg-sprite-loader')
      .test(/\.svg$/)
      .include.add(resolve('src/assets/icons')) //处理svg目录
      .end()
      .use('svg-sprite-loader')
      .loader('svg-sprite-loader')
      .options({
        symbolId: 'sp-blog-[name]'
      });
    config.module
      .rule('images')
      .test(/\.(png|jpe?g|gif|webp|svg)(\?.*)?$/)
      .use('url-loader')
      .loader('url-loader')
      .tap(options => Object.assign(options, { limit: 10240 }));
  }
};
