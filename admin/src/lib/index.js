import { common, http, uuid } from '@sixpence/js-utils';
import store from '../store';
import axios from 'axios';
import './jigsaw';
import spExtension from './sp';

var originHttp = {
  originGet: (url, config) => {
    var instance = axios.create({
      timeout: 5000,
      baseURL: window.origin
    });
    return instance.get(url, config);
  }
};

window.sp = Object.assign({}, common, http, originHttp, spExtension);
window.uuid = Object.assign({}, uuid);

axios.defaults.baseURL = process.env.VUE_APP_AXIOS_BASE_URL || window.location.origin;
axios.defaults.timeout = 20000;
axios.defaults.withCredentials = true;
axios.defaults.headers.post['Content-Type'] = 'application/json';
axios.defaults.headers.put['Content-Type'] = 'application/json';
axios.interceptors.request.use(async config => {
  await checkToken();
  config.headers.Authorization = `Bearer ${store.getters.getToken}`;
  return config;
});
axios.interceptors.response.use(response => {
  // 处理excel文件
  if (response.headers && response.headers['content-type'] === 'application/octet-stream') {
    // 提取文件名
    const fileName = response.headers['content-disposition'].match(/filename=(.*)/)[1].split(';')[0];
    // 将二进制流转为blob
    const blob = new Blob([response.data], { type: 'application/octet-stream' });
    downloadUrl(blob, fileName);
    return Promise.resolve(true);
  }
  return Promise.resolve(response);
});

// download url
const downloadUrl = (blob, fileName) => {
  if (typeof window.navigator.msSaveBlob !== 'undefined') {
    window.navigator.msSaveBlob(blob, decodeURI(fileName));
  } else {
    // 创建新的URL并指向File对象或者Blob对象的地址
    const blobURL = window.URL.createObjectURL(blob);
    // 创建a标签，用于跳转至下载链接
    const tempLink = document.createElement('a');
    tempLink.style.display = 'none';
    tempLink.href = blobURL;
    tempLink.setAttribute('download', decodeURI(fileName));
    // 兼容：某些浏览器不支持HTML5的download属性
    if (typeof tempLink.download === 'undefined') {
      tempLink.setAttribute('target', '_blank');
    }
    // 挂载a标签
    document.body.appendChild(tempLink);
    tempLink.click();
    document.body.removeChild(tempLink);
    // 释放blob URL地址
    window.URL.revokeObjectURL(blobURL);
  }
};

/**
 * 检查token是否有效
 */
async function checkToken() {
  // 匿名用户访问无需检查token
  if (store.getters.isLoggedIn) {
    // 已登录但是token为空重新登录
    if (store.getters.isEmptyToken()) {
      reLogin();
    }

    // 刷新token过期重新登录
    if (store.getters.isRefreshTokenExpired()) {
      reLogin();
    }

    // 授权token过期重新获取
    if (store.getters.isAccessTokenExpired()) {
      await refreshToken();
    }
  }
}

/**
 * 刷新token
 */
async function refreshToken() {
  store.commit('changeTokenWithRefreshToken'); // 更换成RefreshToken
  const resp = await sp.get('api/auth_user/refresh_access_token');
  if (resp) {
    store.commit('updateAccessToken', resp);
  }
}

/**
 * 重新登录
 */
function reLogin() {
  store.commit('clearAuth');
  store.commit('changeLogin', false); // 修改登录状态
  location.href = '/login';
}
