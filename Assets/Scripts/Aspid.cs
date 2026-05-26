using UnityEngine;

public class Aspid : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public Level level;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootPoint.right * shootForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(level.WinGame());
            Destroy(gameObject);
            // victory
        }
            
    }
}
