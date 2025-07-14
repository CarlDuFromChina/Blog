# Blog

<div align="center">

![.NET Core](https://img.shields.io/badge/.NET%20Core-6.0-blueviolet)
![Vue.js](https://img.shields.io/badge/Vue.js-2.6-green)
![Docker](https://img.shields.io/badge/Docker-Supported-blue)
![License](https://img.shields.io/badge/License-MIT-yellow)
![GitHub stars](https://img.shields.io/github/stars/CarlDuFromChina/Blog?style=social)

基于 `.Net 6.0` 和 `Vue 2.6` 开发的现代化博客系统，支持 PC 端和移动端响应式显示

[在线预览](https://karldu.cn) · [部署指南](https://karl-du.gitbook.io/sixpence-blog/bu-shu) · [更新日志](https://github.com/CarlDuFromChina/Blog/blob/master/CHANGELOG.md)

</div>

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

## � 项目截图

### 首页

![首页截图](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_index.png)

### 编辑博客

![编辑博客截图](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_edit.png)

### 阅读博客

![阅读博客截图](https://raw.githubusercontent.com/CarlDuFromChina/library/main/blog/blog_read.png)

## �🛠 技术栈

### 后端

+ **.NET 6.0**: 后端 API 框架
+ **Entity Framework**: ORM 框架
+ **MySQL**: 数据库
+ **Redis**: 缓存
+ **Log4Net**: 日志框架

### 前端

+ **Vue 2.6**: 前端框架
+ **Element UI**: UI 组件库
+ **Vue Router**: 路由管理
+ **Vuex**: 状态管理
+ **Axios**: HTTP 客户端

### 部署与工具

+ **Docker**: 容器化部署
+ **Nginx**: 反向代理
+ **Caddy**: Web 服务器

## 📁 项目结构

```text
Blog/
├── admin/                    # 后台管理系统 (Vue)
│   ├── src/
│   │   ├── components/       # 公共组件
│   │   ├── views/           # 页面组件
│   │   ├── router/          # 路由配置
│   │   ├── store/           # 状态管理
│   │   └── style/           # 样式文件
│   └── package.json
├── portal/                   # 前台门户网站 (Vue)
│   ├── src/
│   │   ├── components/       # 公共组件
│   │   ├── views/           # 页面组件
│   │   └── ...
│   └── package.json
├── mobile-h5/                # 移动端 H5 (Vue)
│   ├── src/
│   │   └── ...
│   └── package.json
├── server/                   # 后端 API (.NET 6)
│   ├── Blog.Business/        # 业务逻辑层
│   │   ├── Controller/       # API 控制器
│   │   ├── Service/         # 业务服务
│   │   ├── Entity/          # 实体模型
│   │   └── ...
│   └── Blog.Test/           # 单元测试
├── scripts/                  # 部署脚本
│   └── windows/             # Windows 部署脚本
├── docker-compose.yml        # Docker 编排文件
├── Caddyfile                # Caddy 配置文件
└── README.md
```

## 🚀 快速开始

### 环境要求

+ **.NET 6.0 SDK**
+ **Node.js 14+**
+ **MySQL 5.7+**
+ **Redis** (可选)

### 本地开发

1. **克隆项目**

   ```bash
   git clone https://github.com/CarlDuFromChina/Blog.git
   cd Blog
   ```

2. **后端启动**

   ```bash
   cd server/Blog.Business
   dotnet restore
   dotnet run
   ```

3. **前端启动**

   ```bash
   # 管理后台
   cd admin
   npm install
   npm run serve
   
   # 门户网站
   cd portal
   npm install
   npm run serve
   
   # 移动端 H5
   cd mobile-h5
   npm install
   npm run dev
   ```

### Docker 部署

```bash
# 克隆项目
git clone https://github.com/CarlDuFromChina/Blog.git
cd Blog

# 使用 Docker Compose 启动
docker-compose up -d
```

访问地址：

+ 门户网站：<http://localhost:8080>
+ 管理后台：<http://localhost:8081>
+ 移动端 H5：<http://localhost:8082>
+ API 接口：<http://localhost:5000>

## 🤝 贡献指南

欢迎参与项目贡献！请遵循以下步骤：

1. Fork 本仓库
2. 创建你的特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交你的修改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开一个 Pull Request

## 📄 许可证

本项目基于 [MIT License](LICENSE) 开源协议，详情请参阅 [LICENSE](LICENSE) 文件。

## 📧 联系方式

+ 作者：Karl Du
+ 博客：[karldu.cn](https://karldu.cn)
+ GitHub：[@CarlDuFromChina](https://github.com/CarlDuFromChina)

## ⭐ Star History

如果这个项目对你有帮助，请给它一个 ⭐️！

[![Star History Chart](https://api.star-history.com/svg?repos=CarlDuFromChina/Blog&type=Date)](https://star-history.com/#CarlDuFromChina/Blog&Date)

