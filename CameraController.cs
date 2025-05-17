using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset = 2f;
    [Range(0, 1)] public float smoothTime = 0.3f;
    public BoxCollider2D cameraBounds;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;

    private float camHalfWidth;
    private float camHalfHeight;

    private float minX, maxX, minY, maxY;

    public Vector3 positionOffset;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        if (cameraBounds != null)
        {
            Bounds bounds = cameraBounds.bounds;
            minX = bounds.min.x + camHalfWidth;
            maxX = bounds.max.x - camHalfWidth;
            minY = bounds.min.y + camHalfHeight;
            maxY = bounds.max.y - camHalfHeight;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Target position + offset
            targetPosition = player.transform.position + positionOffset;

            // Horizontal offset berdasarkan arah player
            if (player.transform.localScale.x > 0f)
                targetPosition.x += offset;
            else
                targetPosition.x -= offset;

            // Clamp posisi target ke dalam batas
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
            targetPosition = new Vector3(clampedX, clampedY, transform.position.z);

            // Smooth follow dengan SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
