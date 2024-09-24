using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    // Stats
    public float hp = 3.0f;
    public float speed = 5.0f;
    public float stopDistance;

    private Transform player;
    private Transform closestHouse;

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
            // Busca la casa más cercana
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

        // If hp reaches 0 enemy dies
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If hit by a bullet it loses hp
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;

            // Desyttoys the bullet
            Destroy(collision.gameObject);
        }
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
}
