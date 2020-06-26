using System.Collections.Generic;
using UnityEngine;

public class Stanza : MonoBehaviour
{
    public GameObject NotePrefab;

    private StoryManager storyManager;

    void Awake()
    {
        // Stage data has been loaded and cached in StoryManager
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;
        this.BuildStanza(this.storyManager.GetStageData());
    }

    // Layout the stage by instantiating Note prefabs from NoteData
    private void BuildStanza(StageData stageData)
    {
        // Create a Note prefab for each NoteData
        List<NoteData> notes = stageData.notes;
        foreach (NoteData noteData in notes)
        {
            GameObject notePrefab = Instantiate(this.NotePrefab);
            Note noteComponent = notePrefab.GetComponent<Note>();
            noteComponent.SetPositionAndScale(noteData.xPosition, noteData.zPosition, noteData.zScale);
        }
    }
}
