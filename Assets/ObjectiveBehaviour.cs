using Assets.Scripts.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective Objective;

    public TMP_Text NbKillsText;
    public Image MobTypePreview;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NbKillsText.text = Objective.NbRequiredKills.ToString();
        MobTypePreview.sprite = Objective.Mob.Sprite;
    }
}
