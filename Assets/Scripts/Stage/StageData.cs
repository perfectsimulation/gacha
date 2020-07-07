using System;
using System.Collections.Generic;

public class StageData
{
    public string description;
    public int level;
    public int stage;
    public int[] scoreTier;
    public ElementalPower elementalPower;
    public List<NoteData> notes;
    public List<StageItemDrop> stageItemDrops;

    public StageData() { }

    public StageData(string description, int level, int stage, int[] scoreTier, ElementalPower elementalPower, List<NoteData> notes, List<StageItemDrop> stageItemDrops)
    {
        this.description = description;
        this.level = level;
        this.stage = stage;
        this.scoreTier = scoreTier;
        this.elementalPower = elementalPower;
        this.notes = notes;
        this.stageItemDrops = stageItemDrops;
    }

    public StageData(SerializedStageData serializedData)
    {
        this.description = serializedData.description;
        this.level = serializedData.level;
        this.stage = serializedData.stage;
        this.scoreTier = serializedData.scoreTier;
        this.elementalPower = serializedData.elementalPower;
        this.notes = Serializer.ListFromArray<NoteData>(serializedData.notes);
        this.stageItemDrops = Serializer.ListFromArray<StageItemDrop>(serializedData.stageItemDrops);
    }
}

[Serializable]
public class SerializedStageData
{
    public string description;
    public int level;
    public int stage;
    public int[] scoreTier;
    public ElementalPower elementalPower;
    public NoteData[] notes;
    public StageItemDrop[] stageItemDrops;

    public SerializedStageData() { }

    public SerializedStageData(StageData stageData)
    {
        this.description = stageData.description;
        this.level = stageData.level;
        this.stage = stageData.stage;
        this.scoreTier = stageData.scoreTier;
        this.elementalPower = stageData.elementalPower;
        this.notes = Serializer.ListToArray(stageData.notes);
        this.stageItemDrops = Serializer.ListToArray(stageData.stageItemDrops);
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
public class StageItemDrop
{
    public ItemData item;
    public int dropChance;

    public StageItemDrop(ItemData item, int dropChance)
    {
        this.item = item;
        this.dropChance = dropChance;
    }
}
