# SixpenceStudio-Blog

基于`.Net Web Api`和`Vue`开发的博客系统，主要功能有：博客、想法、推荐信息、友人帐、归档等功能。

支持移动端和 PC 端显示

目前已开通`Demo`网站，请访问 🌍[此网站](http://www.karldu.cn/)获取最新信息。

## 开发环境

### 前端

前端基于 Vue.js，使用 ant design 组件库

#### 开发工具

Visual Studio Code

代码格式化插件：prettier  
vue 开发插件：vetur  
蚂蚁组件插件：ant-design-vue-helper  
Git 插件：Git History、GitLens

#### 开发环境

node：14.5.0  
yarn：1.15.2  
npm：6.14.5

### 后端

#### 开发工具

Visual Studio 2019

### 数据库

PostgreSQL：10.8

### 扩展函数

CREATE FUNCTION pg_catalog.text(integer) RETURNS text STRICT IMMUTABLE LANGUAGE SQL AS 'SELECT textin(int4out($1));';
CREATE CAST (integer AS text) WITH FUNCTION pg_catalog.text(integer) AS IMPLICIT;
COMMENT ON FUNCTION pg_catalog.text(integer) IS 'convert integer to text';

CREATE FUNCTION pg_catalog.text(bigint) RETURNS text STRICT IMMUTABLE LANGUAGE SQL AS 'SELECT textin(int8out($1));';
CREATE CAST (bigint AS text) WITH FUNCTION pg_catalog.text(bigint) AS IMPLICIT;
COMMENT ON FUNCTION pg_catalog.text(bigint) IS 'convert bigint to text';
