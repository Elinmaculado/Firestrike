using UnityEngine;

public class Damagable : MonoBehaviour 
{

    public float currentHealth;
    public float scoreValue;

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
        GameManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}