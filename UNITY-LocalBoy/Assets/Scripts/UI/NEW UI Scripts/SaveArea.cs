using UnityEngine.UI;
using UnityEngine;

public class SaveArea : MonoBehaviour
{
    [SerializeField] RectTransform _CanvasRect;
    RectTransform rectTransform;
    public float sim;
    Vector2 size;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        float widthRatio = _CanvasRect.rect.width / Screen.width;
        float heightRatio = _CanvasRect.rect.height / Screen.height;

        float offsetTop = (Screen.safeArea.yMax - Screen.height) * heightRatio;
        float offsetBottom = (Screen.safeArea.yMin) * heightRatio;
        float offsetRight = (Screen.safeArea.xMax - Screen.width) * widthRatio;
        float offsetLeft = (Screen.safeArea.xMin) * widthRatio;

        rectTransform.offsetMax = new Vector2(offsetRight, offsetTop);
        rectTransform.offsetMin = new Vector2(offsetLeft, offsetBottom);
        CanvasScaler canvasScaler = _CanvasRect.GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(canvasScaler.referenceResolution.x, 
            canvasScaler.referenceResolution.y + Mathf.Abs(offsetTop) + Mathf.Abs(offsetBottom));
    }
}
