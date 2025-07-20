using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    private CameraController cameraController;
    [Header("Keyboard Sensitivity")]
    [SerializeField] private Slider keyboardSensitivity;
    [SerializeField] private TextMeshProUGUI keyboardSensitivityText;
    [SerializeField] private string keyboadSensitivityParam = "KeyboardSensitivity";

    [SerializeField] private float minKeyboardSensitivity = 60f;
    [SerializeField] private float maxKeyboardSensitivity = 240f;

    [Header("Mouse Sensitivity")]
    [SerializeField] private Slider mouseSensitivity;
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    [SerializeField] private string mouseSensitivityParam = "MouseSensitivity";

    [SerializeField] private float minMouseSensitivity = 1;
    [SerializeField] private float maxMouseSensitivity = 10;

    private void Awake()
    {
        cameraController = FindFirstObjectByType<CameraController>();
    }

    public void KeyboardSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minKeyboardSensitivity, maxKeyboardSensitivity, value);
        cameraController.AdjustKeyboardSensitivity(newSensitivity);
        keyboardSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void MouseSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minMouseSensitivity, maxMouseSensitivity, value);
        cameraController.AdjustMouseSensitivity(newSensitivity);
        mouseSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboadSensitivityParam, keyboardSensitivity.value);
        PlayerPrefs.SetFloat(mouseSensitivityParam, mouseSensitivity.value);
    }

    private void OnEnable()
    {
        keyboardSensitivity.value = PlayerPrefs.GetFloat(keyboadSensitivityParam, 0.5f);
        mouseSensitivity.value = PlayerPrefs.GetFloat(mouseSensitivityParam, 0.6f);

        KeyboardSensitivity(keyboardSensitivity.value);
        MouseSensitivity(mouseSensitivity.value);
    }
}
