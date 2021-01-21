export default [{
  name: 'friend-list',
  path: '/index/friendList',
  component: () => import('./friendList')
}, {
  name: 'friend-readonly',
  path: '/index/friend/:id',
  component: () => import('./friendReadonly')
}];
