using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameManager = FindFirstObjectByType<GameManagerBehaviour>();
        var text = GetComponent<TMP_Text>();

        text.text = $"{gameManager.NbCoins} coins";
    }
}
