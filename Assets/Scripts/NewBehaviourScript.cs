using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cubeCirclePrepab;
    public GameObject cubeSquarePrepab;
    public GameObject cubeTrianglePrepab;
    public GameObject world;
    void Start()
    {
        //Nameing prepab and Obj
        Vector3 pos = new Vector3(0, 0, 0);

        GameObject circleObj = Instantiate(cubeCirclePrepab, pos, Quaternion.identity);
        circleObj.transform.SetParent(world.transform);
        GameObject squareObj = Instantiate(cubeSquarePrepab, pos, Quaternion.identity);
        squareObj.transform.SetParent(world.transform);
        GameObject triangleObj = Instantiate(cubeTrianglePrepab, pos, Quaternion.identity);
        triangleObj.transform.SetParent(world.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
