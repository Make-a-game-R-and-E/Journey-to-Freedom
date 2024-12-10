using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    [Header("Detection")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform detectionPoint; // Point in front of the enemy
    [SerializeField] float detectionRadius = 0.5f;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private Vector2 leftBoundary;
    private Vector2 rightBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftBoundary = new Vector2(leftPoint.position.x, transform.position.y);
        rightBoundary = new Vector2(rightPoint.position.x, transform.position.y);

        // Optionally disable the points so they don't appear in-game
        leftPoint.gameObject.SetActive(false);
        rightPoint.gameObject.SetActive(false);
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            if (transform.position.x >= rightBoundary.x)
            {
                movingRight = false;
                transform.localScale = new Vector3(-1, 1, 1); // Flip the enemy
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            if (transform.position.x <= leftBoundary.x)
            {
                movingRight = true;
                transform.localScale = new Vector3(1, 1, 1); // Flip the enemy
            }
        }
    }

    void DetectPlayer()
    {
        // Detect if player is in front of enemy
        Collider2D playerInRange = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, playerLayer);
        if (playerInRange != null)
        {
            // Player is detected - you can implement catching logic here
            Debug.Log("Player Detected! You got caught!");
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
        }
    }
}
