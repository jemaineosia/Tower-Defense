using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    
    private UI_Settings settingsUI;
    private UI_MainMenu mainMenuUI;
    private UI_InGame inGameUI;

    private void Awake()
    {
        settingsUI = GetComponentInChildren<UI_Settings>(true);
        mainMenuUI = GetComponentInChildren<UI_MainMenu>(true);
        inGameUI = GetComponentInChildren<UI_InGame>(true);

        SwitchTo(mainMenuUI.gameObject);
        //SwitchTo(settingsUI.gameObject);
        //SwitchTo(inGameUI.gameObject);
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
