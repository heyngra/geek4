using System;
using System.Collections.Generic;
using Godot;

namespace Geek4.scripts.quests;

public partial class ProblematyczneDziecinstwo : Quest
{
    public ProblematyczneDziecinstwo(
        string questName, string questDescription, int experienceReward, int minimumLevel, QuestMilestone[] questMilestones, Action completionCallback = null) : 
        base(questName, questDescription, experienceReward, minimumLevel, questMilestones, completionCallback
        ) {
        
    }

    public static Quest RegisterQuest()
    {

        QuestMilestone milestone1 = new("Zofia", "\"Moja ukochnana siostrzyczka...\"",
            new List<MilestoneStep>()
            {
                new("Przesłuchaj scenę śmierci Marii", "hospital_room_1"),
                new("Wyjdź ze swojego pokoju!", "room_maria")
            }, "");

        QuestMilestone milestone2 = new("Dom", "Muszę rozejrzeć się po swoim domu", new()
        {
            new("Odwiedź pokój rodziców", "Dom Marii"),
            new("Porozmawiaj z ojcem", "Dom Marii"),
        }, "Dom Marii");

        QuestMilestone milestone3 = new("Matka", "Czy z nią będzie wszystko dobrze?", new()
        {
            new("Wyjdź z domu", "Dom Marii"),
            new("Pojedź z ojcem do szpitala"),
            new("Znajdź pokój matki"),
            new("Porozmawiaj z chorą matką")
        });
        
        return new ProblematyczneDziecinstwo(
            "Problematyczne dzieciństwo", 
            "Maria przeżywała piekło w dzieciństwie. Czy temu podołasz?", 
            100, 
            0, 
            new[] { milestone1, milestone2, milestone3 }
        );
    }
}