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
  const { token, userId } = data;
  store.commit('updateAuth', {
    token: token.AccessToken.TokenContent,
    userId: userId
  });
  store.commit('changeLogin', true);
  refreshToken(store, token);
}

function refreshToken(store, token) {
  const now = new Date();
  const t1 = new Date(token.AccessToken.Expires);
  const t2 = new Date(token.RefreshToken.Expires);
  const diff = (t1.getTime() - now.getTime()) / 1000 - 10; // 最后十秒去刷新token
  if (t2 < now) {
    return;
  }
  setTimeout(async () => {
    await store.commit('updateToken', token.RefreshToken.TokenContent); // 更换成RefreshToken
    const resp = await sp.get('api/AuthUser/RefreshAccessToken');
    if (resp) {
      store.commit('updateToken', resp.TokenContent);
      refreshToken(store, { AccessToken: resp, RefreshToken: token.RefreshToken });
    }
  }, diff * 1000);
}
