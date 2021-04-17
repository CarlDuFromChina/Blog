const requireAll = requireContext => requireContext.keys().map(requireContext);
const req = require.context('!svg-sprite-loader?{"symbolId":"sp-blog-[name]"}!./svgs', false, /.svg$/);
requireAll(req);
