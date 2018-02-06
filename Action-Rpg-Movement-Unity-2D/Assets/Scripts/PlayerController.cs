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
        
        Debug.Log("Normalized:" + input.normalized);
        Debug.Log("Clamped:" + Vector2.ClampMagnitude(input, 1f));
        input = Vector2.ClampMagnitude(input, 1f);
        rigidBody.MovePosition(rigidBody.position + input * moveSpeed);
        //rigidBody.AddForce(input * moveSpeed, ForceMode2D.Force);
    }
}
