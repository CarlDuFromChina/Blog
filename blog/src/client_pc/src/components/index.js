import spBlogTable from './spBlogTable';
import spBlogCard from './spBlogCard';
import spMarkdownEdit from './spMarkdownEdit';
import spMarkdownRead from './spMarkdownRead';

const components = [
  { name: spBlogTable.name, component: spBlogTable },
  { name: spBlogCard.name, component: spBlogCard },
  { name: spMarkdownEdit.name, component: spMarkdownEdit },
  { name: spMarkdownRead.name, component: spMarkdownRead }
];

export default components;
