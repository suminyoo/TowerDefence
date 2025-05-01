using UnityEngine;

public class Drag3D : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }
    private void OnMouseUp()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseDrag()
    {
        

        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
