using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Movement
    [Header("Movement")]
    [Tooltip("(Maximal) Movement speed in unity units per second.")]
    public float moveSpeed = 5f;
    [Tooltip("If set to true it allows the player to move faster diagonal direction.")]
    public bool allowDiagonalSpeedUp = false;
    #endregion

    #region Animation
    [Header("Animation")]
    [Tooltip("(Maximal) Animation speed in percent: < 1 slower, > 1 faster, 1 = normal speed")]
    public float animationSpeed = 1f;
    [Tooltip("Minimal animation speed in percent. Only relevant while using a controller.")]
    public float minAnimationSpeed = 0f;
    #endregion

    #region Status
    private bool isMoving = false;
    #endregion

    #region Input
    private Vector2 input = new Vector2();
    private Vector2 lastInput = new Vector2();
    #endregion

    #region Components
    private Rigidbody2D rigidBody;
    private Animator animator;
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        lastInput = input;
        animator.speed = animationSpeed;
    }

    void Update ()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //Ensure that the input magnitude is never greater than 1
        //But allow magnitudes lower than 1
        if(allowDiagonalSpeedUp == false)
        {
            input = Vector2.ClampMagnitude(input, 1f);
        }

        #region AnimationControl
        animator.SetFloat("inputX", input.x);
        animator.SetFloat("inputY", input.y);
        
        if (input.x != 0f || input.y != 0f)
        {
            isMoving = true;

            //Scale the animation speed with the normalized value of the current movement speed.
            animator.speed = ((input.magnitude * moveSpeed) / (moveSpeed)) * animationSpeed;
            //Ensure the set minimal animation speed.
            animator.speed = Mathf.Clamp(animator.speed, minAnimationSpeed , animationSpeed);

            if(input != lastInput)
            {
                animator.SetFloat("lastInputX", lastInput.x);
                animator.SetFloat("lastInputY", lastInput.y);
            }

            lastInput = input;
        }
       else
       {
            isMoving = false;

            animator.speed = animationSpeed;

            animator.SetFloat("lastInputX", lastInput.x);
            animator.SetFloat("lastInputY", lastInput.y);
       }

        animator.SetBool("isMoving", isMoving);
        #endregion
    }

    private void FixedUpdate ()
    {
        rigidBody.MovePosition(rigidBody.position + input * moveSpeed);
    }
}
