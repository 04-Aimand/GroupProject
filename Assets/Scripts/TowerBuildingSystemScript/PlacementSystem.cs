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
    private int selectedTowerIndex;
    private int towerCost;

    [SerializeField]
    private GameObject[] gridVisualization;

    private GridData towerData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private List<GameObject> placedGameObjects = new List<GameObject>();
    private void Start()
    {
        StopPlacement();
        towerData = new GridData();
        
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedTowerIndex = database.towersData.FindIndex(data => data.ID == ID);
        if (selectedTowerIndex < 0)
        {
            Debug.LogError($"No ID Found {ID}");
            return;
        }
        towerCost = database.towersData[selectedTowerIndex].Cost;
        if(GameManager.coins < towerCost)
        {
            Debug.Log("Not Enough coins");
            return;
        }
        gridVisualization[0].SetActive(true);
        preview.StartShowingPlacementPreview(database.towersData[selectedTowerIndex].Prefab, database.towersData[selectedTowerIndex].Size);
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

        bool placementValidity = CheckPlacementValidity(gridposition, selectedTowerIndex);
        if (placementValidity == false)
            return;

        GameObject newTower = Instantiate(database.towersData[selectedTowerIndex].Prefab);
        newTower.transform.position = grid.CellToWorld(gridposition);
        placedGameObjects.Add(newTower);
        GridData selectedData = towerData;
        selectedData.AddObjectAt(gridposition, database.towersData[selectedTowerIndex].Size,
            database.towersData[selectedTowerIndex].ID, placedGameObjects.Count - 1);
        //udate coins text
        GameManager.coins -= towerCost;
        preview.UpdatePosition(grid.CellToWorld(gridposition), false);
        StopPlacement();
    }

    private bool CheckPlacementValidity(Vector3Int gridposition, int selectedTowerIndex)
    {
        GridData selectedData = towerData;

        return selectedData.CanPlaceObjectAt(gridposition, database.towersData[selectedTowerIndex].Size);
    }

    private void StopPlacement()
    {
        selectedTowerIndex = -1;
        gridVisualization[0].SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceTower;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if (selectedTowerIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridposition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridposition)
        {
            bool placementValidity = CheckPlacementValidity(gridposition, selectedTowerIndex);
            preview.UpdatePosition(grid.CellToWorld(gridposition), placementValidity);
            lastDetectedPosition = gridposition;
        }
        

    }
}
