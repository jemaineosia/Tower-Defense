using UnityEngine;

public class UI_BuildButtons : MonoBehaviour
{
    [SerializeField] private float yPositionOffset;
    
    public bool isActive;
    private UI_Animator uiAnimator;

    private void Awake()
    {
        uiAnimator = GetComponentInParent<UI_Animator>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed - Toggling build buttons");
            ToggleBuildButtons();
        }
    }

    public void ToggleBuildButtons()
    { 
        isActive = !isActive;

        float yOffset = isActive ? -yPositionOffset : yPositionOffset;
        Vector3 offset = new Vector3(0, yOffset);

        uiAnimator.ChangePosition(transform, offset);
    }
}
