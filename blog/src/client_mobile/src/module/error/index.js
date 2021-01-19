export default [{
  path: '*',
  redirect: '/404'
}, {
  name: '404',
  path: '/404',
  component: () => import('./404')
}];
