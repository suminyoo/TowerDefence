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
    public GameObject panelTurret;

    [Header("Button")]
    public Button startButton;
    public Button quitButtton;
    public Button againButton;

    void Start()
    {
        panelTurret.SetActive(false);
        panelStart.SetActive(true);
        panelResult.SetActive(false);

        GameManager.OneEnemyUICountChangeEvent += OneEnemyCountChangeUI;
        GameManager.OneTurretUICountChangeEvent += OneTurretCountChangeUI;

        GameManager.OnWinnerEvent += WinnerUI;

        againButton.onClick.AddListener(GameAgain);
        quitButtton.onClick.AddListener(GameQuit);
        startButton.onClick.AddListener(GameStart);
    }


    public void OneTurretCountChangeUI(int count)
    {
        _TurretAmount.text = count.ToString();
    }
    public void OneEnemyCountChangeUI(int count)
    {
        _EnemyAmount.text = count.ToString();
    }
    public void WinnerUI(string winText)
    {
        winnerText.text = winText;
        panelResult.SetActive(true);
    }

    public void GameStart()
    {
        OnGameStartEvent?.Invoke();
        panelStart.SetActive(false);

    }
    public void GameAgain()
    {
        OnGameAgainEvent?.Invoke();
        panelResult.SetActive(false);
        Debug.Log("GAME AGAIN");

    }

    public void GameQuit()
    {
        OnGameEndEvent?.Invoke();
    }

}
