using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] int maxHelath;
    [SerializeField] Image fillBar;
    public PlayerDamagable life;


    private void Start() {
        life.currentHealth = maxHelath;
    }

    private void Update() {
        if(life.currentHealth <maxHelath){
            life.currentHealth += Time.deltaTime;
        }
        if(life.currentHealth >maxHelath){
            life.currentHealth = maxHelath;
        }
        UpdateLifeBar();
    }

    void UpdateLifeBar(){
        fillBar.fillAmount = life.currentHealth /maxHelath;
    }
}
