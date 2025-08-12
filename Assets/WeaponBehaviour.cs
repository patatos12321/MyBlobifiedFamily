using System.Linq;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public ProjectileBehaviour Projectile;
    private MobSmokerBehaviour _closestMob;

    public int Cooldown = 1000;
    public int Range = 5;
    private int _currentCooldownFrame = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Super untested piece of code
        Projectile.Range = Range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_closestMob == null || (this.transform.position - _closestMob.gameObject.transform.position).magnitude > Range)
        {
            FindClosestMob();
        }

        if (_closestMob != null) { 
            if ((this.transform.position - _closestMob.gameObject.transform.position).magnitude < Range)
            {
                LookAt(_closestMob);
            }
        }
        else
        {
            ResetRotation();
        }

        _currentCooldownFrame++;
        if (_currentCooldownFrame >= Cooldown && _closestMob != null)
        {
            Shoot();
        }
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    private void FindClosestMob()
    {
        var mobs = FindObjectsByType<MobSmokerBehaviour>(FindObjectsSortMode.None);
        if (mobs.Any())
        {
            _closestMob = mobs[0];
            var closestDistanceMagnitude = (this.transform.position - _closestMob.gameObject.transform.position).magnitude;
            foreach (var mob in mobs.Skip(1))
            {
                var mobDistanceMagnitude = (this.transform.position - mob.gameObject.transform.position).magnitude;
                if (mobDistanceMagnitude < closestDistanceMagnitude)
                {
                    _closestMob = mob;
                    closestDistanceMagnitude = mobDistanceMagnitude;
                }
            }
        }
    }

    private void Shoot()
    {
        _currentCooldownFrame = 0;
        var instance = Instantiate(Projectile, this.transform);
        instance.Weapon = this;
        //todo: review delay when shots miss
        Destroy(instance, 120);
    }

    public void LookAt(MobSmokerBehaviour target)
    {
        _closestMob = target;
        var direction = target.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
