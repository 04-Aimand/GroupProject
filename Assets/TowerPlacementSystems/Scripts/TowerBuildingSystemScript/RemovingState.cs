using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameTowerIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    TowerDatabaseSO database;
    GridData towerData;
    ObjectPlacer objectPlacer;
    SoundFeedback soundFeedback;

    public RemovingState(Grid grid, PreviewSystem previewSystem, TowerDatabaseSO database, GridData towerData, ObjectPlacer objectPlacer, SoundFeedback soundFeedback)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.towerData = towerData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (towerData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = towerData;
        }

        if (selectedData == null)
        {
            soundFeedback.PlaySound(SoundType.Click);
        }
        else
        {
            soundFeedback.PlaySound(SoundType.Remove);
            gameTowerIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameTowerIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameTowerIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectedIsValid(gridPosition));
    }

    private bool CheckIfSelectedIsValid(Vector3Int gridPosition)
    {
        return (!towerData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectedIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition),validity);
    }
}
