import spBlogCard from './spBlogCard';
import spComment from './spComment';
import spCard from './spCard';
import cloudUpload from './cloudUploadDialog';

const components = [
  { name: spBlogCard.name, component: spBlogCard },
  { name: spComment.name, component: spComment },
  { name: spCard.name, component: spCard },
  { name: cloudUpload.name, component: cloudUpload }
];

export default components;
