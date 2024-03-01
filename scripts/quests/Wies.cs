using System;
using System.Collections.Generic;
using Godot;

namespace Geek4.scripts.quests;

public partial class Wies : Quest
{
    public Wies(
        string questName, string questDescription, int experienceReward, int minimumLevel, QuestMilestone[] questMilestones, Action completionCallback = null) : 
        base(questName, questDescription, experienceReward, minimumLevel, questMilestones, completionCallback
        ) {
        
    }

    public static Quest RegisterQuest()
    {

        QuestMilestone milestone1 = new("Wizyta na wsi", "",
            new List<MilestoneStep>()
            {
                new("Pochwal się ojcu wynikiem egzaminu", "Dom"),
                new("Spakuj swoje ubrania", "Dom"),
                new("Pojedź na wieś", "Wieś"),
                new("Rozejrzyj się po wsi", "Wieś"),
                new("Pomóż dziadkowi (koniec gry)", "Wieś")
            }, "");
        
        return new Wies(
            "Wieś", 
            "W życiu czasami trzeba odpocząć.", 
            100, 
            0, 
            new[] { milestone1 }
        );
    }
}