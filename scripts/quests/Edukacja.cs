using System;
using System.Collections.Generic;
using Godot;

namespace Geek4.scripts.quests;

public partial class Edukacja : Quest
{
    public Edukacja(
        string questName, string questDescription, int experienceReward, int minimumLevel, QuestMilestone[] questMilestones, Action completionCallback = null) : 
        base(questName, questDescription, experienceReward, minimumLevel, questMilestones, completionCallback
        )
    {
    }

    public static Quest RegisterQuest()
    {

        QuestMilestone milestone1 = new("Zapis", "Musze się zapisać na zajęcia.",
            new List<MilestoneStep>()
            {
                new("Wejdź do szkoły", "Szkoła"),
                new("Dowiedz się, gdzie jest dyrektor", "Szkoła"),
                new("Zapisz się u dyrektora", "Pokój dyrektora")
            }, "Szkoła");

        QuestMilestone milestone2 = new("Egzamin", "Dam sobię rade!", new()
        {
            new("Wejdź do pokoju nr. 4", "Szkoła"),
            new("Porozmawiaj z profesorem", "Sala nr. 4"),
            new("Zdaj egzamin.", "Sala nr. 4"),
            new("Wyjdź ze szkoły", "Szkoła"),
        });
        
        
        Quest quest = new Edukacja(
            "Edukacja", 
            "Nauka jest ważna. Ale czy nauczanie jest takie proste podczas zaborów?", 
            100, 
            0, 
            new[] { milestone1, milestone2 }
        );
        
        
        return quest;
    }
}