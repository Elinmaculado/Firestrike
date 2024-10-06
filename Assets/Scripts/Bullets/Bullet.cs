using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTimeSeconds;
    [SerializeField] string targetTag;
    [SerializeField] float damage;

    private void Start()
    {
        Destroy(gameObject,lifeTimeSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.gameObject.GetComponent<Damagable>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
