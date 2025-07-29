using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<TowerUnlockData> towerUnlockDataList = new List<TowerUnlockData>();

    [ContextMenu("Initialize Tower Data")]
    private void InitializeTowerData()
    {
        towerUnlockDataList.Clear();

        towerUnlockDataList.Add(new TowerUnlockData("Crossbow", false));
        towerUnlockDataList.Add(new TowerUnlockData("Cannon", false));
        towerUnlockDataList.Add(new TowerUnlockData("Rapid Fire Gun", false));
        towerUnlockDataList.Add(new TowerUnlockData("Hammer", false));
        towerUnlockDataList.Add(new TowerUnlockData("Spider Nest", false));
        towerUnlockDataList.Add(new TowerUnlockData("Anti-Air Harpon", false));
        towerUnlockDataList.Add(new TowerUnlockData("Just Fan", false));
    }
}

[System.Serializable]
public class TowerUnlockData
{
    public string towerName;
    public bool isUnlocked;

    public TowerUnlockData(string newTowerName, bool newIsUnlockStatus)
    {
        towerName = newTowerName;
        isUnlocked = newIsUnlockStatus;
    }
}
