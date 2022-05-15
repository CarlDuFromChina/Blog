export default [
  {
    path: '/login',
    name: 'login',
    meta: {
      title: 'Login'
    },
    component: () => import('./login')
  },
  {
    path: '/github-oauth/:id?',
    name: 'githubOAuth',
    meta: {
      title: 'Github OAuth'
    },
    component: () => import('./github')
  },
  {
    path: '/gitee-oauth/:id?',
    name: 'giteeOAuth',
    meta: {
      title: 'Gitee OAuth'
    },
    component: () => import('./gitee')
  }
];
