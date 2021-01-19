import blog from './blog';
import error from './error';

export default [{
  path: '/',
  redirect: 'index'
}, {
  name: 'index',
  path: '/index',
  component: () => import('./index.vue'),
  children: [].concat(blog),
  redirect: '/index/blogList'
}].concat(error);
