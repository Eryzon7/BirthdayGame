using UnityEngine;
using UnityEngine.SceneManagement;

public class CakeLayer : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Transform currentTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }
    float CalculateNormalizedOffset(Transform layer)
    {
        float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;

        float centerX = Camera.main.transform.position.x;

        float offset = layer.position.x - centerX;

        return offset / (cameraWidth / 2f); // normalized -1 to 1
    }

    public void CakeDrop()
    {
        speed = 0;
        rb.gravityScale = 3f;

        float normalized = CalculateNormalizedOffset(transform);

        CakeLayerData data = new CakeLayerData
        {
            normalizedOffsetX = normalized
        };

        GameManager.Instance.cakeLayers.Add(data);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
