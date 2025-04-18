using System;
using TMPro;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public static event Action StaticDestroyEvent;
    public        event Action NotStaticDestroyEvent;
    
    public Transform gunbarrel;
    public Transform RealTarget;
    public GatlingGun gatlingGun;
    
    //public UIManager _UIManager;
    
    [Header("PS")]
    public ParticleSystem MuzzelFlash_ParticleSystem;
    public ParticleSystem BulletShells_ParticleSystem;
    public ParticleSystem Traser_ParticleSystem;

    [Header("PS Destroy")]
    public ParticleSystem Destroy_ParticleSystem;

    public TextMeshProUGUI HPtxt; //UI 상에서 텍스트 필드를 가리킴
    public TextMeshProUGUI ATKtxt;

    [SerializeField] Transform NearTarget = null;


    int BulletCount = 5;
    float lapTime = 0;

    private int hp;
    public int HP
    {
        get { return hp; }
        set { 
            hp = value;
            HPtxt.text = hp.ToString();

        }
    }

    private int atk;
    public int ATK
    {
        get { return atk; }
        set { 
            atk = value;
            ATKtxt.text = atk.ToString();
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
    }
    public void Begin()
    {
        MuzzelFlash_ParticleSystem.gameObject.SetActive(true);
        MuzzelFlash_ParticleSystem.Play();
        BulletShells_ParticleSystem.Play();
        Traser_ParticleSystem.Play();
    }

    private void Start()
    {
        Destroy_ParticleSystem.Stop();  
        gatlingGun.enabled = false;
        Invoke("DoSomething", 3);
    }
    private void Update()
    {
        lapTime += Time.deltaTime;  // 1초에 24실행, 0.22

        if (lapTime > 1)
        {
            Fire();
            lapTime = 0;
        }
        if (NearTarget != null)
        {
            gunbarrel.LookAt(NearTarget);

        }
    }

    void DoSomething()
    {
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

    void Destroy()
    {  
        transform.gameObject.SetActive(false);   // 사라짐.         
        Destroy_ParticleSystem.Play();
        Destroy(gameObject);
       
    }

    public void TakeDamage(int damage)
    {

        HP = HP - damage;

        if (HP <= 0)        
            Destroy();
        
    }




}
