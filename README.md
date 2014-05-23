MvcIsRouteMatch
===============

This repository contains a code example to determine whether an URL (`Uri` object) matches a configured route of a controller action. 

The code belongs to [a blogpost of mine](http://henkmollema.blogspot.nl/2013/09/aspnet-mvc-check-if-referrer-url.html) regarding [this question on StackOverflow](http://stackoverflow.com/questions/4748342/how-to-determine-if-an-arbitrary-url-matches-a-defined-route/4749840).

### Usage:

```csharp
Uri referrer = Request.UrlReferrer;
bool match = referrer.IsRouteMatch(httpContext: HttpContext, controllerName: "Catalog", actionName: "Category");
```

The above code creates an internal http request using the [`RouteInfo`](https://github.com/HenkMollema/MvcIsRouteMatch/blob/master/RouteInfo.cs) class. The `InternalHttpContext` class can be used to fetch the route data for the request. We can then match the controller and action values in the `RouteData` object to the specified values.

I'm assuming you're using this inside a `Controller` class, so the `Request` and `HttpContext` properties are available.

The [`UriExtensions`](https://github.com/HenkMollema/MvcIsRouteMatch/blob/master/UriExtensions.cs) contains the `IsRouteMatch` method as well as the `GetRouteParameterValue`. This is a bonus method which allows you to fetch additional route parameter values.

For example:

Consider this route:

```csharp
routes.MapRoute(
	"Category",
	"gifts/{categoryName}",
	new { controller = "Catalog", action = "Category" },
```

You can fetch the value of `categoryName` this way:
```csharp
string categoryName = referrer.GetRouteParameterValue(HttpContext, "categoryName");
```
