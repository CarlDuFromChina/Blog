FROM node:14-alpine as node
WORKDIR /app
COPY ./ /app/
RUN npm config set registry https://registry.npm.taobao.org \
    && npm install \
    && npm run prod

FROM nginx:1.15.2-alpine

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY --from=node /app/dist /usr/share/nginx/html
EXPOSE 80 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]