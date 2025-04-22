using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//오늘 과제:  터렛과 에너미가 교전하는 로직 구현. 
//  로직으로 승패 결정하기. 랜덤으로 HP, 설정, 살상력 Damage  
// HP random (90,  100)   
// Damage    (40, 200)  
// 

public class UIManager : MonoBehaviour
{
    public static event Action OnGameEndEvent; //fire and forget
    public static event Action OnGameAgainEvent;
    public static event Action OnGameStartEvent;

    //각각의 클래스는 각자의 역할을 해야하기 때문에 이 이벤트는 게임매니저로 날려줄거임

    [Header("Text")]
    public TextMeshProUGUI _TurretAmount;  // UI에텍스트 필드.   
    public TextMeshProUGUI _EnemyAmount;  // UI에 텍스트 필드.
    public TextMeshProUGUI winnerText;

    [Header("Obj")]
    public GameObject panelStart;
    public GameObject panelResult;

    [Header("Button")]
    public Button startButton;
    public Button quitButtton;
    public Button againButton;

    private int totalTurret;
    public int TotalTurret
    {
        get { return totalTurret; }
        set { 
            totalTurret = value;
            _TurretAmount.text = TotalTurret.ToString();
        }
    }

    private int totalEnemy;
    public int TotalEnemy
    {
        get { return totalEnemy; }
        set { 
            totalEnemy = value;
            _EnemyAmount.text = TotalEnemy.ToString();
        }
    }



    void Start()
    {
        totalEnemy = 5;
        totalTurret = 5;

        panelStart.SetActive(true);
        panelResult.SetActive(false);

        Turret.StaticDestroyEvent += OneTurretRemove;  //이벤트를 듣고 함수실행
        Enemy.OnDestroyEnemy += OneEnemyRemove;
        //static으로 선언되었기때문에 인스턴스없이 부를 수 있음




        againButton.onClick.AddListener(GameAgain);
        quitButtton.onClick.AddListener(GameQuit);
        startButton.onClick.AddListener(GameStart);

    }

    public void OneTurretRemove()
    {
        TotalTurret = TotalTurret - 1;
        if (TotalTurret <= 0)
        {
            winnerText.text = "Enemy Win";
            panelResult.SetActive(true);
            OnGameEndEvent?.Invoke();

        }
    }
    public void OneEnemyRemove()
    {
        TotalEnemy = TotalEnemy - 1;
        if (TotalEnemy <= 0)
        {
            winnerText.text = "Turret Win";
            panelResult.SetActive(true);
            OnGameEndEvent?.Invoke(); //이벤트 발생함수 Invoke

        }
    }

    public void GameStart()
    {
        OnGameStartEvent?.Invoke();
        panelStart.SetActive(false);

    }
    public void GameAgain()
    {
        TotalTurret = 5;
        TotalEnemy = 5;
        OnGameAgainEvent?.Invoke();
        panelResult.SetActive(false);
        Debug.Log("GAME AGAIN");

    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");
    }

}
