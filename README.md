# Cash Machine C\#
[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/742Untitled.png)](https://github.com/caiovms/desafio-warren-brasil)

Application developed as part of the selection process for the position of software developer at Warren Brasil.

### Technology
Main technologies used:

  - [.Net Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1)
  - [Docker](https://www.docker.com/)
  - [MySql](https://www.mysql.com/)

### Architecture  
The application architecture proposes to follow the premises of DDD (Domain-Driven-Design):

[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/834Apresenta_o1.png)](https://www.brunobrito.net.br/domain-driven-design/)

### Prerequisites

- Docker
  - [Windows/Mac](https://www.docker.com/products/docker-desktop)
  - [Linux](https://sempreupdate.com.br/container-instalar-docker-compose-no-ubuntu-20-04/)

To make sure that the docker has been installed correctly, run the following command:

- Windows
    ```sh
    $ docker -v
    ```

- Linux
    ```sh
    $ sudo systemctl status docker
    ```

### Installation

Download the code by running the following command:

```sh
$ git clone https://github.com/caiovms/cash-machine.git
```

Navigate to the project's root folder:

```sh
$ cd cash-machine
```

After with the docker installed, execute, build the project using the following command:

```sh
$ docker-compose build
```

If you do not have the Asp.Core and MySql images, the download will be performed automatically, then the image of the application will be generated, as well as the MySQL and MySQL Admin containers.

The application has 3 containers:

| Container | Address | Port |
| :------: | :------: | :------: |
| Webapi | http://localhost:5000 | 5000
| MySQL Admin | http://localhost:80 | 80
| MySQL | - | 3306

### How to use:
To upload the containers and access the application, execute the following command:
```sh
$ docker-compose up -d
```

** Note: ** The application container is waiting for the database container to be ready to be able to be executed, this way, its initialization takes a few seconds longer than the other containers, wait.

Make sure the containers are running, to do this, run the command:

```sh
$ docker container ls -a
```

[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/635Untitled.png)]()


To access the application, access the address http://localhost:5000.

To access the database administrator, access the address http://localhost:80 using the following information:

**Server:** *service_mysql*\
**Username:** *admin*\
**Password:** *warren2020*

To finalize the containers and delete the database data stored on the volume, run the following command:
```sh
$ docker-compose down -v
```

If you just want to finalize the containers and keep the data, remove the * -v * parameter from the above command:
```sh
$ docker-compose down
```

This way, when uploading the application again, the previous data will be kept.

### Notas

- All operations are carried out for one account, but the structure was designed taking into account a real environment.
- The Monetize function simulates a day pass, applying a 1% yield rate to the account balance.

