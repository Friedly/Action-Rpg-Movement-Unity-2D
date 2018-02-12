using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class InputController : MonoBehaviour
{
    #region Input
    private Vector2 input = new Vector2();
    private Vector2 lastInput = new Vector2();
    #endregion

    #region Components
    private MovementController movementController;
    private AnimationController animationController;
    #endregion

    void Start()
    {
        movementController = GetComponent<MovementController>();
        animationController = GetComponent<AnimationController>();

        lastInput = input;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movementController.Direction = input;

        if (animationController != null)
        {
            animationController.animator.SetFloat("inputX", input.x);
            animationController.animator.SetFloat("inputY", input.y);
        }

        if (movementController.IsMoving == true)
        {
            //Scale the animation speed with the normalized value of the current movement speed.
            animationController.animator.speed = ((input.magnitude * movementController.moveSpeed) / (movementController.moveSpeed)) * animationController.animationSpeed;
            //Ensure the set minimal animation speed.
            animationController.animator.speed = Mathf.Clamp(animationController.animator.speed, animationController.minAnimationSpeed, animationController.animationSpeed);

            if (animationController && input != lastInput)
            {
                animationController.animator.SetFloat("lastInputX", lastInput.x);
                animationController.animator.SetFloat("lastInputY", lastInput.y);
            }

            lastInput = input;
        }
        else
        {
            if (animationController != null)
            {
                animationController.animator.speed = animationController.animationSpeed;
            }
        }

        if(animationController != null)
        {
            animationController.animator.SetBool("isMoving", movementController.IsMoving);
        }
    }
}
