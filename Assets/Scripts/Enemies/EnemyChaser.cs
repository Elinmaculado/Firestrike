using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    // Stats
    public Damagable life;
    public float speed = 5.0f;
    public float stopDistance;

    private Transform player;
    private Transform closestHouse;

    public float attackCooldown;
    public float damage;
    public float fireDamage;

    bool isReadyToDamage = true;


    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;

    void Start()
    {
        // Busca el jugador por su tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            FindClosestHouse();

            if (closestHouse != null)
            {
                // messures distance between player and house
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                float distanceToHouse = Vector3.Distance(transform.position, closestHouse.position);

                // Moves to the closest one
                if (distanceToPlayer <= distanceToHouse)
                {
                    // it moves only if it is further away than the minimum distance
                    if (distanceToPlayer > stopDistance)
                    {
                        MoveTowards(player.position);
                    }
                }
                else
                {
                    if (distanceToHouse > stopDistance)
                    {
                        MoveTowards(closestHouse.position);
                    }
                }
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.SetInteger("Vertical", (int)(direction.y * 2));
        sprite.flipX = direction.x < 0;
    }

    // Find Closesest house
    void FindClosestHouse()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject house in houses)
        {
            float distance = Vector3.Distance(transform.position, house.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHouse = house.transform;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

           if (collision.gameObject.CompareTag("House"))
        {
            collision.gameObject.GetComponent<BuildingController>().OnFire(fireDamage * Time.deltaTime);
        }
        if (isReadyToDamage)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DealDamge());
                collision.gameObject.GetComponent<Damagable>().Damage(damage);
            }
        }
     
    }

    IEnumerator DealDamge()
    {
        isReadyToDamage = false;
        yield return new WaitForSeconds(attackCooldown);
        isReadyToDamage = true;
    }
}
