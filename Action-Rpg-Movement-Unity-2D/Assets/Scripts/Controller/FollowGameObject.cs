using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public GameObject goToFollow;
    public float followSpeed = 5f;

    private Vector3 position;

    private void Start()
    {
        if(goToFollow != null)
        {
            position = goToFollow.transform.position;
            position.z = -10;
        }
        else
        {
            Debug.LogError("Can not follow a null reference!");
        }
     }

    void LateUpdate ()
    {
        position = goToFollow.transform.position;
        position.z = -10;

        this.transform.position = Vector3.Lerp(this.transform.position, position, followSpeed * Time.deltaTime);
    }
}
