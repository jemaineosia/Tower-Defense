using TMPro;
using UnityEngine;

public class UI_BuildButton : MonoBehaviour
{
    private BuildManager buildManager;
    private CameraEffects cameraEffects;
    private GameManager gameManager;

    [SerializeField] private string towerName = "Default Tower";
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private int price = 50;
    [SerializeField] private GameObject towerToBuild;
    [SerializeField] private float towerCenterY = 0.5f;

    private void Awake()
    {
        buildManager = FindFirstObjectByType<BuildManager>();
        cameraEffects = FindFirstObjectByType<CameraEffects>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void BuildTower()
    {
        if(gameManager.HasEnoughCurrency(price) == false)
            return;

        if (towerToBuild == null)
        {
            Debug.LogError("Tower to build is not assigned in the UI_BuildButton script.");
            return;
        }

        BuildSlot slotToUse = buildManager.selectedBuildSlot;
        buildManager.CancelBuildAction();
        slotToUse.SnapToDefaultPositionImmediately();
        slotToUse.SetSlotAvailableTo(false);
        cameraEffects.Screenshake(.15f, .02f);

        GameObject newTower = Instantiate(towerToBuild, slotToUse.GetBuildPosition(towerCenterY), Quaternion.identity);
    }

    private void OnValidate()
    {
        towerNameText.text = towerName;
        gameObject.name = "BuildButtonUI - " + towerName;
    }

}
