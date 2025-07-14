const path = require('path');
const CompressionPlugin = require("compression-webpack-plugin")

function resolve(dir) {
  return path.join(__dirname, '.', dir);
}

var isProd = process.env.NODE_ENV === 'production';
const cdn = {
  css: [
    'https://gcore.jsdelivr.net/npm/ant-design-vue@1.7.8/dist/antd.min.css'
  ],
  js: [
    'https://gcore.jsdelivr.net/npm/moment@2.29.1/moment.min.js',
    'https://gcore.jsdelivr.net/npm/marked@2.1.3/marked.min.js',
    'https://gcore.jsdelivr.net/npm/vue@2.6.14/dist/vue.min.js',
    'https://gcore.jsdelivr.net/npm/vue-router@3.5.3/dist/vue-router.min.js',
    'https://gcore.jsdelivr.net/npm/vuex@3.6.2/dist/vuex.min.js',
    'https://gcore.jsdelivr.net/npm/ant-design-vue@1.7.8/dist/antd.min.js',
  ],
  externals: {
    moment: 'moment',
    marked: 'marked',
    vue: 'Vue',
    'vue-router': 'VueRouter',
    'vuex':'Vuex',
    'ant-design-vue': 'antd',
  }
};

module.exports = {
  runtimeCompiler: true,
  lintOnSave: false,
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
    output: {
      filename: '[name].[hash].js'
    },
    externals: isProd ? cdn.externals : [],
    resolve: {
      alias: {
        '@': resolve('src')
      }
    },
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
    config.module
      .rule('svg')
      .exclude.add(resolve('src/assets/icons'))
      .end();
    
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
  }
};
