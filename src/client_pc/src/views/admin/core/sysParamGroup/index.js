export default [
  {
    path: '/admin/sysParamGroup',
    name: 'sysParamGroupList',
    component: () => import('./sysParamGroupList'),
    meta: { title: '选项集' }
  }
];
