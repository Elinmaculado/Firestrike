using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IEDamagable
{

    [SerializeField] int maxHelath;
    [SerializeField] Image fillBar;
    [SerializeField] string iframeTag;
    [SerializeField] string playerTag;
    [SerializeField] float invulnerabilityTime;
    [SerializeField] SpriteRenderer playerSprite;
    Color damagedColor;
    float currentHealt;

    public void Damage(float damage)
    {
        if(gameObject.CompareTag(iframeTag)) { return; }
        StartCoroutine(IFrames());
        currentHealt -= damage;
        UpdateLifeBar();
        if(currentHealt<=0){
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    private void Start() {
        currentHealt = maxHelath;
        gameObject.tag = playerTag;
    }

    private void Update() {
        if(currentHealt<maxHelath){
            currentHealt += Time.deltaTime;
        }
        if(currentHealt>maxHelath){
            currentHealt = maxHelath;
        }
        UpdateLifeBar();
        if(Input.GetKeyDown(KeyCode.Q)){
            Damage(10);
        }

    }

    void UpdateLifeBar(){
        fillBar.fillAmount = currentHealt/maxHelath;
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
