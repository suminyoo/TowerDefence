using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy enemy;
    public Turret turret;

    public GameObject enemyPrefab;
    public GameObject turretPrefab;

    public Transform world;
    public Transform battleField;

    public GameObject[] enemyObj = new GameObject[5];
    public GameObject[] turretObj = new GameObject[5];

    public GameObject[] Obj = new GameObject[5];

    Enemy[] enemies = new Enemy[5];
    Turret[] turrets = new Turret[5];   

    void Start()
    {
            
        Initialize();
        //Initialize01(enemyObj, enemies, enemyPrefab);
        //Initialize01(turretObj, turrets, turretPrefab);
        Prepare();

    }
    //void Initialize01(GameObject[] tmpObj, Enemy[] objects, GameObject tmpPrefab)
    //{
    //    for (int i = 0; i< 5; i++)
    //    {
    //        int xPos = Random.Range(-30, 30);
    //        int zPos = Random.Range(-30, 30);
    //        Vector3 pos = new Vector3(xPos, 0, zPos);

    //        GameObject obj = Instantiate(tmpPrefab, pos, Quaternion.identity);

    //        obj.transform.position = pos;
    //        obj.transform.SetParent(battleField.transform);

    //        tmpObj[i] = obj;
    //        objects[i] = obj.GetComponent<Enemy>();
    //    }

    //}


    void Initialize( )
    {
        for (int i = 0; i < 5; i++)
        {
            int xPos = Random.Range(-30, 30);
            int zPos = Random.Range(-30, 30);
            Vector3 pos = new Vector3(xPos, 0, zPos);
            GameObject obj = Instantiate(enemyPrefab, pos, Quaternion.identity);

            obj.transform.position = pos;
            obj.transform.SetParent(battleField.transform);
            enemyObj[i] = obj;
            enemies[i] = obj.GetComponent<Enemy>();
        }
        for (int i = 0; i < 5; i++)
        {
            int xPos = Random.Range(-30, 30);
            int zPos = Random.Range(-30, 30);
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
        enemy.HP = Random.Range(100, 150);
        turret.HP = Random.Range(100, 150);

        enemy.ATK = Random.Range(100, 150);
        turret.ATK = Random.Range(100, 150);
    }


}
