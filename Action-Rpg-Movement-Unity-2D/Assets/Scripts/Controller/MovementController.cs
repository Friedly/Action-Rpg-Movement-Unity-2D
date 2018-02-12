using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    [Tooltip("(Maximal) Movement speed in unity units per second.")]
    public float moveSpeed = 5f;
    public bool normalizeDirection = true;

    private Vector2 direction;
    public Vector2 Direction
    {
        set
        {
            if(normalizeDirection == true)
            {
                direction = Vector2.ClampMagnitude(value, 1f);
            }
            else
            {
                direction = value;
            }
        }
        get { return direction;  }
    }
    #endregion

    #region Status
    private bool isMoving = false;
    public bool IsMoving
    {
        get { return isMoving; }
    }
    #endregion

    #region Components
    private Rigidbody2D rigidBody;
    #endregion

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        direction = new Vector2(0f, 0f);
    }

    private void Update()
    {
        if(direction.magnitude > 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
