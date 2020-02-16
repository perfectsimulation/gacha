using System;

[Serializable]
public class StageData
{
    public string id;
    public int level;
    public int stage;
    public int[] scoreTier;
    public NoteData[] notes;
    public StageData() { }
    public StageData(int level, int stage, int[] scoreTier, NoteData[] notes)
    {
        this.id = string.Format("{0}-{1}", level, stage);
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.notes = notes;
    }
}

[Serializable]
public class NoteData
{
    public float xPosition;
    public float zPosition;
    public float zScale; // length of note
    public NoteData() { }
    public NoteData(float xPos, float zPos, float zScl)
    {
        this.xPosition = xPos;
        this.zPosition = zPos;
        this.zScale = zScl;
    }
}
