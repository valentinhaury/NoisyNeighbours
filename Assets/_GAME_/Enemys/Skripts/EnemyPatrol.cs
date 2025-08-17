using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed = 2f;
    public Vector2 startDirection = Vector2.right;
    public float walkDuration = 3f;
    public float freezeDuration = 3f;

    private Rigidbody2D rb;
    private float walkTimer;
    private float freezeTimer = 3f;
    private bool active;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        freezeTimer += Time.deltaTime;
        if (!active && freezeTimer >= freezeDuration)
        {
            active = true;
        }

        if (active)
        {
            walkTimer += Time.deltaTime;

            if (walkTimer >= walkDuration)
            {
                startDirection = startDirection * -1;
                walkTimer = 0f;
            }
        }
        
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector2 newPosition = rb.position + startDirection.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    public void Freeze()
    {
        active = false;
        freezeTimer = 0f;
    }
}
