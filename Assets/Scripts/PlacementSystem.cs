using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private TowerDatabaseSO database;
    private int selectedTowerIndex;

    [SerializeField]
    private GameObject[] gridVisualization;

    private GridData floorData, towerData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObjects = new List<GameObject>();
    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        towerData = new GridData();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
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
        gridVisualization[0].SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceTower;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceTower()
    {
        if (inputManager.IsPointerOverUI())
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
        GridData selectedData = database.towersData[selectedTowerIndex].ID == 0 ? floorData : towerData;
        selectedData.AddObjectAt(gridposition, database.towersData[selectedTowerIndex].Size,
            database.towersData[selectedTowerIndex].ID, placedGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridposition, int selectedTowerIndex)
    {
        GridData selectedData = database.towersData[selectedTowerIndex].ID == 0 ? floorData : towerData;

        return selectedData.CanPlaceObjectAt(gridposition, database.towersData[selectedTowerIndex].Size);
    }

    private void StopPlacement()
    {
        selectedTowerIndex = -1;
        gridVisualization[0].SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceTower;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedTowerIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridposition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridposition, selectedTowerIndex);
        previewRenderer.material.color = placementValidity ? Color.green : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridposition);
    }
}
