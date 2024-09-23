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
    private Transform house;

    void Start()
    {
        // Busca el jugador por su tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Busca la casa por su tag
        GameObject houseObject = GameObject.FindGameObjectWithTag("House");
        if (houseObject != null)
        {
            house = houseObject.transform;
        }
    }

    void Update()
    {
        if (player != null && house != null)
        {
            // messures distance between player and house
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            float distanceToHouse = Vector3.Distance(transform.position, house.position);

            // Moves to the closeste one
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
                    MoveTowards(house.position);
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
}
