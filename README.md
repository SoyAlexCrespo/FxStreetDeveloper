# FxStreet Prueba técnica
## Alex Crespo - 05/02/2020

### Instrucciones para ejecutar
La prueba está implementada en .NetCore 3.1.1. Para su ejecución, se deberá lanzar el proyecto FxStreetDeveloper.API. 

### Consideraciones
 - Diseño en DDD (algo simplificado) respetando la inmutabilidad de las entidades y agregados. No se ha podido resolver el uso de ValueObjects contra EntityFramework, con lo cual, las entidades utilizan tipos de datos primitivos (code smell primitive obsession).
 - Se implementa siguiendo TDD en la medida de la posible, a pesar de que no se han respetado los commits en algunos de los ciclos red-green-refactor.
 - Se utiliza EntityFramework InMemory para ahorrarnos el patrón Repository (habitual en proyectos DDD). Esto da la ventaja de que nos ahorramos esa capa e implementamos los tests unitarios directamente sobre un sistema muy similar a la base de datos final. Por contra, hacemos que partes muy próximas al dominio dependan de la infraestructura (no inyectada), testeamos infraestructura en test unitarios (no debería ser así, esto debería ser en tests de integración) y hacemos tambalear un poco los principios SOLID. Inicialmente pareció buena idea.
 - Se ha intentado replicar al máximo la estructura de la Rest API propuesta, pero hay alguna diferencia (en como se muestran los DataModels de las interficies) debido a la versión de .NetCore.
 - Se utiliza HangFire para resolver el CronJob que llamará al Endpoint IncorrectAlignment con la lista de jugadores y managers que cumplen los requisitos de tarjetas del enunciado, en partidos que comenzarán en 5 minutos o menos. Se puede acceder a la consola de HangFire mediante la ruta /hangfire. 
 
### Puntos de mejora
Me habría gustado disponer de más tiempo para solventar los siguientes puntos:
 - Utilizar ValueObjects en las entidades.
 - Mayor cobertura de test (comencé usando TDD, pero al llegar a Match decidí implementar sus funcionalidades de forma más directa debido a la urgencia en la entrega y al hecho de poder testear con Swagger).
 - Refactorizaciones generales (reutilizar código, resolver alguna abstracción / herencia en las entidades o en los Dto, descubrir algunas interficies que permitiesen aplicar el principio de segregación de interficies, etc).
 - El Job de HangFire vuelve a enviar cada minuto, durante los 5 minutos previos al partido, la lista con los jugadores y entrenadores sancionados (hubiese sido interesante persistir algún flag en la entidad para evitar esa reiteración).
