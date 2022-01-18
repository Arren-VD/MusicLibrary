#Music Library
##Intro
Music Library is ment to integrate external music libraries (spotify,soundcloud,local mp3s...) with a central database.
This way you will have all your music in one central place. It will also have functionality you do not always have on the external clients.
For example filtering on genre/category. Or in the case of spotify, to use playlists as a category and have an oversight of all categories per song and be able to easily add a categery (as playlist) to a spotify song.
## Project status
Currently the focus is on the back-end. There is a small front-end portion to be able to get an authorization token that is used by the back-end.

##Technical
###Back-End
#### Summary
The back-end exists of 7 parts. 
* DataAccess to access local Database, 
* Domain for the business logic
* ExternalAPI for the clients to connect to external API's
* Models (Database models)
* Views, contains the DTO's
* API, access point for the application
* UnitTesting, contains the unit tests for this app.
#### Used Technologies
*FluentValidations
*FluentAssertions
*Automapper
*xUnit
*EF
*LINQ
*GlobalExceptionHandler
*Swagger
*SQLServer
###Front-End
Currently a placeholder and a place to get authorization access with spotify.

