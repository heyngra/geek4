using System;
using System.Collections.Generic;
using Godot;

namespace Geek4.scripts.quests;

public partial class ExampleQuest : Quest
{
    public ExampleQuest(
        string questName, string questDescription, int experienceReward, int minimumLevel, QuestMilestone[] questMilestones, Action completionCallback = null) : 
        base(questName, questDescription, experienceReward, minimumLevel, questMilestones, completionCallback
    ) {
        
    }

    public static Quest RegisterQuest()
    {

        QuestMilestone milestone1 = new("Milestone 1", "This is the first milestone in a quest",
            new List<MilestoneStep>()
            {
                new("Talk with the Szafka xD", "test_env2"),
                new("Porozmawiaj jeszcze raz z szafką!", "test_env2", () => { GD.Print("hehe ;p"); })
            }, "test_env2");
        
        return new ExampleQuest(
            "Example Quest", 
            "This is an example quest, that is used to show how quests work.", 
            100, 
            0, 
            new[] { milestone1 }
        );
    }
}