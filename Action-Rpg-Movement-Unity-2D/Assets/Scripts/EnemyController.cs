using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class EnemyController : MonoBehaviour
{
    #region Targeting
    [Header("Targeting")]
    private Transform target;
    private Vector2 directionToTarget;
    [Tooltip("Tag for finding possible targets.")]
    public string targetTag = "Player";
    [Tooltip("Range in which the enemy will notice other objects.")]
    public float targetingRange = 5f;
    public float approachRange = 2f;
    #endregion

    #region Components
    private MovementController movementController;
    private AnimationController animationController;
    #endregion

    void Start ()
    {
        movementController = GetComponent<MovementController>();
        animationController = GetComponent<AnimationController>();
    }
	
	void Update ()
    {
		if(target != null)
        {
            directionToTarget = target.position - transform.position;

            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget <= approachRange || distanceToTarget > targetingRange)
            {
                target = null;
                return;
            }

            movementController.Direction = directionToTarget;

            if (animationController != null)
            {
                animationController.animator.SetFloat("directionX", directionToTarget.x);
                animationController.animator.SetFloat("directionY", directionToTarget.y);
            }
        }
        else
        {
            movementController.Direction = Vector2.zero;

            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(targetTag);
            if(potentialTargets != null)
            {
                foreach (GameObject potentialTarget in potentialTargets)
                {
                    if(Vector2.Distance(potentialTarget.transform.position, transform.position) <= targetingRange)
                    {
                        target = potentialTarget.transform;
                    }
                }
            }
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, approachRange);
    }
}
