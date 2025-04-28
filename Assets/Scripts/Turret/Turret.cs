using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Turret : BaseItem
{
    public static event Action OnDestroyTurret;
    public static event Action<Vector3> OnDestroyTurretPos;

    public override  void CheckHP(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            OnDestroyTurret?.Invoke();
            OnDestroyTurretPos?.Invoke(transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
