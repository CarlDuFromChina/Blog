export default [
  {
    path: '/admin/userInfo',
    name: 'userInfoList',
    component: () => import('./userInfoList'),
    meta: { title: '用户信息' }
  }
];
