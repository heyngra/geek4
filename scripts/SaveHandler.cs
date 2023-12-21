using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;
using Microsoft.VisualBasic;

    /*public class SavedObject
{
    public Dictionary<string, Object> SavedValues = new();
    
    public void Save(string name, Object value)
    {
        SavedValues[name] = value;
    }

    public void Save(string[] nameTree, Object value)
    {
        if (nameTree.Length == 1)
        {
            Save(nameTree[0], value);
            return;
        }
        if (!SavedValues.ContainsKey(nameTree[0])) SavedValues[nameTree[0]] = new SavedObject();
        ((SavedObject) SavedValues[nameTree[0]]).Save(nameTree.Skip(1).ToArray(), value);
    }
    
    public Object Get(string name)
    {
        return SavedValues[name];
    }

    public string toJSON()
    {
        return JsonConvert.SerializeObject(SavedValues);
    }
    
    public static SavedObject fromJSON(string json)
    {
        SavedObject savedObject = new();
        savedObject.SavedValues = JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);
        return savedObject;
    }
}*/



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
        GD.Print(savedNodes+"\n\n\n\n");
        scenes.Add(GetNode<Node>("/root"));
        foreach (Node pausedScenesValue in Singleton.PausedScenes.Values) scenes.Add(pausedScenesValue);

        /*foreach (KeyValuePair<string, Godot.Collections.Dictionary<string, Variant>> savedNode in savedNodes)
        {
            foreach (KeyValuePair<string, Variant> savedNode2 in savedNode.Value)
            {
                if (savedNode2.Value.VariantType == Variant.Type.Dictionary)
                {
                    savedNodes[savedNode.Key][savedNode2.Key] = savedNode2.Value.Obj;
                }
            }
        }*/
        
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
        GD.Print(json);
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
    
}
