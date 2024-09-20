using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask collitionLayers;
    [SerializeField] float lifeTimeSeconds;

    private void Start()
    {
        Destroy(gameObject,lifeTimeSeconds);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
