using System.Collections.Generic;
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
        Initialize();

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
            enemies[i].HP = Random.Range(100000, 150000);
            enemies[i].ATK = Random.Range(100, 150);
            enemies[i].Prefare(turretObj);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            turrets[i].HP = UnityEngine.Random.Range(100000, 150000);
            turrets[i].ATK = UnityEngine.Random.Range(100, 150);
            turrets[i].Prefare(enemyObj);
        }
    }

    void BeginGame()
    {
        manager.panelStart.SetActive(false);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Begin();
            turrets[i].Begin();          

        }
    }
    
    private void Update()
    {
        manager.startButton.onClick.AddListener(BeginGame);
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
                enemies[i].LoatAtTarget();
                   // Prefare(turretObj);
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                turrets[i].LoatAtTarget();
                //turrets[i].Prefare(enemyObj);
            }
        }
    }

    public void RestartGame()
    {
        Initialize();
        Prepare();
        BeginGame();
    }



}
