using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

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
