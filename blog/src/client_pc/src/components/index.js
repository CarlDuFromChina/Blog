import spBlogTable from './spBlogTable';
import spBlogCard from './spBlogCard';
import spMarkdownEdit from './spMarkdownEdit';

const components = [
  { name: spBlogTable.name, component: spBlogTable },
  { name: spBlogCard.name, component: spBlogCard },
  { name: spMarkdownEdit.name, component: spMarkdownEdit }
];

export default components;
