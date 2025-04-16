using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform gunbarrel;
    public Transform RealTarget;
    public static event Action OnDestroyEnemy;

    //public int HP = 100;
    //public int ATK;

    public ParticleSystem MuzzelFlash_ParticlaSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;

    public TextMeshProUGUI HPtxt; //UI �󿡼� �ؽ�Ʈ �ʵ带 ����Ŵ
    public TextMeshProUGUI ATKtxt;

    Transform NearTarget = null;


    //prop -> propfull tab tab
    private int hp;
    ///
    public int HP
    {
        get { return hp; }
        set { 
            hp = value; 
            HPtxt.text = hp.ToString();
        }
    }

    private int atk;  //private lowercase
    public int ATK
    {
        get { return atk; }
        set { 
            atk = value;
            ATKtxt.text = ATK.ToString();
        }
    }

    public void Prepare(GameObject[] target)
    {
       // NearTarget = NearestTarget.FindNearestTarget(gameObject, target).trasform;
    }

    //ParticleSystem ���� 
    //����� Enemy(Script)�� �˾ƺ��� ����
    [Header("Ps for Destroy")]
    public ParticleSystem Destroy_ParticleSystem;

    //Awake�� ����
    private void Awake()
    {
        Destroy_ParticleSystem.Stop();
        MuzzelFlash_ParticlaSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();
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
