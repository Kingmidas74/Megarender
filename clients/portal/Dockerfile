FROM node:16.10.0-alpine3.11 AS build

WORKDIR /usr/src/app
COPY ./clients/portal/package*.json ./
RUN npm ci
COPY ./clients/portal .
RUN npm run build
RUN chmod 777 ./dist/megarender-client/env.js

FROM nginxinc/nginx-unprivileged:latest
COPY ./clients/portal/nginx/default.conf /etc/nginx/conf.d/
COPY --from=build /usr/src/app/dist/megarender-client /tmp/share/nginx/html
CMD ["/bin/sh",  "-c",  "envsubst < /tmp/share/nginx/html/env.js | tee /tmp/share/nginx/html/env.js && exec nginx -g 'daemon off;'"]