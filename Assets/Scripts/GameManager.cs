using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject turretPrefab;

    public Transform world;
    public Transform battleField;

    public GameObject[] enemyObj = new GameObject[5];
    public GameObject[] turretObj = new GameObject[5];


    Enemy[] enemies = new Enemy[5];
    Turret[] turrets = new Turret[5];

    // canvas
    // render mode -> world space
    // reset
    // scale 0.001 0.001
    // ui canvas, image, text(textmashpro)
    // drag apply (propfull tab tab)
    // override
     
    void Start()
    {
        GameObject obj;
        for (int i = 0; i < 5; i++)
        {
            obj = Initialize01(enemyPrefab);
            obj.name = enemyPrefab.name + i;
            enemyObj[i] = obj;
            enemies[i] = obj.GetComponent<Enemy>();
            obj = Initialize01(turretPrefab);
            obj.name = turretPrefab.name + i;
            turretObj[i] = obj;
            turrets[i] = obj.GetComponent<Turret>();
        }

        Prepare();


    }
    //1x -30 ~ 0
    //1z 30 ~ -30
    //2x  0 ~ 30
    //2z 30 ~ -30

    

    GameObject Initialize01(GameObject tmpPrefab)
    {
        int xPos = Random.Range(-30, 30);
        int zPos = Random.Range(-30, 30);
        Vector3 pos = new Vector3(xPos, 0, zPos);

        GameObject obj = Instantiate(tmpPrefab, pos, Quaternion.identity);

        obj.transform.position = pos;
        obj.transform.SetParent(battleField.transform);
        

        return obj;

    }

    void Prepare()
    {
        for (int i = 0; i < enemies.Length; i++ )
        {
            enemies[i].HP = Random.Range(100, 150);
            enemies[i].ATK = Random.Range(100, 150);
            enemies[i].Prepare(turretObj);
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].HP = Random.Range(100, 150);
            turrets[i].ATK = Random.Range(100, 150);
            turrets[i].Prepare(enemyObj);
        }
    }

    void BeginGame()
    {
        for (int i = 0; i < enemies.Length; ++i)
        { 
            enemies[i].Begin();

        }
        for (int i = 0; i < turrets.Length; ++i)
        {
            turrets[i].Begin();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            BeginGame();
        }
    }
}
