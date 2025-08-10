using UnityEngine;

public class PlayerBlobBehaviour : MonoBehaviour
{
    public float MoveSpeed = 0.3f;
    public WeaponBehaviour[] WeaponBehaviours;

    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        var newPosition = _rb.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= MoveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += MoveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= MoveSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += MoveSpeed;
        }
        _rb.MovePosition(newPosition);
    }
}
