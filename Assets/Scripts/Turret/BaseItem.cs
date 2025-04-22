using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class BaseItem : MonoBehaviour
{
    private string targetTagName;

    public string TargetTagName
    {
        get { return targetTagName; }
        set { targetTagName = value; }
    }

    public TextMeshProUGUI HPtxt;
    public TextMeshProUGUI ATKtxt;

    [Header("PS")]
    public ParticleSystem MuzzelFlash_ParticleSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;

    [Header("PS for Destroy")]
    public ParticleSystem Destroy_ParticleSystem;

    [Header("Transform")]
    public Transform gunbarrel;
    public Transform NearTarget;

    int BulletCount = 5;
    float lapTime = 0;

    private int hp;
   
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;

            HPtxt.text = hp.ToString();
        }
    }

    private int atk;
  
    public int ATK
    {
        get { return atk; }
        set
        {
            atk = value;

            ATKtxt.text = atk.ToString();
        }
    }
 
    private void Awake() //Awake, Update 다 상속돼서 그대로 실행
    {
        Destroy_ParticleSystem.Stop();
        MuzzelFlash_ParticleSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();
    }

    public void Initialize()
    {
        Invoke("DoSomething", 3);
    }
  
    public void Begin()
    {
        LookAtTarget();
        DoSomething();
    }

    public void Prepare(GameObject[] targets)
    {
        NearTarget = NearestTarget.FindNearestTarget(gameObject, targets).transform;
    }
   
    public virtual void FindNewTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTagName);
 
        if (targets.Length == 0) return;
        Prepare(targets);
        LookAtTarget();
    }
 
    public void LookAtTarget()
    {
        gunbarrel.LookAt(NearTarget);
    }
    void DoSomething()
    {
        MuzzelFlash_ParticleSystem.gameObject.SetActive(true);
        MuzzelFlash_ParticleSystem.Play();
        BulletShells_ParticleSystem.Play();
        Traser_ParticleSystem.Play();
    }

    public void Fire()
    {
        if (BulletCount <= 0)
        {
            Reload();
            return;
        }
        BulletCount--;
    }

    public void Reload()
    {
        MuzzelFlash_ParticleSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();

        BulletCount = 5;
        Invoke("DoSomething", 3);
    }

    public void TakeDamage(int damage)
    {
        HP = HP - damage;

        if (HP <= 0)
            Destroy();
    }

    public void StopShooting()
    {
        MuzzelFlash_ParticleSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();
    }
   
    public virtual void Destroy() //virtual 선언으로 child가 override할 수 있게
    {
        transform.gameObject.SetActive(false);      
        Destroy(gameObject);
    }


    private void Update()
    {
        lapTime += Time.deltaTime;  // 1초에 24실행, 0.22

        if (lapTime > 1)
        {
            Fire();
            lapTime = 0;
        }

        if (NearTarget == null)
        {
            FindNewTarget();
        }
    }
}
