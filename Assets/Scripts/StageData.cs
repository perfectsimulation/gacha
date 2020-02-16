using System;

[Serializable]
public class StageData
{
    public string id;
    public int level;
    public int stage;
    public int[] scoreTier;
    public CardBonus cardBonus;
    public NoteData[] notes;

    public StageData() { }

    public StageData(int level, int stage, int[] scoreTier, CardBonus[] cardBonus, NoteData[] notes)
    {
        this.id = string.Format("{0}-{1}", level, stage);
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.cardBonus = cardBonus[0];
        this.notes = notes;
    }

    public StageData(int level, int stage, int[] scoreTier, CardBonus cardBonus, NoteData[] notes)
    {
        this.id = string.Format("{0}-{1}", level, stage);
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.cardBonus = cardBonus;
        this.notes = notes;
    }
}

[Serializable]
public class CardBonus
{
    // Score multipliers of Card properties
    public float redX;
    public float orangeX;
    public float yellowX;
    public float greenX;
    public float blueX;
    public float purpleX;
    public float pinkX;

    public CardBonus()
    {
        this.redX = 0f;
        this.orangeX = 0f;
        this.yellowX = 0f;
        this.greenX = 0f;
        this.blueX = 0f;
        this.purpleX = 0f;
        this.pinkX = 0f;
    }

    public CardBonus(float red, float orange, float yellow, float green, float blue, float purple, float pink)
    {
        this.redX = red;
        this.orangeX = orange;
        this.yellowX = yellow;
        this.greenX = green;
        this.blueX = blue;
        this.purpleX = purple;
        this.pinkX = pink;
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
