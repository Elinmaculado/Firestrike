using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // Stats
    public float hp = 3.0f;
    public float speed = 5.0f;
    public float stopDistance = 3.0f;
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    public float bulletSpeed = 10.0f;
    public Transform firePoint;

    private Transform player;
    private Transform house;
    private float nextFireTime = 0f;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
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
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Calcula la distancia entre el enemigo y la casa
            float distanceToHouse = Vector3.Distance(transform.position, house.position);

            // Determina cuál objetivo es más cercano
            Transform target = distanceToPlayer <= distanceToHouse ? player : house;

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

        // Si la vida del enemigo llega a 0 o menos, destruir el enemigo
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Método para mover al enemigo hacia una posición
    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // Método para detener el movimiento del enemigo
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

        // Añade fuerza a la bala (supongamos que tiene un Rigidbody2D)
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; // Usa la velocidad definida
    }


    // Método que detecta colisiones
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con un objeto que tiene el tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1; // Resta 1 a la vida
            Destroy(collision.gameObject); // Destruir la bala después de impactar
        }
    }
}
