using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{

    private UI_Animator uiAnimator;

    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private TextMeshProUGUI CurrencyText;
    [Space]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float waveTimerOffset;
    [SerializeField] UI_TextBlinkEffect waveTimerTextBlinkEffect;

    private void Awake()
    {
        uiAnimator = GetComponentInParent<UI_Animator>();
        if (uiAnimator == null)
        {
            Debug.LogError("UI_Animator component not found on UI_InGame.");
        }
    }

    public void UpdateHealthPoints(int value, int maxHp)
    {
        int newValue = maxHp - value;
        healthPointsText.text = "Threat : " + newValue.ToString() + "/" + maxHp.ToString();
    }

    public void UpdateCurrencyUI(int value) => CurrencyText.text = "Resources : " + value.ToString();
    public void UpdateWaveText(float waveNumber) => waveText.text = "Next Wave: " + waveNumber.ToString("00");
    public void EnableWaveTimer(bool enable) 
    { 
        Transform waveTimerTransform = waveText.transform.parent;
        
        float yOffset = enable ? -waveTimerOffset : waveTimerOffset;
        Vector3 offset = new Vector3(0, yOffset);


        uiAnimator.ChangePosition(waveTimerTransform, offset);
        waveTimerTextBlinkEffect.EnableBlink(enable);
        //waveText.transform.parent.gameObject.SetActive(enable); 
    }

    public void ForceWaveButton()
    {
        WaveManager waveManager = FindFirstObjectByType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.ForceNextWave();
        }
    }
}