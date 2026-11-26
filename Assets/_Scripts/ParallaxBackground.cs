
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Background References")]
    [SerializeField] private SpriteRenderer background1;
    [SerializeField] private SpriteRenderer background2;
    [SerializeField] private SpriteRenderer background3;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Parallax Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float parallaxMultiplier = 0.3f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // Posiciona a segunda imagem após a primeira
        background2.transform.position = new Vector3(
            background1.bounds.max.x + background2.bounds.extents.x,
            background1.transform.position.y,
            background1.transform.position.z
        );

        background3.transform.position = new Vector3(
            background2.bounds.max.x + background3.bounds.extents.x,
            background2.transform.position.y,
            background2.transform.position.z
        );
    }

    private void LateUpdate()
    {
        if (GroundMovement.Instance == null ||
            mainCamera == null)
            return;

        MoveBackground();
        RepositionBackground();
    }

    private void MoveBackground()
    {
        float groundSpeed =
            GroundMovement.Instance.GetSpeed();

        float backgroundSpeed =
            groundSpeed * parallaxMultiplier;

        Vector3 movement =
            Vector3.left * backgroundSpeed * Time.deltaTime;

        background1.transform.position += movement;
        background2.transform.position += movement;
        background3.transform.position += movement;

    }

    private void RepositionBackground()
    {
        float cameraLeft =
            mainCamera.transform.position.x -
            mainCamera.orthographicSize *
            mainCamera.aspect;

        if (background1.bounds.max.x <= cameraLeft)
        {
            background1.transform.position = new Vector3(
                background3.bounds.max.x +
                    background1.bounds.extents.x,
                background1.transform.position.y,
                background1.transform.position.z
            );
        }

        if (background2.bounds.max.x <= cameraLeft)
        {
            background2.transform.position = new Vector3(
                background1.bounds.max.x +
                    background2.bounds.extents.x,
                background2.transform.position.y,
                background2.transform.position.z
            );
        }

        if (background3.bounds.max.x <= cameraLeft)
        {
            background3.transform.position = new Vector3(
                background2.bounds.max.x +
                    background3.bounds.extents.x,
                background3.transform.position.y,
                background3.transform.position.z
            );
        }
    }
}
