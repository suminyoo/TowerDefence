using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineChange : MonoBehaviour
{
    public SplineAnimate anim;
    public SplineContainer splineA;
    public SplineContainer splineB;
    public GameObject newSplineGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Container = splineA;
            anim.Restart(true);  

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            anim.Container = splineB;
            anim.Restart(true);
        }
    }
}
