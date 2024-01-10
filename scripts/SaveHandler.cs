using Godot;
using System;
using System.Collections.Generic;

public partial class SaveHandler : Node
{
    private singleton Singleton;

    public override void _Ready()
    {
        Singleton = GetNode<singleton>("/root/Singleton");
    }
    
    public void SaveGame()
    {
        using var saveGame = FileAccess.Open("user://save1.dat", FileAccess.ModeFlags.Write);
        List<Node> scenes = new();
        Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> savedNodes = Singleton.Save ?? new();
        scenes.Add(GetNode<Node>("/root"));
        foreach (Node pausedScenesValue in Singleton.PausedScenes.Values) scenes.Add(pausedScenesValue);
        
        foreach (Node scene in scenes)
        {
            foreach (Node node in singleton.GetAllChildren(scene))
            {
                if (node.HasMethod("SaveData"))
                {
                    var response = node.Call("SaveData");
                    var dict = (Godot.Collections.Dictionary<string, Variant>)response;
                    String saveName = node.GetPath() ?? String.Empty;
                    if (node.Get("OverrideSaveName").Obj != null)
                    {
                        saveName = (string)node.Get("OverrideSaveName").Obj;
                    }

                    if (saveName != null) savedNodes[saveName] = dict;
                }
            }
        }
        
        savedNodes["unique"] = new Godot.Collections.Dictionary<string, Variant>
        {
            {"lastScene", Singleton.CurrentScene.SceneFilePath}
        };
        
        string json = Json.Stringify(savedNodes);
        saveGame.StoreString(json);
    }

    public void LoadGame()
    {
        using var saveGame = FileAccess.Open("user://save1.dat", FileAccess.ModeFlags.Read);
        
        string jsonString = saveGame.GetAsText();
        
        var json = new Json();
        var parseResult = json.Parse(jsonString);
        
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
            return;
            
        }


        var save = new Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>>((Godot.Collections.Dictionary)json.Data);
        
        Singleton.Save = save;
    }

    public object GetValue(Node self, string key, string forceName = null)
    {
        //String name = forceName ?? self.GetPath() + self.SceneFilePath;
        String name;

        if (forceName != null)
        {
            name = forceName;
        }
        
        else if (self.Get("OverrideSaveName").Obj != null)
        
        {
            name = (string)self.Get("OverrideSaveName").Obj;
        }
        
        else
        {
            name = self.GetPath() ?? "gfjkdlgjfdklgjfdklgfdsjl;gfdsjkglfdsjgkl;sdf";
        }

        if (Singleton.Save == null)
        {
            Singleton.Save = new();
        }
        if (!Singleton.Save.ContainsKey(name!)) return null;
        var callerObject = Singleton.Save[name!];
        return callerObject[key];

    }

    public void SetUniqueValue(string key, Object value)
    {
        Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> savedNodes = Singleton.Save ?? new();
        savedNodes["unique"][key] = (Variant)value;
    }
    
}
