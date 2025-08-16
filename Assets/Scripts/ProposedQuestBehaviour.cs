using Assets.Scripts.Domain;
using UnityEngine;
using UnityEngine.UI;

public class ProposedQuestBehaviour : MonoBehaviour
{
    private QuestGiverBehaviour _questGiverBehaviour;
    public ObjectiveBehaviour ObjectivePrefab;
    public Quest Quest;

    public void SetQuestGiverParent(QuestGiverBehaviour questGiverBehaviour)
    {
        _questGiverBehaviour = questGiverBehaviour;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var objective in Quest.Objectives)
        {
            var instantiatedPrefab = Instantiate(ObjectivePrefab, this.transform);
            instantiatedPrefab.Objective = objective;
        }

        this.GetComponent<Button>().onClick.AddListener(QuestSelected);
    }

    void QuestSelected()
    {
        _questGiverBehaviour.SelectQuest(Quest);
    }
}
