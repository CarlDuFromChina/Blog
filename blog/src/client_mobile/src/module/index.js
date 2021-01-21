import blog from './blog';
import error from './error';
import friend from './friend';

export default [{
  path: '/',
  redirect: 'index'
}, {
  name: 'index',
  path: '/index',
  component: () => import('./index.vue'),
  children: [].concat(blog, friend),
  redirect: '/index/blogList'
}].concat(error);
