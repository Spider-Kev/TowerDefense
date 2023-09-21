using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesFactory : MonoBehaviour
{
    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to all object pooling of allies
    /// </summary>
    public StructAllyPooling[] allyPooling;
    /// <summary>
    /// Reference to cell pointing
    /// </summary>
    public CellPointing cellPointing;
    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        if (cellPointing == null)
            cellPointing = FindObjectOfType<CellPointing>();
    }

    /// <summary>
    /// Listen when an object needs a new Ally spawned
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("SpawnAlly", OnAskedSpawnAlly);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SpawnAlly", OnAskedSpawnAlly);
    }

    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// When an ally is asked to spawn, cast the event manager for the kind of ally to spawn and creates it on the
    /// position of the cell pointing
    /// </summary>
    /// <param name="arg0"></param>
    private void OnAskedSpawnAlly(object arg0)
    {
        var itemFound = Array.Find(allyPooling, pooler => pooler.allyPooling == (AlliesScriptable)arg0);
        cellPointing.selectedCell.SetAlly(itemFound.pooling.AskForObject(cellPointing.selectedCell.transform.position).GetComponent<Ally>());
        cellPointing.selectedCell.asociatedAlly.SetAllyInScene(cellPointing.selectedCell);
        cellPointing.RestartSelectedCell();
    }
    #endregion
}
