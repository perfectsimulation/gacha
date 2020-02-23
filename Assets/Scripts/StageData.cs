using System;

[Serializable]
public class StageData
{
    public string id;
    public string description;
    public int level;
    public int stage;
    public int[] scoreTier;
    public CardBonus cardBonus;
    public NoteData[] notes;
    public ItemDrop[] itemDrops;

    public StageData() { }

    public StageData(string description, int level, int stage, int[] scoreTier, CardBonus[] cardBonus, NoteData[] notes, ItemDrop[] itemDrops)
    {
        this.id = string.Format("{0}-{1}", level, stage);
        this.description = description;
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.cardBonus = cardBonus[0];
        this.notes = notes;
        this.itemDrops = itemDrops;
    }

    public StageData(string description, int level, int stage, int[] scoreTier, CardBonus cardBonus, NoteData[] notes, ItemDrop[] itemDrops)
    {
        this.id = string.Format("{0}-{1}", level, stage);
        this.description = description;
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.cardBonus = cardBonus;
        this.notes = notes;
        this.itemDrops = itemDrops;
    }
}

[Serializable]
public class CardBonus
{
    // Score multipliers of Card properties
    public float waterX;
    public float airX;
    public float hotX;
    public float coldX;

    public CardBonus()
    {
        this.waterX = 0f;
        this.airX = 0f;
        this.hotX = 0f;
        this.coldX = 0f;
    }

    public CardBonus(float water, float air, float hot, float cold)
    {
        this.waterX = water;
        this.airX = air;
        this.hotX = hot;
        this.coldX = cold;
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

[Serializable]
public class ItemDrop
{
    public int itemId;
    public float dropChance;

    public ItemDrop(int itemId, float dropChance)
    {
        this.itemId = itemId;
        this.dropChance = dropChance;
    }
}
