using Assets.Scripts.Domain;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int Amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Player))
        {
            var gameManager = GameManagerBehaviour.Instance;
            if (gameManager != null)
            {
                gameManager.AddMoney(Amount);
            }

            Destroy(gameObject);
        }
    }
}
