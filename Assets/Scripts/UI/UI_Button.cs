using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI_Animator uiAnim;
    private RectTransform rectTransform;

    [SerializeField] private float showcaseScale = 1.1f;
    [SerializeField] private float scaleUpDuration = 0.25f;

    private Coroutine scaleCoroutine;
    [Space]
    [SerializeField] private UI_TextBlinkEffect myTextBlinkEffect;


    private void Awake()
    {
        uiAnim = GetComponentInParent<UI_Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        
        scaleCoroutine = StartCoroutine(uiAnim.ChangeScaleCo(rectTransform, showcaseScale, scaleUpDuration));

        if (myTextBlinkEffect != null)
            myTextBlinkEffect.EnableBlink(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(uiAnim.ChangeScaleCo(rectTransform, 1, scaleUpDuration));

        if (myTextBlinkEffect != null)
            myTextBlinkEffect.EnableBlink(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.one;
    }
}
