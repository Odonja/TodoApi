# oefenproject voor C#

Attempting this tutorial: https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api

At the time of making it works at https://azuretestappl.azurewebsites.net/swagger/index.html

The next step will be the Azure configuration API:

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

Playing with azure service bus (part of it is in https://github.com/Odonja/animal-service-bus)

- login in Azure portal en bekijk de eigenschappen van onze service bus
- maak een console applicatie zoals hier beschreven.
  https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples/Sample01_SendReceive.md
  > connection string: ...
  > queue: configurationqueue
- bekijk deze list van examples, zodat je andere topics ziet die me service bus te maken heb.
- study this article on architecture.
  https://learn.microsoft.com/en-us/azure/architecture/example-scenario/integration/queues-events
  From the artiacle you will understand, that in order to have a true event-driven (aka message based) service
  that you need an Event Grid (required premium $).
  One that is clear to you from the article, you are ready to move to the next step.
- Use the "Send and receive a message using queues" code and integrate that into a new REST service /api/config/use-service-bus
  This service shall use service-bus to obtain the REST configuration-response.

Used sources (not complete):

- https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api
- https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
- https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-samples
-
