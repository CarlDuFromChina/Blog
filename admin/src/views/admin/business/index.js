var editor = [
  {
    path: '/admin/editor/post/:id?',
    name: 'postEdit',
    component: () => import('./editor/postEdit.vue'),
    meta: { title: '博客编辑' }
  },
  {
    path: '/admin/editor/draft/:draftId?',
    name: 'draftEdit',
    component: () => import('./editor/postEdit.vue'),
    meta: { title: '草稿编辑' }
  },
  {
    path: '/admin/editor/reading/:id?',
    name: 'readingNoteEdit',
    component: () => import('./editor/readingNoteEdit.vue'),
    meta: { title: '阅读笔记编辑' }
  }
];

export default [
  {
    path: '/admin/workplace',
    name: 'workplace',
    component: () => import('./workplace.vue')
  },
  {
    path: '/admin/post/:category?',
    name: 'post-list',
    component: () => import('./postList.vue'),
    meta: { title: '文章管理' }
  },
  {
    path: '/admin/series',
    name: 'seriesList',
    component: () => import('./seriesList.vue'),
    meta: { title: '博客系列' }
  },
  {
    path: '/admin/category',
    name: 'category',
    component: () => import('./category/categoryList.vue'),
    meta: { title: '文章分类' }
  },
  {
    path: '/admin/idea',
    name: 'idea',
    component: () => import('./idea/ideaList.vue'),
    meta: { title: '想法' }
  },
  {
    path: '/admin/link',
    name: 'link',
    component: () => import('./link/linkList.vue'),
    meta: { title: '链接' }
  },
  {
    path: '/admin/recommendInfo',
    name: 'recommendInfo',
    component: () => import('./recommendInfo/recommendInfoList.vue'),
    meta: { title: '推荐信息' }
  },
  {
    path: '/admin/drafts',
    name: 'draft',
    component: () => import('./draftList'),
    meta: { title: '草稿管理' }
  },
  {
    path: '/admin/readingNote',
    name: 'readingNoteList',
    component: () => import('./readingNoteList.vue'),
    meta: { title: '阅读笔记' }
  }
].concat(editor);
