using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // Stats
    public Damagable life;
    public float speed = 5.0f;
    public float stopDistance = 3.0f;
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    public float bulletSpeed = 10.0f;
    public Transform firePoint;

    private Transform player;
    private Transform closestHouse;
    private float nextFireTime = 0f;



    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;

    void Start()
    {
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
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                float distanceToHouse = Vector3.Distance(transform.position, closestHouse.position);

                Transform target = distanceToPlayer <= distanceToHouse ? player : closestHouse;

                // Si está dentro del rango de disparo, dispara
                if (Vector3.Distance(transform.position, target.position) <= stopDistance)
                {
                    StopMovement();
                    if (Time.time >= nextFireTime)
                    {
                        Shoot(target);
                        nextFireTime = Time.time + fireRate;
                    }
                }
                else
                {
                    MoveTowards(target.position);
                }
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.SetInteger("Vertical", (int)(direction.y*2));
        sprite.flipX = direction.x < 0;
    }

    void StopMovement()
    {
        // No se necesita hacer nada aquí ya que solo dejamos de mover el enemigo
    }

    // Método que dispara balas hacia el objetivo
    void Shoot(Transform target)
    {
        // Instancia el prefab de la bala en el firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calcula la dirección hacia el objetivo
        Vector3 direction = (target.position - firePoint.position).normalized;

        // Añade fuerza a la bala (suponiendo que tiene un Rigidbody2D)
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; // Usa la velocidad definida
    }

    // Método que detecta colisiones
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con un objeto que tiene el tag "Bullet"
        /*
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1; // Resta 1 a la vida
            Destroy(collision.gameObject); // Destruir la bala después de impactar
        }
        */
    }

    // Método para encontrar la casa más cercana
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
