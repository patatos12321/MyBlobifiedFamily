using System.Linq;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
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

        var direction = (_closestMob.transform.position - this.transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * Speed, direction.y * Speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //todo: handle different mob types
        if (!collision.collider.TryGetComponent<MobSmokerBehaviour>(out var mob))
        {
            collision.otherCollider.TryGetComponent(out mob);
            if (mob == null)
            {
                return;
            }
        }

        mob.Damage(Damage);
        Destroy(this.gameObject);
    }
}
