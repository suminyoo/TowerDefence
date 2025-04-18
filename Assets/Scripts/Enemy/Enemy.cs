using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform gunbarrel;
    public Transform RealTarget;
    public static event Action OnDestroyEnemy;

    public ParticleSystem MuzzelFlash_ParticleSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;

    public TextMeshProUGUI HPtxt; //UI 상에서 텍스트 필드를 가리킴
    public TextMeshProUGUI ATKtxt;

    //[SerializeField]는 private을 보고 싶을때 사용

    [SerializeField] Transform NearTarget = null;


    //prop -> propfull tab tab
    private int hp;
    ///
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            HPtxt.text = hp.ToString();
        }
    }

    private int atk;  //private lowercase
    public int ATK
    {
        get { return atk; }
        set
        {
            atk = value;
            ATKtxt.text = ATK.ToString();
        }
    }

    private void Awake()
    {
        Destroy_ParticleSystem.Stop();
        MuzzelFlash_ParticleSystem.Stop();
        BulletShells_ParticleSystem.Stop();
        Traser_ParticleSystem.Stop();
    }

    public void Prepare(GameObject[] target)
    {

        NearTarget = NearestTarget.FindNearestTarget(gameObject, target).transform;

        Debug.Log("Nearest Target: " + NearTarget.name);
    }
    public void Begin()
    {
        MuzzelFlash_ParticleSystem.gameObject.SetActive(true);
        MuzzelFlash_ParticleSystem.Play();
        BulletShells_ParticleSystem.Play();
        Traser_ParticleSystem.Play();
    }

    //ParticleSystem 정의 
    //헤더로 Enemy(Script)에 알아보기 쉽게
    [Header("Ps for Destroy")]
    public ParticleSystem Destroy_ParticleSystem;


    public void CheckHP(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            OnDestroyEnemy?.Invoke();
            gameObject.SetActive(false);
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
        //if (Input.GetKeyDown(KeyCode.A) )
        //{
        //    Destroy_ParticleSystem.Play();
        //}
        if (NearTarget != null)
        {
            LookAtTarget();
        }
        else
        {
            FindNewTarget();
        }


    }

    private void LookAtTarget()
    {
        gunbarrel.LookAt(NearTarget);
    }

    private void FindNewTarget()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(); //태그
        List<GameObject> targets = new List<GameObject>();
        //int enemyLayer = LayerMask.NameToLayer("Turret");

        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "Turret")
            {
                targets.Add(obj);
            }

        }
        if (targets.Count == 0) return;
        Prepare(targets.ToArray());
        LookAtTarget();

    }
}
