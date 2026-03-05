using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 4f;
    [SerializeField] int damage = 1;
    [SerializeField] bool destroyOnAnyCollision = true;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ensure top-down projectile doesn't fall due to gravity
        if (rb != null)
            rb.gravityScale = 0f;
    }

    void Start()
    {
        if (lifeTime > 0f)
            Destroy(gameObject, lifeTime);
    }

    // Called by turret when prefab lacks Rigidbody2D or as an alternative initializer
    public void SetInitialVelocity(Vector2 velocity)
    {
        if (rb != null)
            rb.linearVelocity = velocity;
        else
            // fallback: move via transform if no rigidbody (not recommended for physics collisions)
            StartCoroutine(MoveByTransform(velocity));
    }

    System.Collections.IEnumerator MoveByTransform(Vector2 velocity)
    {
        float elapsed = 0f;
        while (elapsed < lifeTime)
        {
            transform.position += (Vector3)(velocity * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyOnAnyCollision)
            Destroy(gameObject);
    }
}
