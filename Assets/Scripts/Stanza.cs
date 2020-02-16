using UnityEngine;

public class Stanza : MonoBehaviour
{
    public GameObject NotePrefab;

    private StageManager stageManager;

    void Awake()
    {
        // Stage data has been loaded and cached in StageManager
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
        this.BuildStanza(this.stageManager.GetStageData());
    }

    // Layout the stage by instantiating Note prefabs from NoteData
    private void BuildStanza(StageData stageData)
    {
        // Create a Note prefab for each NoteData
        NoteData[] notes = stageData.notes;
        foreach (NoteData noteData in notes)
        {
            GameObject notePrefab = Instantiate(this.NotePrefab);
            Note noteComponent = notePrefab.GetComponent<Note>();
            noteComponent.SetPositionAndScale(noteData.xPosition, noteData.zPosition, noteData.zScale);
        }
    }
}
