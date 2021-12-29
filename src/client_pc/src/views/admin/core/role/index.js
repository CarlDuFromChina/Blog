export default [
  {
    path: '/admin/role',
    name: 'role',
    component: () => import('./roleList'),
    meta: { title: '角色列表' }
  }
];
