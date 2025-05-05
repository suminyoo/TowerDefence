using UnityEngine;
using UnityEngine.UI;
public class PlaceObjectBelowButton : MonoBehaviour
{
    public Button uiButton;             // Assign in inspector
    public GameObject targetObject;     // The GameObject to move
    public Camera uiCamera;             // Assign the UI camera (or use Camera.main if appropriate)
    public float offsetY = -50f;        // Offset in screen pixels (negative is below)

    void Start()
    {
        // Get the position of the button in screen space
        Vector3 buttonScreenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, uiButton.GetComponent<RectTransform>().position);

        // Apply Y offset to move below
        Vector3 belowScreenPos = new Vector3(buttonScreenPos.x, buttonScreenPos.y + offsetY, buttonScreenPos.z);

        // Convert to world position
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                uiButton.GetComponent<RectTransform>(), belowScreenPos, uiCamera, out worldPos))
        {
            targetObject.transform.position = worldPos;
        }
    }
}
