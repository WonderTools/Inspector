# Inspector

When developing web application, its common to have an end point in the application that gives information such as name, environment, version, build number etc.
Inspector is a asp.net core middle ware that gives you the ability to add a version end point by writing minimal amount of code.

## How to use Inspector
1. Install WonderTools/Inspector in your asp.net core application
  * The nuget package could also be installed by nuget package manager or by command
  ```PS
  Install-Package WonderTools.Inspector -Version 1.0.0
  ```

2. In the asp.net core Startup, In the method ConfigureServices, add Inspector by
  ```c#
  services.AddInspector();
  ```

3. In the asp.net core Startup, In the method Configure, use Inspector by 
```c#
app.UseInspector(x =>
{
    x.AddEnvironment("developement");
    x.AddName("Inspector Usage Sample");
    x.AddVersion("1.0.0");
});
```
4. Now in your application, there should be an endpoint www.your-application.com/version that exposes the below data 
```javascript
{
  "Environment": "developement",
  "Name": "Inspector Usage Sample",
  "Version": "1.0.0",
  "Some Key": "Some Value"
}
```