using UnityEngine;

public class Damagable : MonoBehaviour 
{

    public float currentHealth;
    public float scoreValue;
    public AudioClip[] damageSounds;
    public AudioClip[] dieSounds;
    public AudioSource audioSource;

    public void Damage(float damage)
    {
        currentHealth -= damage;
        audioSource.pitch = Random.Range(1.0f, 2.0f);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            audioSource.PlayOneShot(damageSounds[randomIndex]);
        }
    }

    public void Die()
    {
        int randomIndex = Random.Range(0, dieSounds.Length);
        audioSource.PlayOneShot(dieSounds[randomIndex]);
        Destroy(gameObject,(dieSounds[randomIndex].length));
        GameManager.instance.AddScore(scoreValue);
    }
}