using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Transform player;
    private Rigidbody2D rb;

    public float detectionRange = 5f;  // Range within which the enemy detects the player
    public float attackRange = 1f;     // Range within which the enemy attacks the player
    public float attackRate = 1f;      // Time between attacks
    private bool isChasing = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > detectionRange)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    void ChasePlayer()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        Debug.Log("Chasing the player!");
    }

    void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            // Implement attack logic here
            Debug.Log("Attacking the player!");

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            Debug.Log("Player detected. Start chasing!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            Debug.Log("Player out of range. Stop chasing!");
        }
    }
}
