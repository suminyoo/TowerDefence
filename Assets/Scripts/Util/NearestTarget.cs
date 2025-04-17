using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject nearestTarget;


    // Start is called before the first frame update
    //static method는 인스턴스화 하지 않고 사용할 수 있음
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
