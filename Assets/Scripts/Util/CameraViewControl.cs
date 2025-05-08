using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI turretName;
    public TextMeshProUGUI viewModeText;

    private List<Transform> turretList = new List<Transform>();
    private int currentIndex = -1; // 초기엔 아무 것도 선택 안 됨

    public Transform targetTurret; // 현재 선택된 터렛
    public Transform targetBarrel; // 회전하는 총구 (건 배럴)의 Transform

    [SerializeField] UIManager mainUI;
    bool viewMode;


    private void Start()
    {
        mainUI.OnGameAgainEvent += Initialized;

        
        Initialized();


    }
    public void Initialized()
    {
        currentIndex = 0;
        viewMode = false;
        viewModeText.text = "Turret\nView";
        turretViewPointPanel.SetActive(false);
        viewPointCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public void TurretViewMode()
    {

        if (!viewMode)
        {
            OnTurretViewModeActive();
            viewMode = true;
            viewModeText.text = "Return";
        }
        else
        {
            OffTurretViewMode();
            viewMode = false;
            viewModeText.text = "Turret\nView";

        }
    }
    private void OnTurretViewModeActive()
    {
        mainCamera.enabled = false;
        viewPointCamera.enabled = true;
        turretViewPointPanel.SetActive(true);
    }
    private void OffTurretViewMode()
    {
        mainCamera.enabled = true;
        viewPointCamera.enabled = false;
        turretViewPointPanel.SetActive(false);
    }

    public void NextTurretView()
    {
        UpdateTurretList();

        if (turretList.Count == 0)
            return;

        currentIndex = (currentIndex + 1) % turretList.Count;
        MoveToTargetViewPoint(turretList[currentIndex]);
    }

    public void PrevTurretView()
    {
        UpdateTurretList();

        if (turretList.Count == 0)
            return;

        currentIndex = (currentIndex - 1 + turretList.Count) % turretList.Count;
        MoveToTargetViewPoint(turretList[currentIndex]);
    }

    private void UpdateTurretList()
    {
        turretList.Clear();
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

        foreach (GameObject turret in turrets)
        {
            if (turret != null && turret.activeInHierarchy)
                turretList.Add(turret.transform);
        }

        // 현재 선택된 인덱스가 리스트 범위에서 벗어났는지 확인
        if (turretList.Count == 0)
            currentIndex = -1;
        else if (currentIndex >= turretList.Count)
            currentIndex = 0;
    }
    private void MoveToTargetViewPoint(Transform target)
    {
        turretName.text = target.name;
        targetTurret = target;
        targetBarrel = target.transform.Find("GatelingGun_L3_Base/GatelingGun_L3_BaseRotation/GatelingGun_L3_Arm/GatelingGun_L3_GunBody/GatelingGun_L3_Barrel");
        transform.position = targetTurret.position;
        transform.rotation = targetTurret.rotation;

    }

    private void LateUpdate()
    {
        if (targetBarrel != null)
        {
            // 회전 방향 기준으로 뒤에서 5f, 위로 2f 떨어진 위치
            Vector3 offset = -targetBarrel.forward * 5f + Vector3.up * 2f;
            transform.position = targetBarrel.position + offset;

            // 총구 바라보기
            transform.LookAt(targetBarrel.position + targetBarrel.forward * 10f); // 총구 앞 방향 보기
        }
    }


}
