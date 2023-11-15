using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedTowerIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    TowerDatabaseSO database;
    GridData towerData;
    ObjectPlacer objectPlacer;
    SoundFeedback soundFeedback;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, TowerDatabaseSO database, GridData towerData, ObjectPlacer objectPlacer, SoundFeedback soundFeedback)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.towerData = towerData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;

       selectedTowerIndex = database.towersData.FindIndex(data => data.ID == ID);
        if (selectedTowerIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.towersData[selectedTowerIndex].PrefabPreview,
                database.towersData[selectedTowerIndex].Size);
        }
        else
            throw new System.Exception($"No tower with ID {iD}");
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedTowerIndex);
        if (placementValidity == false)
        {
            soundFeedback.PlaySound(SoundType.WrongPlacement);
            return;
        }
        soundFeedback.PlaySound(SoundType.Place);

        int index = objectPlacer.PlaceObject(database.towersData[selectedTowerIndex].Prefab, grid.CellToWorld(gridPosition));

        GridData selectedData = towerData;
        selectedData.AddObjectAt(gridPosition, database.towersData[selectedTowerIndex].Size,
            database.towersData[selectedTowerIndex].ID, index);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
        GameManager.instance.BuyTower(database.towersData[database.towersData[selectedTowerIndex].ID].Cost);
    }

    private bool CheckPlacementValidity(Vector3Int gridposition, int selectedTowerIndex)
    {
        GridData selectedData = towerData;

        return selectedData.CanPlaceObjectAt(gridposition, database.towersData[selectedTowerIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedTowerIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
