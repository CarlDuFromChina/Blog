# Blog

基于`.Net 6.0`和`Vue 2.6`开发博客`web`系统，支持`PC`端和移动端显示

欢迎访问：[我的博客](https://karldu.cn)

版本变更：[CHANGELOG.md](https://github.com/CarlDuFromChina/Blog/blob/master/CHANGELOG.md)

## 项目特点

+ 支持`PC`和移动端显示
+ 支持第三方登录（`Github`、`Gitee`）快捷注册
+ 采用`Markdown`编辑器，更适合程序员的编辑器
+ 想法和读书笔记采用`wangEditor`富文本编辑器，放飞自我
+ 留言板采用`Disqus`，最流行的评论组件
+ 支持代码高亮和复制，图片预览，深色模式等功能，提升用户体验。
+ 项目采用前后端分离
+ 项目支持`Docker`部署和传统部署（`Windows`、`Linux`）
+ 支持分享到第三方平台（掘金、微信公众号）
+ 支持编辑和同步微信公众号图文素材

## 文档

+ [部署指南](https://karl-du.gitbook.io/sixpence-blog/bu-shu)

  + [Windows 部署](https://karl-du.gitbook.io/sixpence-blog/bu-shu/windows-bu-shu)

  + [Docker 部署](https://karl-du.gitbook.io/sixpence-blog/bu-shu/docker-bu-shu)

+ [版本迁移](https://karl-du.gitbook.io/sixpence-blog/ban-ben)

## 环境配置

项目使用环境配置文件来管理不同环境的配置，请根据以下步骤配置：

### Admin 后台管理

```bash
cd admin
cp .env.example .env
# 编辑 .env 文件，配置对应的环境变量
```

### Portal 门户网站

```bash
cd portal
cp .env.example .env
# 编辑 .env 文件，配置对应的环境变量
```

**注意：** `.env` 文件包含敏感信息，不会被提交到版本控制系统中。

## 截图

首页

![Image](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_index.png)

编辑博客

![Image](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_edit.png)

阅读博客

![Image](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_read.png)

