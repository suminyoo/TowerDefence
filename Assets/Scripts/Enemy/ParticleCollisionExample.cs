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
        

        // ���� �ǰݵǾ����� ��� �Ǵ��ұ�? 
        // ���� �ǰ� Ȥ�� ���� �ִ� �ǹ��� �ǰ� �� ���� �����ϱ�.
        // 
        if (other.GetComponent<Enemy>())
        {
            Debug.Log("Enemy hit");
            other.GetComponent<Enemy>().CheckHP(31);
        }

        if (other.GetComponent<Turret>())
        {
            Debug.Log("Turret hit");
            other.GetComponent<Turret>().TakeDamage(1);
        }
    }
}