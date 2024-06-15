# oefenproject voor C#

Attempting this tutorial: https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api 

At the time of making it works at https://azuretestappl.azurewebsites.net/swagger/index.html

The next step will be the Azure configuration API (not done yet):

Trying the Azure App Configuration

- Add a REST interface (GET only) to display the configuration.
  Name: Controllers/ConfigController.cs
  Initially return constant value
- Use this tutorial to integrate Configuration
  https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
  - read the first page. There are different providers. You will use the first options: appsettings.json
    It already exist. Add an own fields based on the tutorial.
  - next look for text "An alternative approach when using the options pattern"
    to use that approach to read a configuration parameter and return that on the Config REST interface.

Used sources (not complete):
- https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api
- https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
