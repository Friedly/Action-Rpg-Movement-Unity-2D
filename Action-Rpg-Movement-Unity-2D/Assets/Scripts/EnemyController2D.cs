using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    [Tooltip("(Maximal) Movement speed in unity units per second.")]
    public float moveSpeed = 1f;
    #endregion

    #region Animation
    [Header("Animation")]
    [Tooltip("(Maximal) Animation speed in percent: < 1 slower, > 1 faster, 1 = normal speed")]
    public float animationSpeed = 1f;
    [Tooltip("Minimal animation speed in percent. Only relevant while using a controller.")]
    public float minAnimationSpeed = 0f;
    #endregion

    #region Targeting
    [Header("Targeting")]
    private Transform target;
    private Vector2 directionToTarget;
    public float targetingRange = 5f;
    #endregion

    #region Status
    //private bool isMoving = false;
    #endregion

    #region Components
    private Rigidbody2D rigidBody;
    private Animator animator;
    #endregion

    void Start ()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        animator.speed = animationSpeed;
    }
	
	void Update ()
    {
		if(target != null)
        {
            directionToTarget = target.position - transform.position;
            directionToTarget.Normalize();

            animator.SetFloat("directionX", directionToTarget.x);
            animator.SetFloat("directionY", directionToTarget.y);
        }
        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if(players != null)
            {
                foreach (GameObject player in players)
                {
                    if(Vector2.Distance(player.transform.position, transform.position) <= targetingRange)
                    {
                        target = player.transform;
                    }
                }
            }
        }
	}

    private void FixedUpdate()
    {
        if(target != null)
        {
            rigidBody.MovePosition(rigidBody.position + directionToTarget * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
