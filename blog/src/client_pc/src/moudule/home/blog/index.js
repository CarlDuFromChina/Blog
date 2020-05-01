import js from './js';
import csharp from './csharp';

export const blogRouter = [
  {
    path: '/blogReadonly/:id?',
    name: 'blogReadonly',
    component: () => import('./blogReadonly')
  },
  {
    path: '/blogEdit/:id?',
    name: 'blogEdit',
    component: () => import('./blogEdit')
  }
];

export const blogsRouter = [
  js,
  csharp
];

export default { blogRouter, blogsRouter };
