using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int Amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var gameManager = FindFirstObjectByType<GameManagerBehaviour>();
            if (gameManager != null)
            {
                gameManager.AddMoney(Amount);
            }

            Destroy(gameObject);
        }
    }
}
