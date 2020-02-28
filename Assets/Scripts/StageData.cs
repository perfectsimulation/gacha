using System;
using System.Collections.Generic;

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

    public StageData(SerializedStageData serializedData)
    {
        this.description = serializedData.description;
        this.level = serializedData.level;
        this.stage = serializedData.stage;
        this.scoreTier = serializedData.scoreTier;
        this.cardBonus = serializedData.cardBonus;
        this.notes = Serializer.ListFromArray<NoteData>(serializedData.notes);
        this.itemDrops = Serializer.ListFromArray<ItemDrop>(serializedData.itemDrops);
    }
}

[Serializable]
public class SerializedStageData
{
    public string description;
    public int level;
    public int stage;
    public int[] scoreTier;
    public CardBonus cardBonus;
    public NoteData[] notes;
    public ItemDrop[] itemDrops;

    public SerializedStageData() { }

    public SerializedStageData(StageData stageData)
    {
        this.description = stageData.description;
        this.level = stageData.level;
        this.stage = stageData.stage;
        this.scoreTier = stageData.scoreTier;
        this.cardBonus = stageData.cardBonus;
        this.notes = Serializer.ListToArray(stageData.notes);
        this.itemDrops = Serializer.ListToArray(stageData.itemDrops);
    }
}

public class MetaData
{
    public int level;
    public int stage;
    public bool isComplete;
    public float highScore;
    public List<string> prerequisiteStageIds;
    public int remainingDailyAttempts;

    public MetaData() { }

    public MetaData(int level, int stage, List<string> prerequisiteStageIds)
    {
        this.level = level;
        this.stage = stage;
        this.isComplete = false;
        this.highScore = 0f;
        this.prerequisiteStageIds = prerequisiteStageIds;
        this.remainingDailyAttempts = 3;
    }

    public MetaData(SerializedMetaData serializedData)
    {
        this.level = serializedData.level;
        this.stage = serializedData.stage;
        this.isComplete = serializedData.isComplete;
        this.highScore = serializedData.highScore;
        this.prerequisiteStageIds = Serializer.ListFromArray<string>(serializedData.prerequisiteStageIds);
        this.remainingDailyAttempts = serializedData.remainingDailyAttempts;
    }

    public void DecrementDailyAttempt()
    {
        this.remainingDailyAttempts--;
    }
}

[Serializable]
public class SerializedMetaData
{
    public int level;
    public int stage;
    public bool isComplete;
    public float highScore;
    public string[] prerequisiteStageIds;
    public int remainingDailyAttempts;

    public SerializedMetaData() { }

    public SerializedMetaData(MetaData metaData)
    {
        this.level = metaData.level;
        this.stage = metaData.stage;
        this.isComplete = metaData.isComplete;
        this.highScore = metaData.highScore;
        this.prerequisiteStageIds = Serializer.ListToArray(metaData.prerequisiteStageIds);
        this.remainingDailyAttempts = metaData.remainingDailyAttempts;
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
