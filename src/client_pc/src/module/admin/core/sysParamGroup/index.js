export default [
  {
    path: '/admin/sysParamGroup',
    name: 'sysParamGroupList',
    component: () => import('./sysParamGroupList'),
    meta: { title: '系统参数' }
  }
];
