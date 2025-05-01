using UnityEngine;

//오늘 과제. 1.  비주얼 스튜디오 에 있는 git  살펴보기.
// 2. 충돌지점에서 이펙을 플레이.   --커프링 방식    디자이너가 테스트할 수 있도록 하기위해 
// 3. 충돌지점에서 이펙을 플레이.   --디커프링 방식  선호하게 되지마. 디자이너는 이걸 어려워한다. 
// 4. 월드 꾸미기 ( 타워가 배치되 월드 꾸미기)   에세  

public class FireProjectile : MonoBehaviour
{    
    public GameObject projectile;          // The projectile prefab    
    public Transform  firePoint;           // The point from which the projectile will be fired
    public float launchVelocity = 700f;     

    public Rigidbody fireRigidbody;
    void Update()
    {       
        if(Input.GetKeyDown(KeyCode.F))           
        {          
            Fire();
        }
    }
    //다른 방식도 테스트 해 봅시다.  이해하기  transform
    void Fire()
    {
        Rigidbody p = Instantiate(fireRigidbody, firePoint.position, firePoint.rotation);    
        p.velocity = firePoint.forward * launchVelocity;
    }
    
}
