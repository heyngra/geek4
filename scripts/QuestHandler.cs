using Godot;
using System;
using System.Collections.Generic;
using Geek4.scripts.quests;

public partial class QuestHandler : Node
{
	public singleton Singleton;
	public string OverrideSaveName = "SavedQuestProgress";
	
	/// <summary>
	/// Quests that are registered in the game, and can be referenced.
	/// REFERNECE ONLY THESE QUESTS, THEY ARE INSTANCED ON _READY
	/// </summary>
	public readonly List<Quest> RegisteredQuests = new();
	
	public override async void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		QuestRegisterer();
		
		// need to wait for Singleton initialize.
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");
		Singleton.SaveHandler.OnLoad.Add(LoadData);
	}
	
	public void RegisterQuest(Quest quest)
	{
		GD.Print("Registered quest: " + quest.QuestName);
		RegisteredQuests.Add(quest);
	}
	
	public void QuestRegisterer()
	{
		RegisterQuest(ExampleQuest.RegisterQuest());
	}
	
	public Quest GetQuestInstance(Type questType)
	{
		foreach (var quest in RegisteredQuests)
		{
			if (quest.GetType() == questType)
			{
				return quest;
			}
		}
		return null;
	}

	public Godot.Collections.Dictionary<string, Variant> SaveData()
	{

		Godot.Collections.Dictionary<string, Variant> savedQuests = new();
		foreach (var quest in RegisteredQuests)
		{
			savedQuests.Add(quest.GetType().ToString(), quest.SaveData());
		}

		return savedQuests;
	}

	public void LoadData()
	{
		// in general, you shouldn't do it, but I guess that's the only way?
		if (Singleton.Save == null)
		{
			GD.Print("XD");return;
		}

		foreach (var questTypeName in Singleton.Save[OverrideSaveName].Keys)
		{
			if (RegisteredQuests.Find(quest1 => quest1.GetType().ToString() == questTypeName) is { } quest)
			{
				quest.LoadData((Godot.Collections.Dictionary<string, Variant>)Singleton.Save[OverrideSaveName][questTypeName]);
			}
		}

	}
}
