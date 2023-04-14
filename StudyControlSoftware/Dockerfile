FROM node:18-alpine as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY package.json .
COPY package-lock.json .

RUN npm install -g npm@9.6.4
RUN npm install

COPY . .
RUN npm run build -- --c production

FROM nginx as runtime
COPY --from=build /app/dist/study-control-software /usr/share/nginx/html