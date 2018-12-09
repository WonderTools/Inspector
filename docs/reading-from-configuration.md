# Adding properties based on configuration

* In addition to adding inspection properties as key value pair, inspector also allows you to add inspection properties from asp.net core configuration.
* All simple properties of a asp.net core configuration could be added as inspection properties. Complex properties will not be added.
* InspectorConfigurator.AddConfigurationSection(IConfiguration, string) facilitates this
* The below examples could give you more information

## Example 1
Configuration




[Back To Documentation](https://wondertools.github.io/Inspector/Index)