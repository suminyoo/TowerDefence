using UnityEngine;
using UnityEngine.UI;

public class ParticleCollisionExample : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public LayerMask mask; //layer�� ����ũ �������ָ� ������ ��ü�� �ǰ�
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

        // ���� �ǰݵǾ����� ��� �Ǵ��ұ�? 
        // ���� �ǰ� Ȥ�� ���� �ִ� �ǹ��� �ǰ� �� ���� �����ϱ�.
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