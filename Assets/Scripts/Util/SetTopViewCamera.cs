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

    //public float moveSpeed = 5.1f;
    public float duration = 1.5f;

    private void Start()
    {
        topViewButton.interactable = true;
        sideViewButton.interactable = false;

        targetPos = targetObj.position;
        targetRot = targetObj.rotation;

        oriPos = mainCamera.transform.position;
        oriRot = mainCamera.transform.rotation;
    }

    //
    //카메라가 확 바뀌지 않게 코루틴써서 속도 조절

    IEnumerator ChangeCameraPosRotCor(Vector3 pos, Quaternion rot)
    {
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            mainCamera.transform.position = Vector3.Lerp(startPos, pos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, rot, t);
            elapsed += Time.deltaTime;
            yield return null; //1프레임동안 cpu한테 제어권을 줌
            
        }
        mainCamera.transform.position = pos;
        mainCamera.transform.rotation = rot;

        if(mainCamera.transform.position == oriPos) 
            topViewButton.interactable = true;
        else
            sideViewButton.interactable = true;

    }


    public void TopView()
    {
        OnTopViewEvent?.Invoke();
        topViewButton.interactable = false;
        sideViewButton.interactable = false;
        StartCoroutine(ChangeCameraPosRotCor(targetPos, targetRot));

    }
    public void SideView()
    {
        OnSideViewEvent?.Invoke();
        topViewButton.interactable = false;
        sideViewButton.interactable = false;
        StartCoroutine(ChangeCameraPosRotCor(oriPos, oriRot));


    }

}