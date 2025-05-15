using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset = 2f;
    public float offsetSmoothing = 5f;
    public BoxCollider2D cameraBounds;

    private Vector3 targetPosition;
    private float camHalfWidth;

    private float minX, maxX;

    void Start()
    {
        camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        if (cameraBounds != null)
        {
            Bounds bounds = cameraBounds.bounds;
            minX = bounds.min.x + camHalfWidth;
            maxX = bounds.max.x - camHalfWidth;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.localScale.x > 0f)
                targetPosition.x += offset;
            else
                targetPosition.x -= offset;

            // Clamp berdasarkan collider
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.x = clampedX;

            // Smooth follow
            transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
        }
    }
}
