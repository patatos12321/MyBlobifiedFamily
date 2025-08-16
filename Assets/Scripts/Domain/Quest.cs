using System.Collections.Generic;

namespace Assets.Scripts.Domain
{
    public class Quest
    {
        public readonly List<Objective> Objectives;

        public Quest(List<Objective> objectives)
        {
            Objectives = objectives;
        }
    }
}
