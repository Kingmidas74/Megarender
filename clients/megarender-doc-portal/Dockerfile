FROM node:14.2.0-alpine3.10 AS build

RUN apk add --update \
  python \
  python-dev \
  py-pip \
  build-base \
  git \
  openssh-client \
&& pip install virtualenv \
&& rm -rf /var/cache/apk/*

WORKDIR /usr/src/app
COPY ./clients/megarender-doc-portal/package.json ./
RUN npm install
COPY ./clients/megarender-doc-portal .
RUN npm run build
#RUN chmod 777 ./dist/megarender-client/env.js

FROM nginxinc/nginx-unprivileged:latest
COPY ./clients/megarender-doc-portal/nginx/default.conf /etc/nginx/conf.d/
COPY --from=build /usr/src/app/build /tmp/share/nginx/html
#CMD ["/bin/sh",  "-c",  "envsubst < /tmp/share/nginx/html/env.js | tee /tmp/share/nginx/html/env.js && exec nginx -g 'daemon off;'"]
CMD ["/bin/sh",  "-c",  "nginx -g 'daemon off;'"]