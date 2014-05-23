using System;
using System.Web;

namespace IsRouteMatch
{
    /// <summary>
    /// Class containing extension methods for the <see cref="T:System.Uri"/> class.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Determines whether the specified url matches the specified controller and action values based on the MVC routing table.
        /// </summary>
        /// <param name="uri">An Uri object containing the url.</param>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <param name="controllerName">The name of the controller class to match.</param>
        /// <param name="actionName">The name of the action method to match.</param>
        /// <returns>true if the specified url is mapped to the specified controller and action; otherwise, false.</returns>
        public static bool IsRouteMatch(this Uri uri, HttpContextBase httpContext, string controllerName, string actionName)
        {
            if (uri == null)
            {
                return false;
            }

            if (httpContext == null)
            {
                return false;
            }

            if (uri.DnsSafeHost != httpContext.Request.Url.DnsSafeHost)
            {
                // It's a remote url.
                return false;
            }

            var routeInfo = new RouteInfo(uri, httpContext.Request.ApplicationPath);

            if (routeInfo.RouteData == null)
            {
                return false;
            }

            if (!routeInfo.RouteData.Values.ContainsKey("controller"))
            {
                return false;
            }

            if (!routeInfo.RouteData.Values.ContainsKey("action"))
            {
                return false;
            }

            return routeInfo.RouteData.Values["controller"].ToString() == controllerName &&
                   routeInfo.RouteData.Values["action"].ToString() == actionName;
        }

        /// <summary>
        /// Gets the specified parameter from the RouteData object of the specified url.
        /// </summary>
        /// <param name="uri">An Uri object containing the url.</param>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <param name="parameterName">The name of parameter to fetch from the RouteValueDictionary.</param>
        /// <returns>A string if the specified value was found in the RouteValueDictionary; otherwise, null.</returns>
        public static string GetRouteParameterValue(this Uri uri, HttpContextBase httpContext, string parameterName)
        {
            if (uri == null)
            {
                return string.Empty;
            }

            if (httpContext == null)
            {
                return string.Empty;
            }
            
            var routeInfo = new RouteInfo(uri, httpContext.Request.ApplicationPath);
            return routeInfo.RouteData.Values[parameterName] != null
                ? routeInfo.RouteData.Values[parameterName].ToString()
                : string.Empty;
        }
    }
}
