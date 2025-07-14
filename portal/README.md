# Blog.UI

> 一个基于 Vue2.0 的博客前端项目

## 功能

+ 主页
+ 分类
+ 项目仓库

## 启动

``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev
```

## 生成

> 注：本项目使用`Github Action`自动发布镜像

手动：

```bash
docker build -f ".\Dockerfile" -t carldu/blog-html-pc:latest .
```

## 部署

快速部署：

```bash
docker run -p 80:80 --name blog-ui -d carldu/blog-html-pc:latest
```

映射`nginx.conf`

```bash
docker run -p 80:80 -v /docker_data/nginx.conf:/etc/nginx/nginx.conf --name blog-ui -d carldu/blog-html-pc:latest
```

## 关于

本项目是作者利用空余时间开发，维护全靠热爱发电。如果使用过程中遇到问题，欢迎报告 issue。
