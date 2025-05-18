using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 moveDirection = Vector2.right;
    private Rigidbody2D rb;

    private float startY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = rb.position.y;
    }

    void FixedUpdate()
    {
        Vector2 velocity = moveDirection * moveSpeed;

        float currentY = rb.position.y;
        float newY = Mathf.MoveTowards(currentY, startY, moveSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector2(velocity.x, (newY - currentY) / Time.fixedDeltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hier kannst du prüfen, ob das Hindernis eine bestimmte Layer hat
        if (collision.gameObject.layer == LayerMask.NameToLayer("Solid")) {

            // Richtung umdrehen
            moveDirection = -moveDirection;

            // NPC optisch drehen
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}





