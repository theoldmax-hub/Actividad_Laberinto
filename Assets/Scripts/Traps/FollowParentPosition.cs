using UnityEngine;

public class FollowParentPosition : MonoBehaviour
{
    public Transform parentTransform;
    private Quaternion startRotation;

    private void Awake()
    {
        if (parentTransform == null) parentTransform = transform.parent;
        startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (parentTransform == null) return; 
        transform.position = parentTransform.position;
        transform.rotation = startRotation;
    }
}
