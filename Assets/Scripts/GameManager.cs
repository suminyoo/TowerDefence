using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Splines;

//과제 월드 spline 3개 이상만들고
//에너미 프리팹에 spline animate 추가해서 생성과 동시에 애니메이션 되게 하기 코루틴
//Spline spline;

public class GameManager : MonoBehaviour
{
    public Spline route1;
    public Spline route2;
    public Spline route3;

    public static event Action<string> OnWinnerEvent;

    public static event Action<int> OneEnemyUICountChangeEvent;
    public static event Action<int> OneTurretUICountChangeEvent;

    [Header("GameObject")]
    public GameObject enemyPrefab;
    public GameObject turretPrefab;
    public GameObject battleField;

    public GameObject[] turretObj = new GameObject[5];
    public GameObject[] enemyObj = new GameObject[5];

    Enemy[] enemies = new Enemy[5];
    Turret[] turrets = new Turret[5];

    private int totalTurret;

    public int TotalTurret
    {
        get { return totalTurret; }
        set { totalTurret = value;}
    }

    private int totalEnemy;
    public int TotalEnemy
    {
        get { return totalEnemy; }
        set { totalEnemy = value; }
    }

    void Start()
    {

        

        UIManager.OnGameAgainEvent += GameAgain;
        UIManager.OnGameStartEvent += GameStart;
        UIManager.OnGameEndEvent += GameQuit;

        Turret.OnDestroyTurret += OneTurretRemove;  //이벤트를 듣고 함수실행
        Enemy.OnDestroyEnemy += OneEnemyRemove;

        //이벤트와 리스너, 퍼블리셔와 섭스크라이버
        //OnGameAgainEvent 이벤트 발생시 UIManager_OnGameAgainEvent 실행
        //UI와 GM은 분리되어야함

        Initialize();
        Prepare();
        //StartCoroutine(SampleCor());

    }

    private void GameStart()
    {
        BeginGame();
    }
    private void GameAgain()
    {
        //코루틴 실행 함수 Cor로 이름도 넣어줌
        StartCoroutine(AgainCor());
    }

    public IEnumerator AgainCor()
    {
        //Coroutine 필요. 로직상문제는 없지만 함수가 겹쳐서 버그가날 수 있음
        //Coroutine으로 순차적인 실행을 보장 StartCoroutine(SampleCor()); 의 형태
        //yield return null;
        StopShooting();
        yield return null;
        DestroyAll();
        yield return null;
        Initialize();
        yield return null;
        Prepare();
        yield return null;
        //yield return new WaitForSeconds(3); //3초
        BeginGame();
    }

    void Initialize()
    {
        TotalEnemy = 5;
        TotalTurret = 5;
        OneEnemyUICountChangeEvent?.Invoke(TotalEnemy);
        OneTurretUICountChangeEvent?.Invoke(TotalTurret);

        for (int i = 0; i < 5; i++)
        {
            int xPos = UnityEngine.Random.Range(-20, 20);
            int zPos = UnityEngine.Random.Range(-20, 20);
            Vector3 pos = new Vector3(xPos, 0, zPos);
            GameObject obj = Instantiate(enemyPrefab, pos, Quaternion.identity);

            obj.transform.position = pos;
            obj.transform.SetParent(battleField.transform);
            
            enemyObj[i] = obj;
            enemies[i] = obj.GetComponent<Enemy>();
        }
        for (int i = 0; i < 5; i++)
        {
            int xPos = UnityEngine.Random.Range(-20, 20);
            int zPos = UnityEngine.Random.Range(-20, 20);
            Vector3 pos = new Vector3(xPos, 0, zPos);
            GameObject obj = Instantiate(turretPrefab, pos, Quaternion.identity);

            obj.transform.position = pos;
            obj.transform.SetParent(battleField.transform);
            turretObj[i] = obj;
            turrets[i] = obj.GetComponent<Turret>();
        }
    }
    void Prepare()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].HP = UnityEngine.Random.Range(100, 150);
            enemies[i].ATK = UnityEngine.Random.Range(100, 150);
            enemies[i].Prepare(turretObj);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            turrets[i].HP = UnityEngine.Random.Range(100, 150);
            turrets[i].ATK = UnityEngine.Random.Range(100, 150);
            turrets[i].Prepare(enemyObj);
        }
    }

    void BeginGame()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Begin();
            turrets[i].Begin();          
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Prepare();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BeginGame();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].LookAtTarget();
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                turrets[i].LookAtTarget();
            }
        }
    }

    public void DestroyAll()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Turret"))
        {
            Destroy(obj);
            Debug.Log("Destroy" + obj.name);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
            Debug.Log("Destroy" + obj.name);
        }
    }
    public void StopShooting()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Turret"))
        {
            Turret turret = obj.GetComponent<Turret>();
            if (turret != null) turret.StopShooting();
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            if (enemy != null) enemy.StopShooting();
        }
    }
    public void OneTurretRemove()
    {
        TotalTurret = TotalTurret - 1;
        OneTurretUICountChangeEvent?.Invoke(TotalTurret);
        if (TotalTurret <= 0)
        {
            Debug.Log("TotalTurret:" + TotalTurret);
            OnWinnerEvent?.Invoke("Enemy Win");
            StopShooting();
            DestroyAll();
        }
    }
    public void OneEnemyRemove()
    {
        TotalEnemy = TotalEnemy - 1;
        OneEnemyUICountChangeEvent?.Invoke(TotalEnemy);
        if (TotalEnemy <= 0)
        {
            Debug.Log("TotalEnemy:" + TotalEnemy);
            OnWinnerEvent?.Invoke("Turret Win");
            StopShooting();
            DestroyAll();
        }
    }
    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");
    }
}
