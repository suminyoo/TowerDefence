using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject nearestTarget;


    // Start is called before the first frame update
    //static method�� �ν��Ͻ�ȭ ���� �ʰ� ����� �� ����
    public static GameObject FindNearestTarget(GameObject self, GameObject[] targets)
    {

        GameObject nearestTarget;
        float closestDistance = Mathf.Infinity;
        nearestTarget = self;

        foreach (GameObject target in targets)
        {
            float distance = 
                Vector3.Distance(self.transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = target;
            }
#if (UNITY_EDITOR)
            if(nearestTarget != null)
            {
                Debug.Log("Nearest Target: " + nearestTarget.name);
            }
#endif
            
        }
        return nearestTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
