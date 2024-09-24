using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    // Stats
    public float hp = 5.0f;
    public float speed = 3.0f;
    public float stopDistance = 4.0f;

    // Prefab del área de fuego
    public GameObject fireAreaPrefab;

    // Frecuencia de ataque (crear área de fuego)
    public float fireRate = 2.0f;

    // Tamaño del área de fuego
    public Vector2 fireAreaSize = new Vector2(3.0f, 3.0f);

    // Duración del área de fuego activa y desactivada (variables públicas)
    public float fireActiveTime = 3.0f;
    public float fireInactiveTime = 3.0f;

    private Transform player;
    private Transform closestHouse;
    private float nextFireTime = 0f;
    private bool isFireActive = false;
    private GameObject fireAreaInstance;

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

            // Si encuentra una casa, calcula distancias
            if (closestHouse != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                float distanceToHouse = Vector3.Distance(transform.position, closestHouse.position);

                // Determina cuál objetivo es más cercano
                Transform target = distanceToPlayer <= distanceToHouse ? player : closestHouse;

                // Si está dentro del rango de ataque, alterna la aparición del fuego
                if (Vector3.Distance(transform.position, target.position) <= stopDistance)
                {
                    StopMovement();
                    if (Time.time >= nextFireTime)
                    {
                        ToggleFire(target);
                    }
                }
                else
                {
                    MoveTowards(target.position);
                }
            }
        }

        // Si la vida del dragón llega a 0 o menos, destruir el dragón
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Método para mover al dragón hacia una posición
    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // Método para detener el movimiento del dragón
    void StopMovement()
    {
        // No se necesita hacer nada aquí ya que solo dejamos de mover el dragón
    }

    // Método que alterna la aparición del fuego
    void ToggleFire(Transform target)
    {
        if (isFireActive)
        {
            // Desactiva el fuego
            Destroy(fireAreaInstance);
            isFireActive = false;
            nextFireTime = Time.time + fireInactiveTime; // Espera fireInactiveTime segundos antes de volver a activar el fuego
        }
        else
        {
            // Activa el fuego
            CreateFire(target);
            isFireActive = true;
            nextFireTime = Time.time + fireActiveTime; // El fuego se mantendrá activo por fireActiveTime segundos
        }
    }

    // Método para crear un área de fuego en frente del dragón
    void CreateFire(Transform target)
    {
        // Calcula la dirección hacia el objetivo
        Vector3 direction = (target.position - transform.position).normalized;

        // Calcula la posición donde aparecerá el área de fuego
        Vector3 firePosition = transform.position + direction * (stopDistance / 2); // Ajusta la posición frente al dragón

        // Instancia el área de fuego en la posición calculada
        fireAreaInstance = Instantiate(fireAreaPrefab, firePosition, Quaternion.identity);

        // Ajusta el tamaño del área de fuego
        fireAreaInstance.transform.localScale = new Vector3(fireAreaSize.x, fireAreaSize.y, 1.0f);
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
