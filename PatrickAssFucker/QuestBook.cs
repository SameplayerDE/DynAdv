using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker
{
    public class QuestBook
    {
        public List<Quest> ActiveQuests { get; private set; } = new List<Quest>();
        public List<Quest> CompletedQuests { get; private set; } = new List<Quest>();

        // Method to add a quest to the quest book.
        public void AcceptQuest(Quest quest)
        {
            if (!ActiveQuests.Contains(quest))
            {
                ActiveQuests.Add(quest);
            }
        }

        // Method to mark a quest as completed and move it to the completed list.
        public void CompleteQuest(Quest quest)
        {
            if (ActiveQuests.Contains(quest))
            {
                ActiveQuests.Remove(quest);
                CompletedQuests.Add(quest);
            }
        }

        // Method to check the completion status of all active quests.
        public void CheckQuestCompletion()
        {
            for (int i = ActiveQuests.Count - 1; i >= 0; i--)
            {
                Quest? quest = ActiveQuests[i];
                quest.CheckCompletion();
                if (quest.IsCompleted)
                {
                    CompleteQuest(quest);
                }
            }
        }

        // Method to display all active quests.
        public void DisplayActiveQuests()
        {
            if (!ActiveQuests.Any())
            {
                Console.WriteLine("No active quests.");
                return;
            }

            Console.WriteLine("Active Quests:");
            foreach (var quest in ActiveQuests)
            {
                Console.WriteLine($"{quest.Title} - {quest.CompletionPercentage():0.00}% completed");
            }
        }

        // Method to display all completed quests.
        public void DisplayCompletedQuests()
        {
            if (!CompletedQuests.Any())
            {
                Console.WriteLine("No completed quests.");
                return;
            }

            Console.WriteLine("Completed Quests:");
            foreach (var quest in CompletedQuests)
            {
                Console.WriteLine(quest.Title);
            }
        }
    }

}
