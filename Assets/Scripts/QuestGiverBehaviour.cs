using Assets.Scripts.Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestGiverBehaviour : MonoBehaviour
{
    private GameManagerBehaviour _gameManagerBehaviour = GameManagerBehaviour.Instance;
    private List<Quest> Quests = new();
    private Quest selectedQuest = null;
    public BaseMobBehaviour[] AvailableMobs;//todo: automatically fetch from mobs folder
    public ProposedQuestBehaviour ProposedQuestBehaviourPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateQuests();
    }

    private void GenerateQuests()
    {
        if (IsFirstWave())
        {
            Quests.Add(new Quest(new List<Objective>()
            {
                new (6, AvailableMobs.First())
            }
            ));
        }

        foreach (Quest quest in Quests) 
        {
            var instantiatedQuestGiver = Instantiate(ProposedQuestBehaviourPrefab, this.transform);
            instantiatedQuestGiver.Quest = quest;
            instantiatedQuestGiver.SetQuestGiverParent(this);
        }

        selectedQuest = Quests.FirstOrDefault();
    }

    private bool IsFirstWave()
    {
        return _gameManagerBehaviour.WaveNumber == 0;
    }

    public void SelectQuest(Quest quest)
    {
        selectedQuest = quest;
    }

    public void DepartOnQuest()
    {
        _gameManagerBehaviour.DepartOnQuest(selectedQuest);
    }
}
