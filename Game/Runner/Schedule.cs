using System;
using System.Collections.Generic;

namespace Sim.Runner
{
    class Schedule
    {
        public SortedDictionary<int, List<Action<int>>> scheduledActions = new();

        public void Add(int onTick, Action<int> action)
        {
            if (!scheduledActions.ContainsKey(onTick))
            {
                scheduledActions[onTick] = new List<Action<int>>();
            }
            scheduledActions[onTick].Add(action);
        }

        public void Run(int currentTick)
        {
            if (!scheduledActions.ContainsKey(currentTick))
            {
                return;
            }
            foreach (Action<int> action in scheduledActions[currentTick])
            {
                action.Invoke(currentTick);
            }
        }
    }
}