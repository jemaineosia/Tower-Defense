using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI_Animator uiAnim;
    private RectTransform rectTransform;

    [SerializeField] private float showcaseScale = 1.1f;
    [SerializeField] private float scaleUpDuration = 0.25f;
    private void Awake()
    {
        uiAnim = GetComponentInParent<UI_Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiAnim.ChangeScale(rectTransform, showcaseScale, scaleUpDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiAnim.ChangeScale(rectTransform, 1, scaleUpDuration);
    }



}
