using TMPro;
using UnityEngine;

public class PlayerBlobBehaviour : MonoBehaviour
{
    public int Acceleration = 100;
    public float MaxSpeed = 0.5f;

    public WeaponBehaviour[] WeaponBehaviours;
    public int PushbackStrength = 30000;

    public int MaxHealth = 100;

    public TMP_Text CurrentHealthText;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    private Rigidbody2D _rb;
    private Animator _animator;
    private int _invincibilityDelay = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        foreach (var weapon in WeaponBehaviours)
        {
            //todo: Spawn multiple weapons
            var spawnLocation = new Vector3(0.5f, 0, this.gameObject.transform.position.z);
            Instantiate(weapon, spawnLocation, Quaternion.identity, this.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DecayInvincibility();

        Move();

    }

    private void Move()
    {
        var accelerationModificator = 1f;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)))
        {
            accelerationModificator = accelerationModificator / 2;
        }
        var modifiedAcceleration = Acceleration * accelerationModificator;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_rb.linearVelocityX > -MaxSpeed)
            {
                _rb.AddForceX(-modifiedAcceleration);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_rb.linearVelocityX < MaxSpeed)
            {
                _rb.AddForceX(modifiedAcceleration);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (_rb.linearVelocityY > -MaxSpeed)
            {
                _rb.AddForceY(-modifiedAcceleration);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_rb.linearVelocityY < MaxSpeed)
            {
                _rb.AddForceY(modifiedAcceleration);
            }
        }
    }

    private void DecayInvincibility()
    {
        if (_invincibilityDelay <= 0) return;

        _invincibilityDelay--;

        if (_invincibilityDelay <= 0)
        {
            _animator.SetBool("Invincible", false);
        }
    }

    void Update()
    {
        if (CurrentHealthText == null) //testing scenes
            return;

        CurrentHealthText.text = _currentHealth.ToString();
        ColorHealthText(CurrentHealth);
    }

    private void ColorHealthText(int currentHealth)
    {
        if (currentHealth == 0)
        {
            CurrentHealthText.color = CurrentHealthText.color == Color.white ? Color.red : Color.white;//flashes white and red
        }
        else if (currentHealth < 30)
        {
            CurrentHealthText.color = Color.red;
        }
        else if (currentHealth < 50)
        {
            CurrentHealthText.color = Color.yellow;
        }
        else
        {
            CurrentHealthText.color = Color.green;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.TryGetComponent<BaseMobBehaviour>(out var mob))
        {
            if (!collision.collider.TryGetComponent(out mob))
            {
                return;
            }
        }

        if (_invincibilityDelay <= 0)
        {
            TakeDamage(mob.Strength);
        }

        Pushback(mob);
        BecomeInvincible();
    }

    private void BecomeInvincible()
    {
        _invincibilityDelay = 20;
        _animator.SetBool("Invincible", true);
    }

    private void Pushback(BaseMobBehaviour mob)
    {
        mob.Push(PushbackStrength, this.transform.position);
    }

    private void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        if (IsDead()) { Lose(); }
    }

    private static void Lose()
    {
        var gameManager = GameManagerBehaviour.Instance;
        gameManager.Defeat();
    }

    private bool IsDead() => CurrentHealth < 0;//player can be alive at 0 cause why not
}
