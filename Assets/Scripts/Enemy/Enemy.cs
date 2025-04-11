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


    //ParticleSystem ���� 
    //����� Enemy(Script)�� �˾ƺ��� ����
    [Header("Ps for Destroy")]
    //������ �� �߰��� ��ƼŬ �巡���ؼ� �߰�
    public ParticleSystem Destroy_ParticleSystem;

    //Awake�� ����
    private void Awake()
    {
        Destroy_ParticleSystem.Stop();          //ó���� Awake�ϰ� Stop���� ����
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
