using UnityEngine;

public class UI_BuildButtons : MonoBehaviour
{
    private UI_Animator uiAnim;
    [SerializeField] private float yPositionOffset;
    [SerializeField] private float openAnimationDuration = .1f;

    public bool isActive;
    private UI_BuildButtonOnHoverEffect[] buildButtons;

    private void Awake()
    {
        uiAnim = GetComponentInParent<UI_Animator>();
        buildButtons = GetComponentsInChildren<UI_BuildButtonOnHoverEffect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ShowBuildButtons();
    }

    public void ShowBuildButtons()
    {
        isActive = !isActive;

        float yOffset = isActive ? yPositionOffset : -yPositionOffset;
        float methodDelay = isActive ? openAnimationDuration : 0;

        uiAnim.ChangePosition(transform, new Vector3(0, yOffset), openAnimationDuration);
        Invoke(nameof(ToggleButtonMovement), methodDelay);
    }

    private void ToggleButtonMovement()
    {
        foreach (var button in buildButtons)
        {
            button.ToggleMovement(isActive);
        }
    }
}
