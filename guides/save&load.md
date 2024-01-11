# How to save data?

If you want to save data from a Node, you need to create a method called `SaveData`. It has to return `Godot.Collections.Dictionary<string, Variant>`. For example:
```csharp
public Godot.Collections.Dictionary<string, Variant> SaveData()
	{
		return new Godot.Collections.Dictionary<string, Variant>
		{
			{"lorem", "ipsum"}
		};
	}
```

# How to load data?

If you want to load data to a Node, you need to get access to Singleton. To do so, call `GetNode<singleton>("/root/singleton")`. After doing so, access SaveHandler, and run method `GetValue`. For example:
```csharp
GetNode<singleton>("/root/singleton").SaveHandler.GetValue(this, "lorem");
```

# Overriding name
Sometimes, you might need to override the name of your node. To do so, declare a public (preferably string) variable called `OverrideSaveName`. When calling `GetValue`, you can provide a third argument forceName if you want to access that value, or if you call `GetValue` from a Node that has `OverrideSaveName` declared, it will automatically use that name. For example:
```csharp
// Declaring OverrideSaveName
public string OverrideSaveName = "dolor";

// Saving
public Godot.Collections.Dictionary<string, Variant> SaveData()
	{
		return new Godot.Collections.Dictionary<string, Variant>
		{
			{"lorem", "ipsum"}
		};
	}

// Loading from that Node
GetNode<singleton>("/root/singleton").SaveHandler.GetValue(this, "lorem");

// Loading from another Node
GetNode<singleton>("/root/singleton").SaveHandler.GetValue(this, "lorem", "dolor");
```
