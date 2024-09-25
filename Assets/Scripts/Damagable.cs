using UnityEngine;

public class Damagable : MonoBehaviour 
{

    public float currentHealth;

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}