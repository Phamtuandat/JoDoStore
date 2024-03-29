# 1. For build React app
FROM node:lts AS development
# Set working directory
WORKDIR /app
#
COPY package.json /app/package.json
COPY package-lock.json /app/package-lock.json
# Same as npm install
RUN yarn install
COPY . /app
ENV PORT=3000
CMD [ "yarn", "start" ]

FROM development AS build

RUN yarn run build
# 2. For Nginx setup
FROM nginx:alpine
# Copy config nginx
COPY --from=build /app/ssl/diydevblog.com.key /etc/ssl/diydevblog.com.key
COPY --from=build /app/ssl/diydevblog.com.pem /etc/ssl/diydevblog.com.pem

COPY --from=build /app/.nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/.nginx/admin.conf /etc/nginx/conf.d/admin.conf
COPY --from=build /app/.nginx/static.conf /etc/nginx/conf.d/static.conf
COPY --from=build /app/.nginx/account.conf /etc/nginx/conf.d/account.conf




WORKDIR /usr/share/nginx/html
COPY .env.production .

# Add bash
RUN apk add --no-cache bash

# Make our shell script executable
RUN chmod +x .env.production
# Remove default nginx static assets
RUN rm -rf ./*
# Copy static assets from builder stage
COPY --from=build /app/build .
# Containers run nginx with global directives and daemon off
ENTRYPOINT ["nginx", "-g", "daemon off;"]

