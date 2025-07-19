using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    
    private UI_Settings settingsUI;
    private UI_MainMenu mainMenuUI;

    private void Awake()
    {
        settingsUI = GetComponentInChildren<UI_Settings>(true);
        mainMenuUI = GetComponentInChildren<UI_MainMenu>(true);

        SwitchTo(mainMenuUI.gameObject);
        SwitchTo(settingsUI.gameObject);
    }

    public void SwitchTo(GameObject uiElement)
    {
        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
