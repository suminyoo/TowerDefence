using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CameraViewControl : MonoBehaviour
{
    public Camera mainCamera;
    public Camera viewPointCamera;
    public Transform targetObj;

    public GameObject turretViewPointPanel;
    public Button nextTurretBtn;
    public Button prevTurretBtn;

    Vector3 targetPos;
    Quaternion targetRot;
    Vector3 oriPos;
    Quaternion oriRot;

    Turret[] turrets = new Turret[5];
    int TurretNum = 0;

    UIManager UImain;


    // Start is called before the first frame update
    void Start()
    {
        turretViewPointPanel.SetActive(false);
        viewPointCamera.enabled = false;
        

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

    }

    public void TurretViewModeActive()
    {
        mainCamera.enabled = false;
        viewPointCamera.enabled = true;
        UImain.ShowInGamePanel(false);
        turretViewPointPanel.SetActive(true);

    }


    public void NextTurretView()
    {
        TurretNum += 1;
        if (TurretNum > 4)
        {
            TurretNum = 0 ;
        }
    }
    public void PrevTurretView()
    {
        TurretNum -= 1;
        if (TurretNum < 0)
        {
            TurretNum = 4;
        }
    }

    

    void MoveToTargetViewPoint()
    {
        viewPointCamera.transform.position = turrets[TurretNum].gameObject.transform.position;
        viewPointCamera.transform.rotation = turrets[TurretNum].gameObject.transform.rotation;
    }

    public void ReturnToOriView()
    {
        viewPointCamera.transform.position = oriPos;
        viewPointCamera.transform.rotation = oriRot;
    }
}
