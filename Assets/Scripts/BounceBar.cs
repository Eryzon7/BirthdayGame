using UnityEngine;

public class BounceBar : MonoBehaviour
{
    public float bounceForce = 10f;
    public float speed = 10f;
    public float limitX = 7f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);

        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        // difference between ball hit and paddle center
        float hitFactor = (
            collision.transform.position.x - transform.position.x
        ) / GetComponent<Collider2D>().bounds.size.x;

        // create bounce direction
        Vector2 direction = new Vector2(hitFactor, 1).normalized;

        // apply bounce
        rb.linearVelocity = direction * bounceForce;
    }
}
