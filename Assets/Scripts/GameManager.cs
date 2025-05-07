using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    
    public GameObject enemyPrefab;
    public GameObject turretPrefab;
    public GameObject battleField;

    public GameObject[] turretObj = new GameObject[5];
    public GameObject[] enemyObj = new GameObject[5];
    public float EnemyInterval = 5.0f;


    [SerializeField] UIManager mainUI;
    Enemy[] enemies = new Enemy[5];
    Turret[] turrets = new Turret[5];

    public GameObject splineForEnemyObj;
    public SplineContainer splineForEnem;

    int TotalEnemy;
    int TotalTurret;
    void Start()
    {
        Turret.OnDestroyTurret += Turret_OnDestroyTurret;
        Enemy.OnDestroyEnemy += Enemy_OnDestroyEnemy;

        //static으로 하지 않고 인스턴스화 해서 줌 static과 차이점
        //UIManager.OnGameEndEventStatic += UIManager_OnGameEndEvent
        mainUI.OnGameEndEvent += UIManager_OnGameEndEvent;
        mainUI.OnGameAgainEvent += UIManager_OnGameAgainEvent;


        TotalEnemy = 5;
        TotalTurret = 5;
        mainUI.TotalEnemy = TotalEnemy;
        mainUI.TotalTurret = TotalTurret;
        StartCoroutine(Initialize());


    }

    private void Enemy_OnDestroyEnemy()
    {
        TotalEnemy--;
        mainUI.TotalEnemy = TotalEnemy;
        if (TotalEnemy <= 0)
        {
            mainUI.ShowWinLosePanel("Turret Win");
        }
    }

    private void Turret_OnDestroyTurret()
    {
        TotalTurret--;
        mainUI.TotalTurret = TotalTurret;
        if (TotalTurret <= 0)
        {
            mainUI.ShowWinLosePanel("Enemy Win");
        }
    }

    private void UIManager_OnGameAgainEvent()
    {
        ClearBattleField();

        TotalEnemy = 5;
        TotalTurret = 5;
        mainUI.TotalEnemy = TotalEnemy;
        mainUI.TotalTurret = TotalTurret;

        StartCoroutine(Initialize());
    }
    private void UIManager_OnGameEndEvent()
    {
        CeaseFire();
        ClearBattleField();
    }

    void ClearBattleField()
    {
        int numChildren = battleField.transform.childCount;
        for (int i = numChildren - 1; i > 0; i--)
            GameObject.Destroy(battleField.transform.GetChild(i).gameObject);
    }

    void CeaseFire()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in targets)
        {
            enemy.GetComponent<Enemy>().CeaseFire();
        }

        targets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<Turret>().CeaseFire();
        }
    }



    IEnumerator Initialize()
    {
        for (int i = 0; i < 5; i++)
        {
            int xPos = Random.Range(-20, 20);
            int zPos = Random.Range(-20, 20);
            Vector3 pos = new Vector3(xPos, 0, zPos);
            GameObject obj = Instantiate(enemyPrefab, pos, Quaternion.identity);

            obj.transform.position = pos;
            obj.transform.SetParent(battleField.transform);
            enemyObj[i] = obj;
            enemies[i] = obj.GetComponent<Enemy>();
            obj.GetComponent<SplineAnimate>().StartOffset = Random.Range(0, 1);

            //터렛의 순차적인 생성을 위해 코루틴 사용
            yield return new WaitForSeconds(EnemyInterval);
        }
        for (int i = 0; i < 5; i++)
        {
            int xPos = -10 + (i * 5);
            int zPos = -10;
            Vector3 pos = new Vector3(xPos, 0, zPos);
            GameObject obj = Instantiate(turretPrefab, pos, Quaternion.identity);

            obj.transform.position = pos;
            obj.transform.SetParent(battleField.transform);
            turretObj[i] = obj;
            turrets[i] = obj.GetComponent<Turret>();
        }
        Prepare();
    }
    void Prepare()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].HP = Random.Range(1000, 1500);
            enemies[i].ATK = Random.Range(10, 30);
        }

        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].HP = Random.Range(1000, 1500);
            turrets[i].ATK = Random.Range(10, 30);
        }
        mainUI.ShowStartBtn(true);
    }

    public void StartGame()
    {
        mainUI.ShowStartBtn(false) ;
        mainUI.ShowInGamePanel(true);

        for (int i = 0; i < enemies.Length; i++)
        {
            turrets[i].Begin();
            enemies[i].Begin();
        }
        
    }



}
