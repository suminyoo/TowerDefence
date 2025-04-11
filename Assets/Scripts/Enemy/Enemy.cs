using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform gunbarrel;
    public Transform RealTarget;
    public static event Action OnDestroyEnemy;

    public int HP = 100;
    public int ATK;

    public ParticleSystem MuzzelFlash_ParticlaSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;


    //ParticleSystem 정의 
    //헤더로 Enemy(Script)에 알아보기 쉽게
    [Header("Ps for Destroy")]
    //정의한 후 추가할 파티클 드래그해서 추가
    public ParticleSystem Destroy_ParticleSystem;

    //Awake로 시작
    private void Awake()
    {
        Destroy_ParticleSystem.Stop();          //처음에 Awake하고 Stop으로 제어
        //MuzzelFlash_ParticlaSystem.Stop();
        //BulletShells_ParticleSystem.Stop();
        //Traser_ParticleSystem.Stop();
    }

    public void CheckHP(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            OnDestroyEnemy?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision bullet)
    {
        CheckHP(10);
        Debug.Log("OnCollisionEnter!!!!!" + bullet.gameObject.name);
    }


    private void OnTriggerEnter(Collider other)
    {
        CheckHP(10);
        Debug.Log("OnTriggerEnter!!!!!" + other.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) )
        {
            Destroy_ParticleSystem.Play();
        }
        gunbarrel.LookAt(RealTarget);

    }
}
