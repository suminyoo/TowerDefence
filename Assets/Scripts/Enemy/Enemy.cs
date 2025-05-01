using System;
using UnityEngine;

public class Enemy : BaseItem
{
    public static event Action OnDestroyEnemy;
    public static event Action<Vector3> OnDestroyEnemyPos;      
    
    public override void CheckHP(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            OnDestroyEnemy?.Invoke();
            OnDestroyEnemyPos?.Invoke(transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
   

}
