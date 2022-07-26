publish.server:
	docker buildx build --platform=linux/amd64 -t carldu/blog-server:latest -f "./src/server_dotnet/Blog/Blog.Business/Dockerfile" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Blog.Business" "./src/server_dotnet/Blog" 
	docker push carldu/blog-server:latest

publish.pc:
	npm run build
	docker buildx build --platform=linux/amd64 -t carldu/blog-html-pc:latest -f "./src/client_pc/Dockerfile" "./src/client_pc"
	docker push carldu/blog-html-pc:latest

publish.mobile:
	npm run build
	docker buildx build --platform=linux/amd64 -t carldu/blog-html-mobile:latest -f "./src/client_mobile/Dockerfile" "./src/client_mobile"
	docker push carldu/blog-html-mobile:latest

publish:
	make publish.server
	make publish.pc
	make publish.mobile