using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class singleton : Node
{
    public Node CurrentScene { get; set; }
    public maria_2 Maria { get; set; }
    public scene_transition Transition { get; set; }

    /// <summary>
    /// Array of paths to objects that shouldn't be instanced when changing scenes. An example could be enemies.
    /// Warning: Deleted object will still use _Ready() function.
    /// </summary>
    public HashSet<NodePath> DeletedObjects = new HashSet<NodePath>();

    /// <summary>
    /// A dictionary, that consists of paused scenes. These scenes are now removed, but they are hold in memory. It is not recommended to modify this dictionary, as removal of scenes might cause a memory leak.
    /// </summary>
    public Godot.Collections.Dictionary<string, Node> PausedScenes = new Godot.Collections.Dictionary<string, Node>();
    
    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
        Transition = GetNode<scene_transition>("/root/SceneTransition");
    }
    
    /// <summary>
    ///   Use this method to change the scene. It transfers character, and respects dead creatures.
    /// </summary>
    /// <param name="path">Scene path</param>
    /// <param name="transition">Transition used in changing scene</param>
    public void GotoScene(string path, string transition = "dissolve")
    {
        CallDeferred(MethodName.DeferredGotoScene, path, transition);
    }
    /// <summary>
    /// Use this method to pause the current scene, and then 
    /// </summary>
    /// <param name="path">Path to scene that will show up after pausing</param>
    /// <param name="transition">Transition used in changing scene</param>
    public void PauseScene(string path, string transition = "dissolve")
    {
        CallDeferred(MethodName.DeferredPauseScene, path, transition);
    }
    /// <summary>
    /// Gets all children of a node, recursively.
    /// </summary>
    /// <param name="mainPath">Node that you are getting children from</param>
    /// <returns>Generator, that goes through each Node.</returns>
    public static IEnumerable<Node> GetAllChildren(Node mainPath)
    {
        foreach (var n in mainPath.GetChildren())
        {
            yield return n;
            foreach (var c in GetAllChildren(n))
            {
                yield return c;
            }
        }
    }

    public async void DeferredGotoScene(string path, string transition)
    {

        maria_2 maria = Maria;
        if (maria == null)
        {
            foreach (var child in GetAllChildren(GetTree().Root))
            {
                GD.Print("Another child" + child);
                if (child is maria_2)
                {
                    maria = (maria_2)child;
                    Maria = maria;
                }
            }
        }
        GD.Print(maria);
        await ToSignal(GetTree().CreateTimer(Transition.LoadTransition("dissolve")), "timeout"); // may sometimes generate errors(?)
        GD.Print("animation finished1");
        maria?.GetParent().RemoveChild(maria); // maria? == if (maria != null)
        PackedScene nextScene = null;
        
        if (PausedScenes.ContainsKey(CurrentScene.SceneFilePath))
        {
            GetTree().Root.RemoveChild(CurrentScene);
            GD.Print("Removed scene "+CurrentScene.SceneFilePath+" from root as a part of pausing.");
        }
        else
        {
            GD.Print("Freed scene "+CurrentScene.SceneFilePath+" as a part of changing scenes.");
            CurrentScene.Free();
        }
        
        if (PausedScenes.TryGetValue(path, out var scene))
        {
            CurrentScene = scene;
            GD.Print("Resumed scene "+path+" as a part of changing scenes.");
            PausedScenes.Remove(path);
        }
        else
        {
            nextScene = (PackedScene)GD.Load(path);
        
            CurrentScene = nextScene.Instantiate();
            GD.Print("Instantiated scene "+path+" as a part of changing scenes.");
        }
        
        GetTree().Root.AddChild(CurrentScene);
        
        if (nextScene != null) // the new scene was not paused, but is Instantiated.
        {
            foreach (var child in GetAllChildren(CurrentScene))
            {
                if (child.IsQueuedForDeletion()) continue;
                if (DeletedObjects.Contains(child.GetPath()))
                {
                    child.QueueFree();
                }
            }
        }
        
        if (maria!=null)
        {
            CurrentScene.AddChild(maria);
            maria.EmitSignal("SceneChange");
        }

        GetTree().CurrentScene = CurrentScene;

        Transition.UnloadTransition("dissolve");
    }

    public void DeferredPauseScene(string path, string transition)
    {
        PausedScenes.Add(CurrentScene.SceneFilePath, CurrentScene);
        GD.Print("Paused scene "+CurrentScene.SceneFilePath);
        DeferredGotoScene(path, transition);
    }
}