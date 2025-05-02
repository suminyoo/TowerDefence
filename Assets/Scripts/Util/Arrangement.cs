using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement : MonoBehaviour
{
    //ctrl shift f 게임 내 메인 카메라 시점 이동
    [SerializeField] Camera mainCamera;

    Vector3 originPos; //카메라의 위치 저장
    Vector3 savedAngles; //카메라의 각도 저장 

    [SerializeField] Transform targetTransform;

    public Vector3 targetPos;
    public Vector3 targetAngles;

    Drag3D drag;

    private void Start()
    {
        

        SaveCameraPositionAndRotation();
        //방법1 가볍지만 수정이 불편
        targetPos = new Vector3(2.39163566f, 88.5168152f, 20.5924263f);
        targetAngles = new Vector3(90f, -7.67606798e-06f, 0f);

        //방법2 물체의 좌표값을 받아와서 하는거
        targetPos = targetTransform.transform.position;
        targetAngles = targetTransform.transform.eulerAngles;
    }
    public void MoveCamaraToTopView()
    {
        mainCamera.transform.position = targetPos;
        mainCamera.transform.eulerAngles = targetAngles;
    }

    public void SaveCameraPositionAndRotation()
    {
        originPos = mainCamera.transform.position;
        savedAngles = mainCamera.transform.eulerAngles;
    }
    public void RestoreCameraPositionAndRotation()
    {
        mainCamera.transform.position = originPos;
        mainCamera.transform.eulerAngles = savedAngles;
    }

    


}
