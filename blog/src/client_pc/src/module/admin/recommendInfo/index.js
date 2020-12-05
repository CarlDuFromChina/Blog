export default [
  {
    path: '/admin/recommendInfo',
    name: 'recommendInfo',
    component: () => import('./recommendInfoList'),
    meta: { title: '推荐信息' }
  }
];
