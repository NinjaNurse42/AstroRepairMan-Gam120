using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform TransFormToFollow;
    public Vector3 Offset;

    [Range(0f, 1f)]
    public float Interpolant = 0.15f;

    void LateUpdate()
    {
        if (!TransFormToFollow) return;

        Vector3 targetPos = TransFormToFollow.position + Offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Interpolant
        );
    }
}
