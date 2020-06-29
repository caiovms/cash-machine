# Desafio Warren Brasil C\#
[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/742Untitled.png)](https://github.com/caiovms/desafio-warren-brasil)

Aplicação desenvolvida como parte do processo seletivo para o cargo de desenvolvedor de software da Warren Brasil.

### Tecnologias
Principais tecnologias utilizadas:

  - [.Net Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1)
  - [Docker](https://www.docker.com/)
  - [MySql](https://www.mysql.com/)

### Arquitetura  
A arquitetura da aplicação se propõe a seguir as premissas do DDD (Domain-Driven-Design): 

[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/834Apresenta_o1.png)](https://www.brunobrito.net.br/domain-driven-design/)

### Pré-requisitos

- Docker
  - [Windows/Mac](https://www.docker.com/products/docker-desktop)
  - [Linux](https://sempreupdate.com.br/container-instalar-docker-compose-no-ubuntu-20-04/)

Para cerificar-se que o docker foi instalado corretamente execute o seguinte comando:

- Windows
    ```sh
    $ docker -v
    ```

- Linux
    ```sh
    $ sudo systemctl status docker
    ```

### Instalação

Faça download do código executando o seguinte comando:

```sh
$ git clone https://github.com/caiovms/desafio-warren-brasil.git
```

Navegue a até a pasta raiz do projeto:

```sh
$ cd desafio-warren-brasil
```

Após com o docker instalado execute, build o projeto utilizando o seguinte comando: 

```sh
$ docker-compose build
```

Caso não possua as imagens do Asp.Core e do MySql, o download será realizado automaticamente, em seguida será gerada a imagem da aplicação bem como as dos containers MySQL e MySQL Admin.

A aplicação possui 3 containers:

| Container | Endereço | Porta |
| :------: | :------: | :------: |
| Webapi | http://localhost:5000 | 5000
| MySQL Admin | http://localhost:80 | 80
| MySQL | - | 3306

### Como usar:
Para subir os containers e acessar a aplicação, execute o seguinte comando:
```sh
$ docker-compose up -d
```

**Obs:** O container da aplicação aguarda o container do banco de dados estar pronto para pode ser executado, por isso, ele demora alguns segundos a mais que os outros. 

Certifique-se que os containers estão rodando, para isso execute o comando:

```sh
$ docker container ls -a
```

[![N|Solid](http://www.hostcgs.com.br/hostimagem/images/635Untitled.png)]()


Para acessar a aplicação, acesse o endereço http://localhost:5000.

Para acessar o administrador do banco de dados acesse o endereço http://localhost:80 utilizando as seguintes informações:

**Servidor:** *service_mysql*\
**Utilizador:** *admin*\
**Palavra-passe:** *warren2020*

Para finalizar os containers e excluir os dados do banco de dados armazenados no volume, execute o seguinte comando:
```sh
$ docker-compose down -v
```

Caso queira apenas finalizar os containers e manter os dados, remova o parâmetro *-v* do comando acima: 
```sh
$ docker-compose down
```

Dessa forma, ao subir a aplicação novamente, os dados anteriores serão mantidos.

### Notas

- Todas as operações são realizadas para uma conta, porém a estrutura foi pensada levando em conta uma ambiente real.
- A função Rentabilizar simula uma passagem de dia, aplicando uma taxa de rendimento de 1% ao saldo da conta.

