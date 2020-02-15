using System.Collections.Generic;
using UnityEngine;

public class Stanza : MonoBehaviour
{
    public GameObject NotePrefab;

    private StageManager stageManager;

    void Awake()
    {
        // Get Stage layout from database
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
        this.BuildStanza(this.stageManager.GetStageData().notes);
    }

    private void BuildStanza(NoteData[] notes)
    {
        // Create a Note prefab for each NoteData
    }
}
