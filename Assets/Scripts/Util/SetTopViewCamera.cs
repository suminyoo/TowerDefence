using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SetTopViewCamera : MonoBehaviour
{
    public static event Action OnTopViewEvent;
    public static event Action OnSideViewEvent;

    public Button topViewButton;
    public Button sideViewButton;

    public Camera mainCamera;
    public Transform targetObj;

    Vector3 targetPos;
    Quaternion targetRot;

    Vector3 oriPos;
    Quaternion oriRot;

    public float moveSpeed = 5.1f;

    private void Start()
    {



        ViewButtonActive(true);


        targetPos = targetObj.position;
        targetRot = targetObj.rotation;

        oriPos = mainCamera.transform.position;
        oriRot = mainCamera.transform.rotation;
    }

    //
    //카메라가 확 바뀌지 않게 코루틴써서 속도 조절

    IEnumerator ChangeCameraPosRotCor(Vector3 pos, Quaternion rot)
    {
        ViewButtonActive(false);

        while (true)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pos, moveSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, rot, moveSpeed * Time.deltaTime);

            yield return null; //1프레임동안 cpu한테 제어권을 줌

            float diffePos = Mathf.Abs(mainCamera.transform.position.x - pos.x);
            float diffeRot = Mathf.Abs(mainCamera.transform.rotation.x - rot.x);

            if (diffePos < 0.001f && diffeRot < 0.001f) break;
        }

        ViewButtonActive(true);

    }

    void ViewButtonActive(bool boo)
    {
        topViewButton.interactable = boo;
        sideViewButton.interactable = boo;

    }

    public void TopView()
    {
        OnTopViewEvent?.Invoke();
        StartCoroutine(ChangeCameraPosRotCor(targetPos, targetRot));
        //StartCoroutine(ChangeCameraPosCor(targetPos));
        //StartCoroutine(ChangeCameraRotCor(targetRot));

    }
    public void SideView()
    {
        OnSideViewEvent?.Invoke();
        StartCoroutine(ChangeCameraPosRotCor(oriPos, oriRot));
        //StartCoroutine(ChangeCameraPosCor(oriPos));
        //StartCoroutine(ChangeCameraRotCor(oriRot));

    }
    public void SimpleTopView()
    {
        mainCamera.transform.position = targetPos;
        mainCamera.transform.rotation = targetRot;
    }
    public void SimpleSideView()
    {
        mainCamera.transform.position = oriPos;
        mainCamera.transform.rotation = oriRot;
    }
}