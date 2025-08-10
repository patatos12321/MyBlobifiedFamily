using System.Linq;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public ProjectileBehaviour Projectile;
    public int Cooldown = 1000;
    private int _currentCooldownFrame = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentCooldownFrame++;
        if (_currentCooldownFrame >= Cooldown)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _currentCooldownFrame = 0;
        var instance = Instantiate(Projectile, this.transform);
        //todo: review delay when shots miss
        Destroy(instance, 120);
    }
}
