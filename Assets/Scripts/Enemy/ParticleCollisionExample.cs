using UnityEngine;
using UnityEngine.UI;

public class ParticleCollisionExample : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public LayerMask mask; //layer로 마스크 설정해주면 설정한 개체만 피격
    void Start()
    {
        var collision = particleSystem.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.collidesWith = mask;
        

        //collision.collidesWith = LayerMask.GetMask("Default");
    }

    void OnParticleCollision(GameObject other)
    {

        // 적이 피격되었나를 어떻게 판단할까? 
        // 적이 피격 혹은 옆에 있는 건물이 피격 될 수도 있으니까.
        // 
        if (other.GetComponent<Enemy>() != null)
        {
            Debug.Log("Enemy hit");
            other.GetComponent<Enemy>().CheckHP(5);
        }

        if (other.GetComponent<Turret>())
        {
            Debug.Log("Turret hit" );
            other.GetComponent<Turret>().TakeDamage(5);
        }
    }
}