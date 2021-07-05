export default [
  {
    path: '/admin/sysMenu',
    name: 'sysMenuList',
    component: () => import('./sysMenuList'),
    meta: { title: '菜单管理' }
  }
];
