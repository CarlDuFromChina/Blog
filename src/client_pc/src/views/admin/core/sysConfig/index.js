export default [
  {
    path: '/admin/sysConfig',
    name: 'sysConfig',
    component: () => import('./sysConfigList'),
    meta: { title: '系统参数' }
  }
];
