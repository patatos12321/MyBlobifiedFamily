using UnityEngine;

public class PlayerBlobBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float MoveSpeed = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
