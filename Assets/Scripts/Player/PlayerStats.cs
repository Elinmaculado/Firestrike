using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] int maxHelath;
    [SerializeField] Image fillBar;
    [SerializeField] string iframeTag;
    [SerializeField] string playerTag;
    [SerializeField] float invulnerabilityTime;
    [SerializeField] SpriteRenderer playerSprite;
    Color damagedColor;
    public Damagable life;


    private void Start() {
        life.currentHealth = maxHelath;
        gameObject.tag = playerTag;
    }

    private void Update() {
        if(life.currentHealth <maxHelath){
            life.currentHealth += Time.deltaTime;
        }
        if(life.currentHealth >maxHelath){
            life.currentHealth = maxHelath;
        }
        UpdateLifeBar();
        if(Input.GetKeyDown(KeyCode.Q)){
            life.Damage(10);
        }

    }

    void UpdateLifeBar(){
        fillBar.fillAmount = life.currentHealth /maxHelath;
    }

    IEnumerator IFrames()
    {
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
