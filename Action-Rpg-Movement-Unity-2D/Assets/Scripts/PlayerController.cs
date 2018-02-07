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
        //Test this for joystick analog controls whether it feels better to remap the low/mid range.
        //input.x = Mathf.Pow(input.x, 2f) * Mathf.Sign(input.x);
        //input.y = Mathf.Pow(input.y, 2f) * Mathf.Sign(input.y);

        //Does this work with the remapping?
        input = Vector2.ClampMagnitude(input, 1f);
        rigidBody.MovePosition(rigidBody.position + input * moveSpeed);
    }
}
