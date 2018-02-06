using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;

    private Vector2 input = new Vector2();

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate ()
    {
        rigidBody.MovePosition(rigidBody.position + input.normalized * moveSpeed);
        //rigidBody.AddForce(input * moveSpeed, ForceMode2D.Force);
    }
}
