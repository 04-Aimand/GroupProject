using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private TowerDatabaseSO database;
    private int towerCost;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData towerData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectplacer;

    IBuildingState buildingState;

    [SerializeField]
    private SoundFeedback soundFeedback;

    [SerializeField]
    private Text Tower1Cost, Tower2Cost, Tower3Cost;

    
    private void Start()
    {
        StopPlacement();
        towerData = new GridData();
        
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        
        towerCost = database.towersData[database.towersData.FindIndex(data => data.ID == ID)].Cost;
        if(GameManager.instance.coins < towerCost)
        {
            Debug.Log("Not Enough coins");
            return;
        }

        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, grid, preview, database, towerData, objectplacer, soundFeedback);
        
        inputManager.OnClicked += PlaceTower;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, database, towerData, objectplacer, soundFeedback) ;
        inputManager.OnClicked += PlaceTower;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceTower()
    {
        if (inputManager.IsPointerOverUI() == true)
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridposition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridposition);

        StopPlacement();
    }

    //private bool CheckPlacementValidity(Vector3Int gridposition, int selectedTowerIndex)
    //{
    //    GridData selectedData = towerData;

    //    return selectedData.CanPlaceObjectAt(gridposition, database.towersData[selectedTowerIndex].Size);
    //}

    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceTower;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        CostCheck();

        if (buildingState == null)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridposition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridposition)
        {
            buildingState.UpdateState(gridposition);
            lastDetectedPosition = gridposition;
        }
    }

    private void CostCheck()
    {
        Tower1Cost.color = (5 < GameManager.instance.coins) ? Color.green : Color.red;
        Tower2Cost.color = (15 < GameManager.instance.coins) ? Color.green : Color.red;
        Tower3Cost.color = (30 < GameManager.instance.coins) ? Color.green : Color.red;
    }
}
