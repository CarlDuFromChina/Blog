# Blog.Server

> 一个基于 .net 6.0 的博客后端项目

## Package

+ Sixpence.Web@2.1.0
  + Sixpence.ORM@3.1.0
  + Sixpence.Common@2.0.0

## Deploy

本项目支持 docker 容器化部署

```bash
docker run -p 5000:5000 --name blog-server -d carldu/blog-server:latest
```

映射

```bash
/docker_data/blog/server/appsettings.json:/app/appsettings.json # 配置文件`appsettings.json`
/docker_data/blog/server/logs:/app/log # 日志
/docker_data/blog/server/storage:/app/storage # 存储文件
```

## About

本项目是作者利用空余时间开发，维护全靠热爱发电。如果使用过程中遇到问题，欢迎报告 issue。