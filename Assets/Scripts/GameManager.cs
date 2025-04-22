using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject turretPrefab;
    public GameObject battleField;

    public GameObject[] turretObj = new GameObject[5];
    public GameObject[] enemyObj = new GameObject[5];

    Enemy[] enemies = new Enemy[5];
    Turret[] turrets = new Turret[5];

    public UIManager manager;

    void Start()
    {
        UIManager.OnGameAgainEvent += UIManager_OnGameAgainEvent;
        UIManager.OnGameEndEvent += UIManager_OnGameEndEvent;
        UIManager.OnGameStartEvent += UIManager_OnGameStartEvent;

        //이벤트와 리스너, 퍼블리셔와 섭스크라이버
        //OnGameAgainEvent 이벤트 발생시 UIManager_OnGameAgainEvent 실행
        //UI와 GM은 분리되어야함

        Initialize();
        Prepare();
        //StartCoroutine(SampleCor());

    }

    private void UIManager_OnGameStartEvent()
    {
        BeginGame();
    }

    private void UIManager_OnGameEndEvent()
    {
        StopShooting();
        DestroyAll();
    }

    private void UIManager_OnGameAgainEvent()
    {
    Again();
    }

    public IEnumerator Again()
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
        BeginGame();
    }


    IEnumerator SampleCor () //Coroutine Cor로 이름넣어줌
    {
        PlayA();
        yield return null; 
        PlayB();
        yield return null;
        PlayC();
        yield return new WaitForSeconds(3); //3초
        PlayD();
    }
    private void PlayA()
    {
        Debug.Log("PlayA");
    }
    private void PlayB()
    {
        Debug.Log("PlayB");
    }
    private void PlayC()
    {
        Debug.Log("PlayC");
    }
    private void PlayD() {
        Debug.Log("PlayD");
    }

    void Initialize()
    {
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
                   // Prefare(turretObj);
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                turrets[i].LookAtTarget();
                //turrets[i].Prefare(enemyObj);
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
            if (turret != null)
            {
                turret.StopShooting();
            }

        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.StopShooting();
            }
        }
    }

}
