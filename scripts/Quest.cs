using Godot;
using System;
using System.Collections.Generic;

public class MilestoneStep
{
    /// <summary>
    /// Name of the current quest's step.
    /// </summary>
    public string StepName;
    
    /// <summary>
    /// Action that is called after completing the step.
    /// </summary>
    public Action StepCompletion;

    /// <summary>
    /// The location of the step.
    /// </summary>
    public string StepLocation;
    public bool StepCompleted = false;
    
    public MilestoneStep(string stepName, string stepLocation="", Action stepCompletion=null)
    {
        StepName = stepName;
        StepCompletion = stepCompletion;
        StepLocation = stepLocation;
    }
}


public class QuestMilestone
{
    /// <summary>
    /// Name of the current quest's milestone.
    /// </summary>
    public string MilestoneName;
    
    /// <summary>
    /// Description of the current quest's milestone.
    /// </summary>
    public string MilestoneDescription;
    
    /// <summary>
    /// Action that is called after completing the milestone.
    /// </summary>
    public Action MilestoneCompletion;

    /// <summary>
    /// The location of the step.
    /// </summary>
    public string MilestoneLocation;
    public bool MilestoneCompleted = false;
    public Quest BaseQuest;
    public readonly List<MilestoneStep> MilestoneSteps = new();
    public int CurrentStep = 0;
    
    public MilestoneStep GetCurrentStep()
    {
        if (MilestoneCompleted) return null;
        return MilestoneSteps[CurrentStep];
    }
    
    public void CompleteStep()
    {
        MilestoneStep currentStep = GetCurrentStep();
        currentStep.StepCompletion?.Invoke();
        currentStep.StepCompleted = true;
        if (CurrentStep == MilestoneSteps.Count - 1)
        {
            BaseQuest.CompleteMilestone();
            return;
        }
        CurrentStep++;
    }

    /// <summary>
    /// Do not call this method. Call BaseQuest.CompleteMilestone() instead.
    /// </summary>
    private void CompleteMilestone()
    {
        MilestoneCompleted = true;
        MilestoneCompletion?.Invoke();
    }


    public QuestMilestone(string milestoneName, string milestoneDescription, List<MilestoneStep> milestoneSteps, string milestoneLocation="", Action milestoneCompletion=null)
    {
        MilestoneName = milestoneName;
        MilestoneDescription = milestoneDescription;
        MilestoneCompletion = milestoneCompletion;
        MilestoneLocation = milestoneLocation;
        foreach (var milestoneStep in milestoneSteps)
        {
            MilestoneSteps.Add(milestoneStep);
        }
    }
}

/*
 * Class used as a base component for all quests.
 * quests/ExampleQuest.cs
 */
public partial class Quest : Node
{
    /// <summary>
    /// Quest's name.
    /// </summary>
    public string QuestName;

    /// <summary>
    /// The general description of a quest (not the quest step).
    /// </summary>
    public string QuestDescription;

    /// <summary>
    /// The current milestone of a quest.
    /// </summary>
    public int QuestMilestone { get; set; } = 0;

    /// <summary>
    /// Is the quest completed?
    /// </summary>
    public bool QuestCompleted = false;

    /// <summary>
    /// Experience reward for completing the quest.
    /// </summary>
    public int ExperienceReward;

    /// <summary>
    /// List of quests that need to be completed before starting this quest.
    /// </summary>
    public List<Quest> PrerequisitQuests = new();

    /// <summary>
    /// Minimum level required to start the quest.
    /// </summary>
    public int MinimumLevel;
    public bool IsStarted = false;

    public readonly List<QuestMilestone> QuestMilestones = new();

    /// <summary>
    /// Action that is called after completing quest.
    /// </summary>
    public Action CompletionCallback;

    /// <summary>
    /// Constructor
    /// </summary>
    public Quest(string questName, string questDescription, int experienceReward, int minimumLevel, QuestMilestone[] questMilestones, Action completionCallback=null)
    {
        QuestName = questName;
        QuestDescription = questDescription;
        ExperienceReward = experienceReward;
        MinimumLevel = minimumLevel;
        CompletionCallback = completionCallback;
        foreach (var questMilestone in questMilestones)
        {
            QuestMilestones.Add(questMilestone);
            questMilestone.BaseQuest = this;
        }
    }

    public QuestMilestone GetCurrentMilestone()
    {
        if (QuestCompleted) return null;
        return QuestMilestones[QuestMilestone];
    }
    public void StartQuest()
    {
        PrerequisitQuests.ForEach(quest => { if (!quest.QuestCompleted) return; });
        IsStarted = true;
    }
    public void CompleteQuest()
    {
        QuestCompleted = true;
        CompletionCallback?.Invoke();
    }

    public void CompleteStep(int currentMilestone, int currentStep, bool forceComplete=false)
    {
        if (QuestMilestone != currentMilestone || GetCurrentMilestone().CurrentStep != currentStep)
        {
            GD.PushWarning("Current milestone and set milestone dismatch. ");
            if (!forceComplete) return;
        }
        if (GetCurrentMilestone().GetCurrentStep().StepCompleted)
        {
            GD.PushWarning("Step already completed. ");
            if (!forceComplete) return;
        }
        GetCurrentMilestone().CompleteStep();
        
    }
    public string GetStepName()
    {
        return GetCurrentMilestone()?.GetCurrentStep()?.StepName;
    }
    public int GetStepNumber()
    {
        return GetCurrentMilestone()?.MilestoneSteps.IndexOf(GetCurrentMilestone()?.GetCurrentStep()) ?? -1;
    }
    
