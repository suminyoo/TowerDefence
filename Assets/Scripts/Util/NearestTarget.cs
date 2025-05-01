using UnityEngine;

public class NearestTarget : MonoBehaviour
{
    public GameObject[] targets; // Array of target objects
    private GameObject nearestTarget;

    //void Update()
    //{
    //    FindNearestTarget();
    //}

    void FindNearestTarget()
    {
        float closestDistance = Mathf.Infinity;
        nearestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null)
        {
            Debug.Log("Nearest Target: " + nearestTarget.name);
        }
    }
    public static GameObject FindNearestTarget(GameObject selt, GameObject[] targets)
    {
        GameObject nearestTarget;

        float closestDistance = Mathf.Infinity;
        nearestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(selt.transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null)
        {
            Debug.Log("Nearest Target: " + nearestTarget.name);
        }

        return nearestTarget;
    }
}
