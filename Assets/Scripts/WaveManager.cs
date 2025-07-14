using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public GridBuilder nextGrid;
    public EnemyPortal[] enemyPortals;
    public int basicEnemy;
    public int fastEnemy;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GridBuilder currentGrid;
    public bool waveCompleted;
    public float timeBetweenWaves = 10f;
    public float wavetimer;

    [SerializeField] private WaveDetails[] levelWaves;
    private int waveIndex;
    
    private float chechInterval = 0.5f;
    private float nextCheckTime;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject basicEnemy;
    [SerializeField] private GameObject fastEnemy;

    private List<EnemyPortal> enemyPortals;

    private void Awake()
    {
       enemyPortals = new List<EnemyPortal>(FindObjectsOfType<EnemyPortal>());
    }

    private void Start()
    {
        SetupNextWave();
    }

    private void Update()
    {
        HandleWaveCompletion();
        HandleWaveTiming();
    }

    private void HandleWaveCompletion()
    {
        if (!ReadyToCheck())
            return;

        if (waveCompleted == false && AllEnemiesDefeated())
        {
            CheckForNewLevelLayout();
            waveCompleted = true;
            wavetimer = timeBetweenWaves;
        }
    }

    private void HandleWaveTiming()
    {
        if (waveCompleted)
        {
            wavetimer -= Time.deltaTime;
            if (wavetimer <= 0)
            {
                SetupNextWave();

                wavetimer = timeBetweenWaves;
            }
        }
    }

    public void ForceNextWave()
    {
        if(!AllEnemiesDefeated())
        { 
            Debug.Log("Cannot force next wave, there are still active enemies.");
            return; 
        }

        SetupNextWave();
    }

    [ContextMenu("Setup Next Wave")]
    private void SetupNextWave()
    {
        List<GameObject> newEnemies = NewEnemyWave();
        int portalIndex = 0;
        
        if (newEnemies == null)
        {
            Debug.Log("No enemies to spawn for this wave.");
            return;
        }

        for (int i=0; i < newEnemies.Count; i++)
        {
            GameObject enemyToAdd = newEnemies[i];
            EnemyPortal portalToReceiveEnemy = enemyPortals[portalIndex];
            
            portalToReceiveEnemy.AddEnemy(enemyToAdd);

            portalIndex++;
            
            if (portalIndex >= enemyPortals.Count)
                portalIndex = 0;

        }

        waveCompleted = false;
    }

    private List<GameObject> NewEnemyWave()
    {
        if (waveIndex >= levelWaves.Length)
        {
            Debug.Log("No more waves available.");
            return null;
        }

        List<GameObject> newEnemyList = new List<GameObject>();

        for (int i = 0; i < levelWaves[waveIndex].basicEnemy; i++)
        {
            newEnemyList.Add(basicEnemy);
        }

        for (int i = 0; i < levelWaves[waveIndex].fastEnemy; i++)
        {
            newEnemyList.Add(fastEnemy);
        }

        waveIndex++;

        return newEnemyList;
    }

    private void CheckForNewLevelLayout()
    {
        if (waveIndex >= levelWaves.Length)
            return;

        WaveDetails nextWave = levelWaves[waveIndex];

        if(nextWave.nextGrid != null)
        {
            UpdatedLevelTiles(nextWave.nextGrid);
            EnableNewPortals(nextWave.enemyPortals);
        }

        currentGrid.UpdateNavMesh();
    }

    private void UpdatedLevelTiles(GridBuilder nextGrid)
    {
        List<GameObject> grid = currentGrid.GetTileSetup();
        List<GameObject> newGrid = nextGrid.GetTileSetup();

        for (int i = 0; i < grid.Count; i++)
        {
            TileSlot currentTile = grid[i].GetComponent<TileSlot>();
            TileSlot newTile = newGrid[i].GetComponent<TileSlot>();


            bool shouldBeUpdated = currentTile.GetMesh() != newTile.GetMesh() ||
                                   currentTile.GetMaterial() != newTile.GetMaterial() ||
                                   currentTile.GetAllChildren().Count != newTile.GetAllChildren().Count ||
                                   currentTile.transform.rotation != newTile.transform.rotation;

            if (shouldBeUpdated)
            {
                currentTile.gameObject.SetActive(false);

                newTile.gameObject.SetActive(true);
                newTile.transform.parent = currentGrid.transform;

                grid[i] = newTile.gameObject;
                Destroy(currentTile.gameObject);
            }
        }
    }

    private void EnableNewPortals(EnemyPortal[] newPortals)
    {
        foreach (EnemyPortal portal in newPortals)
        {
            portal.gameObject.SetActive(true);
            enemyPortals.Add(portal);
        }
    }
    private bool AllEnemiesDefeated()
    {
        foreach (EnemyPortal portal in enemyPortals)
        {
            if (portal.GetActiveEnemies().Count > 0)
            {
                return false; // There are still active enemies
            }
        }
        return true; // All enemies defeated
    }

    private bool ReadyToCheck()
    {
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + chechInterval;
            return true;
        }
        return false;
    }
}
