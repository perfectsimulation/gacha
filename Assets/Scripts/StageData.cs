using System;
using System.Collections.Generic;

[Serializable]
public class StageData
{
    public string description;
    public int level;
    public int stage;
    public int[] scoreTier;
    public CardBonus cardBonus;
    public List<NoteData> notes;
    public List<ItemDrop> itemDrops;

    public StageData() { }

    public StageData(string description, int level, int stage, int[] scoreTier, CardBonus cardBonus, List<NoteData> notes, List<ItemDrop> itemDrops)
    {
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
public class MetaData
{
    public bool isComplete;
    public float highScore;
    public List<string> prerequisiteStageIds;
    public int remainingDailyAttempts;

    public MetaData() { }

    public MetaData(List<string> prerequisiteStageIds)
    {
        this.isComplete = false;
        this.highScore = 0f;
        this.prerequisiteStageIds = prerequisiteStageIds;
        this.remainingDailyAttempts = 3;
    }

    public void DecrementDailyAttempt()
    {
        this.remainingDailyAttempts--;
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
    public ItemData item;
    public float dropChance;

    public ItemDrop(ItemData item, float dropChance)
    {
        this.item = item;
        this.dropChance = dropChance;
    }
}
