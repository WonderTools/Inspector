# Adding properties based on configuration

* By default the end point where the version information is available is http://your-application/version
* This could be changed by void InspectorConfigurator.SetBaseEndpoint(string) method while adding Inspector

## Example 1

When using Inspector, we could add a section to get inspection properties
```cs
	app.UseInspector(x =>
		{
			x.AddName("Service Name - Sample Service");
			x.AddConfigurationSection(Configuration, "Node1:Node2:1");
			x.SetBaseEndpoint("/Hello");
		});
```

Now version endpoint would be https://your-application.com/hello/version


## Example 2

When using Inspector, we could add a section to get inspection properties
```cs
	app.UseInspector(x =>
		{
			x.AddName("Service Name - Sample Service");
			x.AddConfigurationSection(Configuration, "Node1:Node2:1");
			x.SetBaseEndpoint("/Hello/something");
		});
```

Now version endpoint would be https://your-application.com/hello/something/version

[Back To Documentation](https://wondertools.github.io/Inspector/Index)