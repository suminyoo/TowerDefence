using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    float _zoomSpeed = 30f;
    [SerializeField]
    float _zoomMax = 15f;
    [SerializeField]
    float _zoomMin = 100f;

    [SerializeField]
    float _RotateSpeed = -1f;
    [SerializeField]
    float _dragSpeed = 3f;

    bool _dragging = false;
    bool _rotating = false;


    void OnEnable()
    {
        DragObject.OnObjectDragEvent += OnDragging;
        DragObject.OnObjectDragEndEvent += OffDragging;
        SetTopViewCamera.OnTopViewEvent += OnRotating;
        SetTopViewCamera.OnSideViewEvent += OffRotating;
    }

    void OnDisable()
    {
        DragObject.OnObjectDragEvent -= OnDragging;
        DragObject.OnObjectDragEndEvent -= OffDragging;
        SetTopViewCamera.OnTopViewEvent -= OnRotating;
        SetTopViewCamera.OnSideViewEvent -= OffRotating;
    }

    private void OnDragging(BaseItem obj)
    {
        _dragging = true;
    }
    private void OffDragging()
    {
        _dragging = false;
    }
    private void OnRotating()
    {
        _rotating = true;
    }
    private void OffRotating()
    {
        _rotating = false;
    }

    private void LateUpdate()
    {

        CameraZoom();
        if(!_dragging)
            CameraDrag();
        if (!_rotating)
            CameraRotate();
    }

    void CameraRotate()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector3 rotateValue = new Vector3(y, x * -1, 0);
            transform.eulerAngles = transform.eulerAngles - rotateValue;
            transform.eulerAngles += rotateValue * _RotateSpeed;
        }
    }

    void CameraZoom()
    {
        float _zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y <= _zoomMax && _zoomDirection > 0)
            return;

        if (transform.position.y >= _zoomMin && _zoomDirection < 0)
            return;

        transform.position += transform.forward * _zoomDirection * _zoomSpeed;
    }


    void CameraDrag()
    {
        if (Input.GetMouseButton(0))
        {
            float posX = Input.GetAxis("Mouse X");
            float posZ = Input.GetAxis("Mouse Y");

            Quaternion v3Rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            transform.position += v3Rotation * new Vector3(posX * -_dragSpeed, 0, posZ * -_dragSpeed); // 플레이어의 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 차
        }
    }

}