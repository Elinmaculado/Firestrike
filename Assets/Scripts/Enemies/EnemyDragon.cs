using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    // Stats
    public float hp = 5.0f;
    public float speed = 3.0f;
    public float stopDistance = 4.0f;

    // Prefab del �rea de fuego
    public GameObject fireAreaPrefab;

    // Frecuencia de ataque (crear �rea de fuego)
    public float fireRate = 2.0f;

    // Tama�o del �rea de fuego
    public Vector2 fireAreaSize = new Vector2(3.0f, 3.0f);

    // Duraci�n del �rea de fuego activa y desactivada (variables p�blicas)
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
            // Busca la casa m�s cercana
            FindClosestHouse();

            // Si encuentra una casa, calcula distancias
            if (closestHouse != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                float distanceToHouse = Vector3.Distance(transform.position, closestHouse.position);

                // Determina cu�l objetivo es m�s cercano
                Transform target = distanceToPlayer <= distanceToHouse ? player : closestHouse;

                // Si est� dentro del rango de ataque, alterna la aparici�n del fuego
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

        // Si la vida del drag�n llega a 0 o menos, destruir el drag�n
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // M�todo para mover al drag�n hacia una posici�n
    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // M�todo para detener el movimiento del drag�n
    void StopMovement()
    {
        // No se necesita hacer nada aqu� ya que solo dejamos de mover el drag�n
    }

    // M�todo que alterna la aparici�n del fuego
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
            nextFireTime = Time.time + fireActiveTime; // El fuego se mantendr� activo por fireActiveTime segundos
        }
    }

    // M�todo para crear un �rea de fuego en frente del drag�n
    void CreateFire(Transform target)
    {
        // Calcula la direcci�n hacia el objetivo
        Vector3 direction = (target.position - transform.position).normalized;

        // Calcula la posici�n donde aparecer� el �rea de fuego
        Vector3 firePosition = transform.position + direction * (stopDistance / 2); // Ajusta la posici�n frente al drag�n

        // Instancia el �rea de fuego en la posici�n calculada
        fireAreaInstance = Instantiate(fireAreaPrefab, firePosition, Quaternion.identity);

        // Ajusta el tama�o del �rea de fuego
        fireAreaInstance.transform.localScale = new Vector3(fireAreaSize.x, fireAreaSize.y, 1.0f);
    }

    // M�todo para encontrar la casa m�s cercana
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

    // M�todo que detecta colisiones
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con un objeto que tiene el tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1; // Resta 1 a la vida
            Destroy(collision.gameObject); // Destruir la bala despu�s de impactar
        }
    }
}
