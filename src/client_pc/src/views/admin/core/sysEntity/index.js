export default [
  {
    path: '/admin/sysEntity',
    name: 'sysEntityList',
    component: () => import('./sysEntityList'),
    meta: { title: '实体管理' }
  }
];
