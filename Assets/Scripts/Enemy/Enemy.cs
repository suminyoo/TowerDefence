using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform gunbarrel;
    public static event Action OnDestroyEnemy;
    public TextMeshProUGUI HPtxt;
    public TextMeshProUGUI ATKtxt;

  [SerializeField]  Transform NearlTarget=null;
    private int hp;

    public int HP
    {
        get { return hp; }
        set { hp = value;

            HPtxt.text = hp.ToString();
        }
    }

    private int atk;

    public int ATK
    {
        get { return atk; }
        set { atk = value;

            ATKtxt.text = atk.ToString();
        }
    }


    

    [Header("PS")]
    public ParticleSystem MuzzelFlash_ParticleSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;

    [Header("PS for Destroy")]
    public ParticleSystem Destroy_ParticleSystem;

    private void Awake()
    {
        Destroy_ParticleSystem.Stop();
        MuzzelFlash_ParticleSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();
    }

    public void Initialize()
    {
        
    }
    public void Prefare(GameObject[] targets)
    {
        NearlTarget = NearestTarget.FindNearestTarget( gameObject, targets).transform;
    }
    public void Begin()
    {
        MuzzelFlash_ParticleSystem.gameObject.SetActive(true);
        MuzzelFlash_ParticleSystem.Play();
        BulletShells_ParticleSystem.Play();
        Traser_ParticleSystem.Play();
    }
    private void Update()
    {
        if (NearlTarget == null) return;

        gunbarrel.LookAt(NearlTarget);
    }
    
    public void CheckHP(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            OnDestroyEnemy?.Invoke();
            gameObject.SetActive(false);// Destroy(gameObject);
        }
    }

   
    


}
