using UnityEngine;

public class FlameBullet : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] string target;
    [SerializeField] float damage;

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target))
        {
            collision.gameObject.GetComponent<BuildingController>().OnFire(damage);
            Destroy(gameObject);
        }
    }
}
