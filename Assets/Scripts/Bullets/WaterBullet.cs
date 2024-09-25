using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    [SerializeField] float lifeTimeSeconds;
    [SerializeField] string target;
    [SerializeField] float damage;

    private void Start()
    {
        Destroy(gameObject, lifeTimeSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target))
        {
            collision.gameObject.GetComponent<BuildingController>().PutOutFire(damage);
            Destroy(gameObject);
        }
    }
}



