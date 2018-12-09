# Authentication
* By default, there is no authentication, and the version end point is unguarded. This can be changed however
* When adding Inspector, using the method InspectorConfigurator.AuthenticateWith() adds authentication
* There are two overloads for AuthenticateWith method
    * void AuthenticateWith(params string[] validTokens). All valid passwords are passed as parameter
	* void AuthenticateWith(Func<string, bool> authenticator). A Func delegate is a parameter, and password is authenticated by this delegate
* When authentication is added, then version end point is guarded
    * For requests to be served, there has to a header with key "wondertools-authorization" and password as value
* The key used for authentication could be changed by the method InspectorConfigurator.UseAuthenticationHeader(string)
* Once authenication is enabled, a UI page is also server at https://your-application/version-login
* Cors for version end point could be enabled by InspectorConfigurator.EnableCors()

[Back To Documentation](https://wondertools.github.io/Inspector/Index)
