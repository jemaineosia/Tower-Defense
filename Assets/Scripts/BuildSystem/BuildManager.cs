using System.Collections.Generic;
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

    public void MakeBuildSlotNotAvailableIfNeeded(WaveManager waveManager, GridBuilder currentGrid)
    {
        foreach (var wave in waveManager.GetLevelWaves())
        {
            if (wave.nextGrid == null) continue;

            List<GameObject> grid = currentGrid.GetTileSetup();
            List<GameObject> newWaveGrid = wave.nextGrid.GetTileSetup();

            for (int i = 0; i < grid.Count; i++)
            {
                TileSlot currentTile = grid[i].GetComponent<TileSlot>();
                TileSlot nextTile = newWaveGrid[i].GetComponent<TileSlot>();

                bool tileNotTheSame = currentTile.GetMesh() != nextTile.GetMesh() ||
                    currentTile.GetMaterial() != nextTile.GetMaterial() ||
                    currentTile.GetAllChildren().Count != nextTile.GetAllChildren().Count ||
                    currentTile.transform.rotation != nextTile.transform.rotation;

                if (tileNotTheSame == false) continue;

                BuildSlot buildSlot = grid[i].GetComponent<BuildSlot>();

                if (buildSlot != null)
                    buildSlot.SetSlotAvailableTo(false);
            }
        }
    }

    public void CancelBuildAction()
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
