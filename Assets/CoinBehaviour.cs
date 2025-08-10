using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int Amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManagerBehaviour>().AddMoney(Amount);
            Destroy(gameObject);
        }
    }
}
