MvcIsRouteMatch
===============

This repository contains a code example to determine whether an URL (`Uri` object) matches a configured route of a controller action. 

The code belongs to [a blogpost of mine](http://henkmollema.blogspot.nl/2013/09/aspnet-mvc-check-if-referrer-url.html) regarding [this question on StackOverflow](http://stackoverflow.com/questions/4748342/how-to-determine-if-an-arbitrary-url-matches-a-defined-route/4749840).

### Usage:

```csharp
Uri referrer = Request.UrlReferrer;
bool match = referrer.IsRouteMatch(controllerName: "Catalog", actionName: "Category");
```

The above code creates an internal http request using the `RouteInfo` (todo: link) class. The `InternalHttpContext` class can be used to fetch the route data for the request. We can then match the controller and action values in the `RouteData` object to the specified values.
