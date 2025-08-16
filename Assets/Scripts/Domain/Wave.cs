namespace Assets.Scripts.Domain
{
    //idea: quest types?
    public class Wave
    {
        public int Number;
        public Quest Quest;
        public Wave(int number, Quest quest)
        {
            Number = number;
            Quest = quest;
        }
    }
}
