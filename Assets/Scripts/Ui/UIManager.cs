using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action OnGameEndEvent;
    public event Action OnGameAgainEvent;
    //public static event Action OnGameEndEventStatic; static이면 UIManager.event로 부를 수 있음

    public TextMeshProUGUI _TurretAmount;  // UI에 텍스트 필드.   
    public TextMeshProUGUI _EnemyAmount;  // UI에 텍스트 필드.
    public GameObject _panelMain;
    public GameObject _panelWinLose;
    public GameObject _panelInfo;
    public GameObject _panelBeforeStart;

    public TextMeshProUGUI _dragTargetHP;
    public TextMeshProUGUI _dragTargetATK;
    public TextMeshProUGUI _winnerIs;
    public Button _StartBtn;

    private int totalTurret;
    public int TotalTurret
    {
        get { return totalTurret; }
        set { totalTurret = value;

            _TurretAmount.text = TotalTurret.ToString();
        }
    }
    private int totalEnemy;
    public int TotalEnemy
    {
        get { return totalEnemy; }
        set { totalEnemy = value;

            _EnemyAmount.text = TotalEnemy.ToString();
            
        }
    }
    void OnEnable()
    {
        SetTopViewCamera.OnTopViewEvent += ActivateDragMode;
        SetTopViewCamera.OnSideViewEvent += DeactivateDragMode;
        DragObject.OnObjectDragEvent += ActivateInfoPanel;
        DragObject.OnObjectDragEndEvent += DeactivateInfoPanel;
    }

    void OnDisable()
    {
        SetTopViewCamera.OnTopViewEvent -= ActivateDragMode;
        SetTopViewCamera.OnSideViewEvent -= DeactivateDragMode;
        DragObject.OnObjectDragEvent -= ActivateInfoPanel;
        DragObject.OnObjectDragEndEvent -= DeactivateInfoPanel;
    }

    void Start()
    {
        ShowStartBtn(false);
        _panelBeforeStart.gameObject.SetActive(false);
        _panelWinLose.gameObject.SetActive(false);
        _panelInfo.gameObject.SetActive(false);

    }

    void ActivateDragMode()
    {
        _StartBtn.gameObject.SetActive(false);
    }
    void DeactivateDragMode()
    {
        _StartBtn.gameObject.SetActive(true);
    }

    public void ActivateInfoPanel(BaseItem obj)
    {
        _dragTargetHP.text = obj.HP.ToString();
        _dragTargetATK.text = obj.ATK.ToString();
        _panelInfo.gameObject.SetActive(true);

    }
    public void DeactivateInfoPanel()
    {
        _panelInfo.gameObject.SetActive(false);

    }

    public void ShowWinLosePanel(string winner)
    {
        _winnerIs.text = winner;
        _panelWinLose.gameObject.SetActive(true);
    }

    public void ShowStartBtn(bool boo)
    {
        _StartBtn.gameObject.SetActive(boo);
        _panelBeforeStart.gameObject.SetActive(boo);

        //_startBtn.enabled = boo; //버튼 비활성화 할 수 있음
    }

    public void Again()
    {
        ShowStartBtn(false);
       
        _panelWinLose.gameObject.SetActive(false);
        
        OnGameAgainEvent?.Invoke();
    }

    public void Quit()
    {
        OnGameEndEvent?.Invoke();
        Application.Quit();
    }
}
