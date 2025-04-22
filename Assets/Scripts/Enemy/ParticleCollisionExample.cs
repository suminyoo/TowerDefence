using UnityEngine;

public class ParticleCollisionExample : MonoBehaviour
{
    public ParticleSystem particleSystem; 
    public LayerMask mask;  
    void Start()
    {
        var collision = particleSystem.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.collidesWith = mask;// LayerMask.GetMask(mask);
    }

    void OnParticleCollision(GameObject other)
    {
        

        // 적이 피격되었나를 어떻게 판단할까? 
        // 적이 피격 혹은 옆에 있는 건물이 피격 될 수도 있으니까.
        // 
        if (other.GetComponent<Enemy>())
        {
            Debug.Log("Enemy hit");
            other.GetComponent<Enemy>().TakeDamage(1);
        }

        if (other.GetComponent<Turret>())
        {
            Debug.Log("Turret hit");
            other.GetComponent<Turret>().TakeDamage(1);
        }
    }
}