using UnityEngine;

public class UIHyperlink : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenLink()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }
}
