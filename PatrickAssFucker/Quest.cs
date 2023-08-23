using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker
{
    public enum QuestIdentifier
    {
        StillbachBlacksmith
    }
    
    public class Quest
    {

        public QuestIdentifier Identifier;
        public string Title { get; set; }
        public List<QuestStep> Steps { get; private set; } = new List<QuestStep>();
        public bool IsCompleted => Steps.All(s => s.IsCompleted());

        public void AddStep(QuestStep step)
        {
            Steps.Add(step);
        }

        public void CheckCompletion()
        {
            foreach (var step in Steps)
            {
                step.CheckCompletion();
            }
        }

        public double CompletionPercentage()
        {
            if (Steps.Count == 0) return 0;
            return (Steps.Count(s => s.IsCompleted()) / (double)Steps.Count) * 100;
        }

    }
}
