using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IEDamagable
{

    [SerializeField] int maxHelath;
    [SerializeField] Image fillBar;
    float currentHealt;

    public void Damage(float damage)
    {
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
}
