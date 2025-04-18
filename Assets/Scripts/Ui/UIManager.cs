using TMPro;
using UnityEngine;

//오늘 과제:  터렛과 에너미가 교전하는 로직 구현. 
//  로직으로 승패 결정하기. 랜덤으로 HP, 설정, 살상력 Damage  
// HP random (90,  100)   
// Damage    (40, 200)  
// 



public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _TurrentAmount;  // UI에텍스트 필드.   
    public TextMeshProUGUI _EnemyAmount;  // UI에 텍스트 필드.
    
    public int TotalTurret = 5;              //최초 터렛 갯수. 
    public int TotalEnemy = 5;              //최초 터렛 갯수. 

   
    void Start()
    {       
        Turret.StaticDestroyEvent += OneTurretRemove;
        Enemy.OnDestroyEnemy += OneEnemyRemove; //등록 연산자 OnDestroyEnemy 이벤트 OneEnemyRemove 발생하면 실행하게 되어있음

        _EnemyAmount.text = TotalEnemy.ToString();
        _TurrentAmount.text = TotalTurret.ToString();
    }

    public void OneTurretRemove()
    {
        TotalTurret = TotalTurret - 1; 
        _TurrentAmount.text = TotalTurret.ToString();  
    }
    public void OneEnemyRemove()
    {
        TotalEnemy = TotalEnemy - 1;
        _EnemyAmount.text = TotalEnemy.ToString();
    }   

}
