/**
 * 清除用户登录信息
 */
export function clearAuth(store) {
  store.commit('clearAuth');
  store.commit('changeLogin', false); // 修改登录状态
}

/**
 * 保存用户登录信息
 */
export function saveAuth(store, data) {
  const { token, userId, userName } = data;
  store.commit('updateAuth', {
    token: token,
    userId: userId,
    userName: userName
  });
  store.commit('changeLogin', true);
}

/**
 * 第三方联合登录，支持：github、gitee、qq
 * @param {String} type 
 * @returns 
 */
export function thirdPartyLogin(type) {
  switch (type) {
    case 'github':
      return github.login();
    case 'gitee':
      return gitee.login();
    case 'qq':
      return qq.login();
    default:
      break;
  }
}

/**
 * 第三方账号绑定，支持：github、gitee、qq
 * @param {String} type 
 * @param {String} id 
 * @returns 
 */
export function thirdPartyBind(type, id) {
  switch (type) {
    case 'github':
      return github.bind(id);
    case 'gitee':
      return gitee.bind(id);
    case 'qq':
      return qq.bind(id);
    default:
      break;
  }
}

var github = {
  async login() {
    var config = await sp.get('/api/system/login_config').then(resp => resp.github);
    var redirectUri = `${window.location.origin}/github-oauth`; // 回传登录地址
    var url = `https://github.com/login/oauth/authorize?client_id=${config.client_id}&redirect_uri=${redirectUri}`;
    window.location.href = url;
  },
  async bind(userid) {
    var config = await sp.get('/api/system/login_config').then(resp => resp.github);
    var redirectUri = `${window.location.origin}/github-oauth/${userid}`; // 回传登录地址
    var url = `https://github.com/login/oauth/authorize?client_id=${config.client_id}&redirect_uri=${redirectUri}`;
    window.location.href = url;
  }
}


var qq = {
  async login() {
    alert('暂不支持');
    return '';
  },
  async bind() {
    alert('暂不支持');
  }
}

var gitee = {
  async login() {
    var config = await sp.get('/api/system/login_config').then(resp => resp.gitee);
    var redirectUri = `${window.location.origin}/gitee-oauth`
    var url = `https://gitee.com/oauth/authorize?response_type=code&redirect_uri=${redirectUri}&client_id=${config.client_id}`;
    window.location.href = url;
  },
  async bind(userid) {
    var config = await sp.get('/api/system/login_config').then(resp => resp.gitee);
    var redirectUri = `${window.location.origin}/gitee-oauth?id=${userid}`; // 回传登录地址
    var url = `https://gitee.com/oauth/authorize?response_type=code&redirect_uri=${redirectUri}&client_id=${config.client_id}`;
    window.location.href = url;
  }
}
