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
    token: token,
    userId: userId
  });
  store.commit('changeLogin', true);
}
