using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

//오늘 과제:  터렛과 에너미가 교전하는 로직 구현. 
//  로직으로 승패 결정하기. 랜덤으로 HP, 설정, 살상력 Damage  
// HP random (90,  100)   
// Damage    (40, 200)  
// 



public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _TurretAmount;  // UI에텍스트 필드.   
    public TextMeshProUGUI _EnemyAmount;  // UI에 텍스트 필드.
    
    public int TotalTurret=5;              //최초 터렛 갯수. 
    public int TotalEnemy=5;              //최초 터렛 갯수. 

    public GameObject panelStart;
    public GameObject panelResult;

    public Button startButton;
    public Button quitButtton;
    public Button againButton;

    public GameManager gameManager;


    void Start()
    {
        panelStart.SetActive(true);
        panelResult.SetActive(false);
        Turret.StaticDestroyEvent += OneTurretRemove;
        Enemy.OnDestroyEnemy += OneEnemyRemove;

        _EnemyAmount.text = TotalEnemy.ToString();
        _TurretAmount.text = TotalTurret.ToString();
    }

    private void Update()
    {
        if (TotalTurret ==  0 || TotalEnemy == 0)
        {
            GameResult();
        }
        againButton.onClick.AddListener(GameAgain);
        quitButtton.onClick.AddListener(GameQuit);
    }

    public void OneTurretRemove()
    {
        TotalTurret = TotalTurret - 1; 
        _TurretAmount.text = TotalTurret.ToString();  
    }
    public void OneEnemyRemove()
    {
        TotalEnemy = TotalEnemy - 1;
        _EnemyAmount.text = TotalEnemy.ToString();
    }

    public void GameResult ()
    {
        panelResult.SetActive(true);
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

        //게임을 다시하는 로직
        //GameManager에서 다시 Instantiate를 하면 됨
        //과제1 터렛이나 적이 0이 되는 시점에서 panel을 on하기
        //과제2 남아있는 적이나 터렛의 파티클 정지. 에너미 터렛 클레스에서 처리하기
        //과제3 again 버튼 클릭시 게임 재시작 잔존하는 게임 오브젝트 전부 제거하고 
        //GameManager에 어느 메서드를 실행하면 됨
        //GameManager.Instantiate
    }


    public GameObject G;

    public void GameAgain()
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

        Debug.Log("GameAgain");


        ///?
    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");


    }

}
