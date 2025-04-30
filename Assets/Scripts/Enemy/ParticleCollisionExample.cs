using UnityEngine;

public class ParticleCollisionExample : MonoBehaviour
{
    public ParticleSystem myps; 
    public LayerMask mask;  
    [SerializeField] BaseItem baseItem;
    void Start()
    {
        //mask로 특정 레이어에만 피격할 수 있게 
        var collision = myps.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.collidesWith = mask;// LayerMask.GetMask(mask);
    }

    void OnParticleCollision(GameObject other)
    {
        // 적이 피격되었나를 어떻게 판단할까? 
        // 적이 피격 혹은 옆에 있는 건물이 피격 될 수도 있으니까.
        
        if (other.GetComponent<BaseItem>()) //피격된 상대가 base item이면 (turret, enemy)
        {
            Debug.Log(other.name + " hit");
            other.GetComponent<BaseItem>().CheckHP(baseItem.ATK);
        }

    }
}