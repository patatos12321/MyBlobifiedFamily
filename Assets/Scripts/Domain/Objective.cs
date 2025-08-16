namespace Assets.Scripts.Domain
{
    public class Objective
    {
        public int NbRequiredKills { get; set; }
        private int _nbKilled = 0;
        public int NbKilled => _nbKilled;
        public BaseMobBehaviour Mob { get; set; }
        public bool IsCompleted => NbKilled >= NbRequiredKills;

        public Objective(int nbKills, BaseMobBehaviour mob) 
        {
            NbRequiredKills = nbKills;
            Mob = mob;
        }

        public void RegisterKill()
        {
            _nbKilled++;
        }
    }
}