    public MilestoneStep GetStepFromMilestone(int milestone, int step)
    {
        return QuestMilestones[milestone]?.MilestoneSteps[step];
    } 
    
    public void CompleteMilestone()
    {
        QuestMilestone currentMilestone = GetCurrentMilestone();
        currentMilestone.MilestoneCompletion?.Invoke();
        currentMilestone.MilestoneCompleted = true;
        if (QuestMilestone == QuestMilestones.Count - 1)
        {
            CompleteQuest();
            return;
        }
        QuestMilestone++;
    }
    /// <summary>
    /// Checks whether the current step (and milestone) are the same as the given ones.
    /// </summary>
    /// <param name="currentMilestone">Index of checked milestone</param>
    /// <param name="currentStep">Index of checked step</param>
    /// <returns>True if said milestone is in the quest right now.</returns>
    public bool IsStepMilestone(int currentMilestone, int currentStep) { return QuestMilestone == currentMilestone && GetCurrentMilestone()?.CurrentStep == currentStep; }
    /// <summary>
    /// Checks whether the current step (and milestone) are the same as the given ones.
    /// </summary>
    /// <param name="currentMilestone">Index of checked milestone</param>
    /// <param name="currentStepName">Name of checked step</param>
    /// <returns>True if said milestone is in the quest right now.</returns>
    public bool IsStepMilestone(int currentMilestone, string currentStepName) { return QuestMilestone == currentMilestone && GetCurrentMilestone()?.GetCurrentStep()?.StepName == currentStepName; }
    /// <summary>
    /// Checks whether the current step (and milestone) are the same as the given ones.
    /// </summary>
    /// <param name="currentMilestoneName">Name of checked milestone</param>
    /// <param name="currentStep">Index of checked step</param>
    /// <returns>True if said milestone is in the quest right now.</returns>
    public bool IsStepMilestone(string currentMilestoneName, int currentStep) { return GetCurrentMilestone()?.MilestoneName == currentMilestoneName && GetCurrentMilestone()?.CurrentStep == currentStep; }
    /// <summary>
    /// Checks whether the current step (and milestone) are the same as the given ones.
    /// </summary>
    /// <param name="currentMilestoneName">Name of checked milestone</param>
    /// <param name="currentStepName">Name of checked step</param>
    /// <returns>True if said milestone is in the quest right now.</returns>
    public bool IsStepMilestone(string currentMilestoneName, string currentStepName) { return GetCurrentMilestone()?.MilestoneName == currentMilestoneName && GetCurrentMilestone()?.GetCurrentStep()?.StepName == currentStepName; }

    public Godot.Collections.Dictionary<String, Variant> SaveData()
    {
        Godot.Collections.Dictionary<String, Variant> questData = new();
        Godot.Collections.Array<Godot.Collections.Dictionary<String, Variant>> _QuestMilestones = new();
        foreach (var questMilestone in QuestMilestones)
        {
            Godot.Collections.Array<Godot.Collections.Dictionary<String, Variant>> _MilestoneSteps = new();
            foreach (var milestoneStep in questMilestone.MilestoneSteps)
            {
                _MilestoneSteps.Add(new Godot.Collections.Dictionary<String, Variant>
                {
                    //step data
                    {"index", questMilestone.MilestoneSteps.IndexOf(milestoneStep)},
                    {"StepCompleted", milestoneStep.StepCompleted},
                });
            }
            _QuestMilestones.Add(new Godot.Collections.Dictionary<String, Variant>
            {
                // milestone data
                {"index", QuestMilestones.IndexOf(questMilestone)},
                {"CurrentStep", questMilestone.CurrentStep},
                {"MilestoneCompleted", questMilestone.MilestoneCompleted},
                {"MilestoneSteps", _MilestoneSteps},
            });
        }
        questData.Add("QuestName", QuestName);
        questData.Add("IsStarted", IsStarted);
        questData.Add("QuestCompleted", QuestCompleted);
        questData.Add("QuestMilestones", _QuestMilestones);
        questData.Add("QuestMilestone", QuestMilestone);
        return questData;
    }

    public void LoadData(Godot.Collections.Dictionary<String, Variant> data)
    {
        GD.Print(data);
        QuestName = (string)data["QuestName"];
        IsStarted = (bool)data["IsStarted"];
        QuestCompleted = (bool)data["QuestCompleted"];
        QuestMilestone = (int)data["QuestMilestone"];
        foreach (var questMilestone in (Godot.Collections.Array<Godot.Collections.Dictionary<String, Variant>>)data["QuestMilestones"])
        {
            var index = (int)questMilestone["index"];
            foreach (var milestoneStep in (Godot.Collections.Array<Godot.Collections.Dictionary<String, Variant>>)questMilestone["MilestoneSteps"])
            {
                var index2 = (int)milestoneStep["index"];
                // step data
                QuestMilestones[index].MilestoneSteps[index2].StepCompleted = (bool)milestoneStep["StepCompleted"];
            }
            // milestone data
            QuestMilestones[index].CurrentStep = (int)questMilestone["CurrentStep"];
            QuestMilestones[index].MilestoneCompleted = (bool)questMilestone["MilestoneCompleted"];            
        }
    }
}
