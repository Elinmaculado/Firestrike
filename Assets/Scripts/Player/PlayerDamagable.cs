using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable : Damagable
{

    public new void Die()
    {
        GameManager.instance.GameOver();
    }
}
