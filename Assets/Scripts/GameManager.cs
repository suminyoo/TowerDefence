using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy enemy;
    public Turret turret;

    public GameObject enemyPrefab;
    public GameObject turretPrefab;
    public Transform world;
    public Transform battleField;



    void Start()
    {
        int xPos = UnityEngine.Random.Range(2, 10);
        int zPos = UnityEngine.Random.Range(2, 10);

        //vector position
        Vector3 pos = new Vector3(xPos, 0, zPos);

        //생성
        //Instantiate(enemyPrefab, pos, Quaternion.identity).transform.SetParent(world);
        //생성한 world의 자식으로 들어감 (Script메뉴에서 드래그 설정)
        Initialize();

        enemy.HP = UnityEngine.Random.Range(100, 150);
        turret.HP = UnityEngine.Random.Range(100, 150);

        enemy.ATK = UnityEngine.Random.Range(100, 150);
        turret.ATK = UnityEngine.Random.Range(100, 150);

    }

    void Initialize()
    {
        for (int i = 0; i < 10; i++)
        {
            int xPos = UnityEngine.Random.Range(-5, 10);
            int zPos = UnityEngine.Random.Range(-5, 10);

            Vector3 pos = new Vector3(xPos, 0, zPos);
            Instantiate(enemyPrefab, pos, Quaternion.identity).transform.SetParent(battleField);
        }
    }


}
