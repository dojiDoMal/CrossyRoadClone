using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
