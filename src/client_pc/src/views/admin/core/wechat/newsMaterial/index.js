export default [
  {
    path: '/admin/newsMaterial',
    name: 'newsMaterialList',
    component: () => import('./newsMaterialList'),
    meta: { title: '图文管理' }
  }
];
