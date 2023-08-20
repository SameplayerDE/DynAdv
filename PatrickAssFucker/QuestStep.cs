using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker
{
    public class QuestStep
    {
        public string Description { get; set; }
        public Func<bool> IsCompleted { get; set; }
        public Action OnCompletion { get; set; }
        //public List<QuestStep> NextSteps { get; set; } = new List<QuestStep>();
        public bool IsStepCompleted { get; private set; }

        public void CheckCompletion()
        {
            if (!IsStepCompleted &&  IsCompleted())
            {
                OnCompletion?.Invoke();
                IsStepCompleted = true;
                //foreach (var step in NextSteps)
                //{
                //    step.CheckCompletion();
                //}
            }
        }
    }
}
