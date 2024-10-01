using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable : Damagable
{

    [SerializeField] string iframeTag;
    [SerializeField] string playerTag;
    [SerializeField] float invulnerabilityTime;
    [SerializeField] SpriteRenderer playerSprite;
    Color damagedColor;


    private void Start()
    {
        gameObject.tag = playerTag;
    }

    
    public override void Damage(float damage)
    {
        base.Damage(damage);
        Debug.Log("PlayerDamaged");
        StartCoroutine(IFrames());
    }

    public override void Die()
    {
        GameManager.instance.GameOver();
    }


    IEnumerator IFrames()
    {
        Debug.Log("Iframe");
        float alpha = playerSprite.color.a;
        damagedColor = playerSprite.color;
        damagedColor.a = 0.5f;
        playerSprite.color = damagedColor;
        gameObject.tag = iframeTag;
        yield return new WaitForSeconds(invulnerabilityTime);
        gameObject.tag = playerTag;
        damagedColor.a = alpha;
        playerSprite.color = damagedColor;
    }
}
