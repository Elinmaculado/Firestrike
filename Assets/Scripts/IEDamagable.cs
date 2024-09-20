using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEDamagable 
{
    float Healt {  get; protected set; }
    
    void Damage(float damage);
    void Die();

}