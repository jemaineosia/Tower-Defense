using TMPro;
using UnityEngine;

public class UI_TextBlinkEffect : MonoBehaviour
{
    private TextMeshProUGUI myText;

    [SerializeField] public float changeValueSpeed;
    private float targetAlpha;
    private bool canBlink;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!canBlink) return;

        if (Mathf.Abs(myText.color.a - targetAlpha) > 0.01f)
        {
            float newAlpha = Mathf.Lerp(myText.color.a, targetAlpha, Time.deltaTime * changeValueSpeed);
            ChangeColorAlpha(newAlpha);
        }
        else
        {
            ChangeTargetAlpha();
        }
    }

    public void EnableBlink(bool enable)
    {
        canBlink = enable;
        
        if (!canBlink)
            ChangeColorAlpha(1);
    }

    private void ChangeTargetAlpha() => targetAlpha = (targetAlpha == 1) ? 0 : 1;

    private void ChangeColorAlpha(float newAlpha)
    {
        Color myColor = myText.color;
        myText.color = new Color(myColor.r, myColor.g, myColor.b, newAlpha);
    }
}
