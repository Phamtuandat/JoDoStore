<h1 align="center"><project-name>
JoDo Store
</h1>

<h2 align="center">Open-source for a mini-ecommerce website with a simple CRUD backend.

## Built With

<p align="center" style="margin-top:20px" >
<img src="/Asset/typescript.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">
<img src="/Asset/c-sharp.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">
<img src="/Asset/docker.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">
<img src="/Asset/postgre.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">
<img src="/Asset/node-js.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">
<img src="/Asset/nginx.png" style="width:30px; vertical-align:top; margin:4px;height:30px;">

## Links

- ✔️ [Repo](https://github.com/Phamtuandat/JoDoStore)

- ✔️ [Demo](https://phamtuandat.click)

- ✔️ [API](https://phamtuandat.click/api)

# Modules

- ✔️ JoDo Store Ecommerce

![Home Page](/Asset/home.JPG "Home Page")

![Product List Page](/Asset/listpage.JPG)

![Product Detail Page](/Asset/productDetail.JPG)

- ✔️ Admin management
- MVC dasdboard admin.
  

# About

**✔️ 1. Customer Management**

- Managing customers profiles
- Managing sale orders,
  <br/>

---

**✔️ 2. E-Commerce platform for Online Sales**

- Sale orders Management
- Product Showcase and Cart management

---

**✔️ 2. E-Commerce media storage**

- Storage image of website in server storage,

---

# How To Use

Use can pull my docker image in docker hub:

```bash
$ docker pull phamtuandat1a0/aspnet:v1.0.1
$ docker pull phamtuandat1a0/reactapp
```

## 1. Run using docker compose 🌈

A pre-built version of this platform is ready to use with docker compose:

### Check the ip adress of your system

```bash
$ ipconfig
$ ifconfig
```

```
version: '3.4'

services:
  store-db:
     container_name: postgres_container
     image: postgres
     environment:
        POSTGRES_USER: ${POSTGRES_USER}
        POSTGRES_PASSWORD: ${POSTGRES_PASSWORD}
     ports:
        - "5432:5432"
     volumes:
        - pgdata:/var/lib/postgresql/data
     networks:
        - postgres
     restart: unless-stopped


  gearshop_dotnetapp:
    image: phamtuandat1a0/aspnet:v1.0.1
    depends_on:
      - "store-db"
    container_name: app-service
    ports:
      - "4432:4432"
    environment:
      DB_CONNECTION_STRING: YOUR_CONNECTION_STRING
      CLOUDINARY_URL: your-cloudinaryApis
      ASPNETCORE_ENVIRONMENT: "Production"
    networks:
      - postgres

  # frontend:
  #   image: phamtuandat1a0/reactapp
  #   depends_on:
  #    - "store-db"
  #   container_name: react-app
  #   ports:
  #     - 3000:80
  #   networks:
  #    - postgres

volumes:
  pgdata:
networks:
  postgres:
    driver: bridge
```

</p>
## Future Updates

- [ ] Connect to shipping API,
- [ ] Management User, admin,
- [ ] Register required email comfirmed

## Author

- [Profile](https://github.com/phamtuandat "Pham Tuan Dat")
- [Email](mailto:phamtuandat1a0@gmail.com?subject=Hi "Hi!")
- [Website](https://phamtuandat.click "Welcome")

## 🤝 Support

Contributions, issues, and feature requests are welcome!

Give a ⭐️ if you like this project!
