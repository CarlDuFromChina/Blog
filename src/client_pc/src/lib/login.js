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

export function thidPartyLogin(type) {
  switch (type) {
    case 'github':
      return githubLogin();
    case 'gitee':
      return giteeLogin();
    case 'qq':
      return qqLogin();
    default:
      break;
  }
}

async function githubLogin() {
  var config = await sp.get('/api/system/LoginConfig').then(resp => resp.github);
  var url = `https://github.com/login/oauth/authorize?client_id=${config.client_id}`;
  window.location.href = url;
}

async function qqLogin() {
  alert('暂不支持');
  return '';
}

async function giteeLogin() {
  var config = await sp.get('/api/system/LoginConfig').then(resp => resp.gitee);
  var redirectUri = `${window.location.origin}/gitee-oauth`
  var url = `https://gitee.com/oauth/authorize?response_type=code&redirect_uri=${redirectUri}&client_id=${config.client_id}`;
  window.location.href = url;
}
