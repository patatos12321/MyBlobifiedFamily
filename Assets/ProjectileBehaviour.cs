using System.Linq;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public WeaponBehaviour Weapon;
    public int Pushback = 1000;
    public int Damage = 10;
    public int Speed = 100;
    public int Range = 5;

    private MobSmokerBehaviour _closestMob;

    private void Start()
    {
        //todo: find every type of mobs
        var mobs = FindObjectsByType<MobSmokerBehaviour>(FindObjectsSortMode.None);
        if (!mobs.Any())
        {
            Destroy(this.gameObject);
            return;
        }

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

        if (closestDistanceMagnitude > Range)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Weapon != null)
        {
            Weapon.LookAt(_closestMob);
        }
        LookAt(_closestMob.transform);

        var direction = (_closestMob.transform.position - this.transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * Speed, direction.y * Speed));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("OutOfBounds"))
        {
            Destroy(this.gameObject);
            return;
        }

        //todo: handle different mob types
        if (!collider.TryGetComponent<MobSmokerBehaviour>(out var mob))
        {
            return;
        }

        mob.Damage(Damage);
        mob.Push(Pushback, this.transform.position);
        Destroy(this.gameObject);
    }

    public void LookAt(Transform target)
    {
        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
