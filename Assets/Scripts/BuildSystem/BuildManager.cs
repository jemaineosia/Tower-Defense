using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BuildSlot selectedBuildSlot;
    private UI ui;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && selectedBuildSlot != null)
            CancelBuildAction();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                bool clickedNotOnBuildSlot = hit.collider.GetComponent<BuildSlot>() == null;

                if(clickedNotOnBuildSlot)
                    CancelBuildAction();
            }
        }
    }

    private void CancelBuildAction()
    {
        if (selectedBuildSlot == null)
            return;
        
        selectedBuildSlot.UnselectTile();
        selectedBuildSlot = null;
        DisableBuildMenu();
    }

    public void SelectBuildSlot(BuildSlot buildSlot)
    {
        if (selectedBuildSlot != null)
            selectedBuildSlot.UnselectTile();

        selectedBuildSlot = buildSlot;
    }

    public void EnableBuildMenu()
    { 
        if(selectedBuildSlot != null) return;

        ui.buildButtonsUI.ShowBuildButtons(true);
    }

    public void DisableBuildMenu()
    {
        ui.buildButtonsUI.ShowBuildButtons(false);
    }

    public BuildSlot GetSelectedSlot() => selectedBuildSlot;
}
