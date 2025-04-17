using TMPro;
using UnityEngine;

//���� ����:  �ͷ��� ���ʹ̰� �����ϴ� ���� ����. 
//  �������� ���� �����ϱ�. �������� HP, ����, ���� Damage  
// HP random (90,  100)   
// Damage    (40, 200)  
// 



public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _TurretAmount;  // UI���ؽ�Ʈ �ʵ�.   
    public TextMeshProUGUI _EnemyAmount;  // UI�� �ؽ�Ʈ �ʵ�.
    
    public int TotalTurret=5;              //���� �ͷ� ����. 
    public int TotalEnemy=5;              //���� �ͷ� ����. 

   
    void Start()
    {       
        Turret.StaticDestroyEvent += OneTurretRemove;
        Enemy.OnDestroyEnemy += OneEnemyRemove;

        _EnemyAmount.text = TotalEnemy.ToString();
        _TurretAmount.text = TotalTurret.ToString();
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

}
