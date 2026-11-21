
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private float platformSpeed;
    private bool isDiferent = false;

    private void Update()
    {
        if (!isDiferent)
        {
            platformSpeed = GroundMovement.Instance.GetSpeed();
        }

        // Movimenta a plataforma para a esquerda
        transform.position += Vector3.left * platformSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        platformSpeed = newSpeed;
    }
}
