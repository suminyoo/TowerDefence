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
        StopShooting();
        DestroyAll();
        Initialize();
        Prepare();
        BeginGame();
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
            enemies[i].Prefare(turretObj);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            turrets[i].HP = UnityEngine.Random.Range(100, 150);
            turrets[i].ATK = UnityEngine.Random.Range(100, 150);
            turrets[i].Prefare(enemyObj);
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
