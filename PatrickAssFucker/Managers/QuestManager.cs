namespace PatrickAssFucker.Managers;

public class QuestManager
{
    
    private List<Quest> _quests = new();
    public static QuestManager Instance { get; } = new();

    static QuestManager()
    {
        
    }
    
    private QuestManager()
    {
        var demo = new Quest();
        
        demo.Title = "Der verlorene Sohn";
        demo.Identifier = QuestIdentifier.StillbachBlacksmith;

        var demoStep0 = new QuestStep();
        demoStep0.IsCompleted = () =>
            StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "visit.blacksmith.secondFloor");
        
        demo.AddStep(demoStep0);
        
        AddQuest(demo);
    }

    public void AddQuest(Quest quest)
    {
        _quests.Add(quest);
    }

    public Quest? GetQuestById(QuestIdentifier identifier)
    {
        int index;
        for (index = 0; index < _quests.Count; index++)
        {
            var quest = _quests[index];
            if (identifier == quest.Identifier)
            {
                return quest;
            }
        }

        return null;
    }
}