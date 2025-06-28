# SentimentApi

  

API RESTful para análisis de sentimiento de comentarios de usuarios sobre productos.

## Proceso de creación

  

1. **Estructura del proyecto**: Se creó una solución en C# con ASP.NET Core

``` cs

dotnet new webapi -n SentimentApi

```

  

2. **Modelo de datos**: Creacion del model `Comment` con los campos requeridos se agrega en la carpeta de `Models`  y se configuró el contexto de Entity Framework para SQL Server en la carpeta `Data` con el archivo `CommentsContext.css`

  

3. Añadir paquetes de Entity Framework y SQL Server

``` cs

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

  

dotnet add package Microsoft.EntityFrameworkCore.Tools

```

  

4.  **Lógica de Análisis de Sentimientos**: Creación del servicio `SentimentAnalyzer.css` en la carpeta de `Services`

  

5. **Controlador y Endpoints**: Creamos `CommentsController.cs` en `Controllers` y agregamos los endpoints.

  

   - `POST /api/comments`: Recibe un comentario, analiza el sentimiento y lo almacena.

   - `GET /api/comments`: Devuelve todos los comentarios, con filtros opcionales por producto y sentimiento.

  

   - `GET /api/sentiment-summary`: Devuelve un resumen de conteo de comentarios por sentimiento.

e.

5. **Pruebas manuales**: Se probó la API usando Postman, verificando el correcto funcionamiento de los endpoints principales.

  

## Cómo ejecutar el proyecto

  

1. Clona el repositorio.

  

2. Configura la cadena de conexión a SQL Server en `appsettings.json`.

  

3. Ejecuta las migraciones

```cs

dotnet ef database update

```

  

4. Correr la api

```cs

dotnet run

```  

  

5. Accede a Postman en http://localhost:5298/api/comments creamos dos peticiones POST con el body

```json

{

  "productId": "PROD0001",

  "userId": "A00001",

  "commentText": "Este producto es excelente, cumple con lo prometido y mucho mas recomendado."

}

  

{

  "productId": "PROD0004",

  "userId": "A00002",

  "commentText": "Es muy malo el producto, no cumple con lo prometido no lo compren."

}

```

6. Podemos obtener los comentarios con peticion GET con http://localhost:5298/api/comments?product_id=PROD0001 es el positivo  o el negativo con http://localhost:5298/api/comments?product_id=PROD0004 con el id del producto a comprobar  

7. Obtener resumen de sentimientos de los productos con http://localhost:5298/api/sentiment-summary

  

## Estructura del proyecto

  

- `Controllers/CommentsController.cs`

- `Models/Comment.cs`

- `Services/SentimentAnalyzer.cs`

- `DTOs/CommentsDto.cs`

- `Data/CommentsContext.cs`

- `Program.cs`

- `appsettings.json`

  
  

## Utilizacion con docker

### Configuracion con docker
1. Habilita autenticación SQL Server y conexiones remotas
- - En el servidor → Propiedades → Seguridad.
- Selecciona **"SQL Server y autenticación de Windows"**.
- En "SQL Server Configuration Manager", habilitar el protocolo TCP/IP y  el puerto 1433 esté configurado en "IPAll".
- Reiniciar el servicio de SQL Server.

1. Asegurar de que el firewall de windows este abierto en el puerto 1433 si no ejecutar en powershell como administrador el siguiente comando
   
``` shell
New-NetFirewallRule -DisplayName "SQL Server" -Direction Inbound -Protocol TCP -LocalPort 1433 -Action Allow
```

### Ejecucion en docker
1. Contruccion de magen
``` shell
docker build -t sentiment-api .
docker run -d -p 8080:8080 --name sentiment-container sentiment-api
```
2. Prueba con postman con http://localhost:8080/api/sentiment-summary

## Solucion de errores

### Error de conexión a SQL Server 
provider: TCP Provider, error: 40 - Could not open a connection to SQL Server
- Hay que asegurarse que el puerto 1433 esta abierto y escuchando con 
``` shell
netstat -an | findstr 1433
```
- Si no esta escuchando ingresar a la terminal del docker 
```shell
docker exec -it sentiment-container /bin/sh
```
- Ejecutar los siguientes comandos
```shell
apt update && apt install -y iputils-ping telnet ping host.docker.internal telnet host.docker.internal 1433
```
## Notas

- El análisis de sentimiento es básico y case-insensitive.
- 

  

- Próximos pasos:

  - agregar manejo de errores

  - pruebas automáticas

  

## Autor

  

Irvin Alejandro Morales Ramirez (IrvinMora13)