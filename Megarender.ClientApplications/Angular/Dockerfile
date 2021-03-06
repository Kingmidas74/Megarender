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
COPY ./Megarender.ClientApplications/Angular/package.json ./
RUN npm install
COPY ./Megarender.ClientApplications/Angular .
RUN npm run build

FROM nginx:1.17.10-alpine
COPY ./Megarender.ClientApplications/Angular/nginx/default.conf /etc/nginx/conf.d/
COPY --from=build /usr/src/app/dist/TestApp /usr/share/nginx/html

CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/env.js | tee /usr/share/nginx/html/env.js && exec nginx -g 'daemon off;'"]
