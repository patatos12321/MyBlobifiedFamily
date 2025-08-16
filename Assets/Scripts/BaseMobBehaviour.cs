using UnityEngine;

public class BaseMobBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private WaveManager _waveManager;

    public GameObject Coin;

    public const int NbFramesBeforeLaunch = 15;
    public const int Speed = 1000;
    public const int MaxHealth = 50;
    public int Strength = 10;
    public int Value = 1;
    private int _currentHealth = 50;

    private int _currentNbFramesBeforeLaunch = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _waveManager = FindFirstObjectByType<WaveManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        _currentNbFramesBeforeLaunch++;
        if (_currentNbFramesBeforeLaunch < NbFramesBeforeLaunch)
        {
            return;
        }

        Vector3 direction;
        if (_waveManager != null)
        {
            direction = (_waveManager.Player.gameObject.transform.position - this.transform.position).normalized;
        }
        else
        {
            //just go towards the center
            direction = (new Vector3() - this.transform.position).normalized;
        }

        _rb.AddForce(new Vector2(direction.x * Speed, direction.y * Speed));
        _currentNbFramesBeforeLaunch = 0;
    }

    public void Push(int force, Vector3 from)
    {
        var direction = (this.transform.position - from).normalized;
        _rb.AddForce(new Vector2(direction.x * force, direction.y * force));
    }

    public void Damage(int dmg)
    {
        _currentHealth -= dmg;
        if (IsDead())
        {
            Die();
        }
    }

    private void Die()
    {
        var coin = Instantiate(Coin);
        coin.transform.position = this.transform.position;
        coin.GetComponent<CoinBehaviour>().Amount = Value;
        Destroy(this.gameObject);
    }

    private bool IsDead() => _currentHealth <= 0;
}
