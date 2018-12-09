# Adding properties based on configuration

* In addition to adding inspection properties as key value pair, inspector also allows you to add inspection properties from asp.net core configuration.
* All simple properties of a asp.net core configuration could be added as inspection properties. Complex properties will not be added.
* InspectorConfigurator.AddConfigurationSection(IConfiguration, string) facilitates this
* The below examples could give you more information

## Example 1 - Using a section for configuration
asp.net core Configuration
```json
{
  "ConfigurationData": {
    "Property1": "Value1",
    "Property2": "Value2",
    "ArrayProperty": [ 12343, 324, 2342 ],
    "PropertyWithObjectAsValue": {
       "SomeProperty": "SomeValue"
    }
  } 
}
```

When using Inspector, we could add a section to get inspection properties
```cs
	app.UseInspector(x =>
		{
			x.AddName("Service Name - Sample Service");
			x.AddConfigurationSection(Configuration, "ConfigurationData");
		});
```

Now the end point https://your-application.com/version would give the following data
```json
{
  "Name": "Service Name - Sample Service",
  "Property1": "Value1",
  "Property2": "Value2"
}
```

## Example 2 - Complicated section
asp.net core Configuration
```json
{
  "Node1": {
    "Node2": [
      "SomeData",
      {
        "Property1": "Value1",
        "Property2": "Value2",
        "ArrayProperty": [ 12343, 324, 2342 ],
        "PropertyWithObjectAsValue": {
          "SomeProperty": "SomeValue"
        }
      },
      2342
    ]
  }
}
```

When using Inspector, we could add a section as shown below
```cs
	app.UseInspector(x =>
		{
			x.AddName("Service Name - Sample Service");
			x.AddConfigurationSection(Configuration, "Node1:Node2:1");
		});
```

Now the end point https://your-application.com/version would give the following data
```json
{
  "Name": "Service Name - Sample Service",
  "Property1": "Value1",
  "Property2": "Value2"
}
```

[Back To Documentation](https://wondertools.github.io/Inspector/Index)