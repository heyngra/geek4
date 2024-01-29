using Godot;
using System;
using Godot.Collections;
using CollectionExtensions = System.Collections.Generic.CollectionExtensions;

public partial class QuestList : ItemList
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
	public Dictionary<string, Quest> QuestsInList = new();
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetQuestPreview(Quest quest = null)
	{
		GetNode<Label>("/root/Gui/QuestMenu/QuestControl/PanelContainer/Control2/PanelContainer/VBoxContainer/QuestName").Text = quest?.QuestName ?? "";
		GetNode<Label>("/root/Gui/QuestMenu/QuestControl/PanelContainer/Control2/PanelContainer/VBoxContainer/QuestDescription").Text = quest?.QuestDescription ?? "";
		GetNode<Label>("/root/Gui/QuestMenu/QuestControl/PanelContainer/Control2/PanelContainer/VBoxContainer/MilestoneName").Text = quest?.GetCurrentMilestone()?.MilestoneDescription ?? "";
		GetNode<Label>("/root/Gui/QuestMenu/QuestControl/PanelContainer/Control2/PanelContainer/VBoxContainer/StepName").Text = quest?.GetStepName() ?? "";
	}
	public void _on_visibility_changed()
	{
		if (Singleton == null) return;
		Clear();
		QuestsInList.Clear();
		SetQuestPreview();
		Singleton.QuestHandler.RegisteredQuests.ForEach(quest =>
		{
			if (quest.IsStarted && !quest.QuestCompleted)
			{
				AddItem(quest.QuestName);
				CollectionExtensions.TryAdd(QuestsInList, quest.QuestName, quest);
			}
		});
		if (QuestsInList.Count > 0)
		{
			Select(0);
			_on_item_selected(0);
		}
	}

	public void _on_item_selected(int index)
	{
		if (QuestsInList.Count > 0)
		{
			SetQuestPreview(QuestsInList[GetItemText(index)]);
		}
	}
}
