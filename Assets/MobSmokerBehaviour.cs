using UnityEngine;

public class MobSmokerBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private WaveManager _waveManager;

    public const int NbFramesBeforeLaunch = 15;
    public const int Speed = 1000;
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
        _currentNbFramesBeforeLaunch++;
        if (_currentNbFramesBeforeLaunch < NbFramesBeforeLaunch)
        {
            return;
        }
        var direction = (_waveManager.Player.gameObject.transform.position - this.transform.position).normalized;
        _rb.AddForce(new Vector2(direction.x * Speed, direction.y * Speed));
        _currentNbFramesBeforeLaunch=0;
    }
}
