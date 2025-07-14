# Blog Admin

> 一个基于 Vue2.6 的博客后台管理项目，支持`Docker`镜像跨平台部署

主仓库地址：[Sixpence Blog](https://github.com/CarlDuFromChina/Blog)

## 框架介绍

主框架版本：

+ vue@2.6.14
+ @vue/cli-service@5.0.8

开发环境：

+ nodejs@18.13.0
+ webpack@5.44.0
+ webpack-cli@4.7.2
+ webpack-dev-server@3.11.2

## 功能介绍

+ 第三方登录（GitHub、Gitee）
+ 仪表盘
+ 创作平台
  - 系列管理
  - 文章管理
  - 专栏管理
  - 草稿管理
  - 想法管理
  - 读书笔记管理
  - 推荐信息管理
  - 链接信息
+ 微信平台
  - 图文素材库
  - 素材库
  - 自动回复
+ 资源管理
  - 文件管理
  - 图库
+ 系统管理
  - 菜单管理
  - 实体管理
  - 作业管理
  - 用户信息
  - 选项集
  - 系统参数
  - 角色管理

## 启动项目

``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev
```

## Docker 发布和部署

### 发布

```bash
docker build -f ".\Dockerfile" -t carldu/blog-html-admin:latest .
docker push carldu/blog-html-admin:latest
```

### 部署

快速部署：

```bash
docker run -p 80:80 --name blog-ui -d carldu/blog-html-admin:latest
```

映射`nginx.conf`

```bash
docker run -p 80:80 -v /docker_data/nginx.conf:/etc/nginx/nginx.conf --name blog-admin -d carldu/blog-html-admin:latest
```

## 关于

本项目是作者利用空余时间开发，维护全靠热爱发电。如果使用过程中遇到问题，欢迎报告 issue。
