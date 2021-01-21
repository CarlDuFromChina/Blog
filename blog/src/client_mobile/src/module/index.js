import blog from './blog';
import error from './error';
import friend from './friend';
import reading from './reading';

export default [{
  path: '/',
  redirect: 'index'
}, {
  name: 'index',
  path: '/index',
  component: () => import('./index.vue'),
  children: [].concat(blog, friend, reading),
  redirect: '/index/blogList'
}].concat(error);
