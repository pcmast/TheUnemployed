using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PCCursorController : MonoBehaviour
{
    [Header("Cursor")]
    public RectTransform cursorRect;
    public Canvas pcCanvas;
    public float cursorSpeed = 1000f;

    private Vector2 cursorPosition;

    void Start()
    {
        cursorPosition = cursorRect.anchoredPosition;
    }

    void Update()
    {
        MoveCursor();
        HandleClick();
    }

    void MoveCursor()
{
    float moveX = Input.GetAxis("Mouse X");
    float moveY = Input.GetAxis("Mouse Y");

    cursorPosition += new Vector2(moveX, moveY) * cursorSpeed * Time.deltaTime;

    RectTransform canvasRect = pcCanvas.GetComponent<RectTransform>();

   
    Vector2 cursorSize = cursorRect.sizeDelta / 2f;

    cursorPosition.x = Mathf.Clamp(cursorPosition.x, canvasRect.rect.min.x + cursorSize.x, canvasRect.rect.max.x - cursorSize.x);
    cursorPosition.y = Mathf.Clamp(cursorPosition.y, canvasRect.rect.min.y + cursorSize.y, canvasRect.rect.max.y - cursorSize.y);

    cursorRect.anchoredPosition = cursorPosition;
}
   void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Crear PointerEventData
            PointerEventData pointer = new PointerEventData(EventSystem.current);

            // Convierte la posición del cursor virtual a coordenadas de pantalla
            Camera cam = pcCanvas.worldCamera;
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, cursorRect.position);
            pointer.position = screenPos;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);

            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}