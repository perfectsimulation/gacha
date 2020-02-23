using System;

[Serializable]
public class CardData
{
    // TODO: implement level and experience
    // TODO: design interesting properties - placeholder values for now
    public string name;
    public string imageUrl;
    public int level;
    public int rarity;
    public int rank;
    public int waterAffinity;
    public int airAffinity;
    public int hotAffinity;
    public int coldAffinity;
    public int hostAffinity;
    public int detectability;
    public int severity;
    public LevelRequirement[] levelReqs;
    public RankRequirement[] rankReqs;

    public CardData()
    {
        this.name = "card";
        this.imageUrl = "";
        this.level = 1;
        this.rarity = 0;
        this.rank = 0;
        this.waterAffinity = 0;
        this.airAffinity = 0;
        this.hotAffinity = 0;
        this.coldAffinity = 0;
        this.hostAffinity = 0;
        this.detectability = 0;
        this.severity = 0;
    }

    public CardData(string name, string imageUrl, int level, int rarity, int rank, int water, int air, int hot, int cold, int host, int detectability, int severity, LevelRequirement[] levelReqs, RankRequirement[] rankReqs)
    {
        this.name = name;
        this.imageUrl = imageUrl;
        this.level = level;
        this.rarity = rarity;
        this.rank = rank;
        this.waterAffinity = water;
        this.airAffinity = air;
        this.hotAffinity = hot;
        this.coldAffinity = cold;
        this.hostAffinity = host;
        this.detectability = detectability;
        this.severity = severity;
        this.levelReqs = levelReqs;
        this.rankReqs = rankReqs;
    }
}

[Serializable]
public class LevelRequirement
{
    public int level;
    public float experience;

    public LevelRequirement()
    {
        this.level = 2;
        this.experience = 100f;
    }

    public LevelRequirement(int level, float experience)
    {
        this.level = level;
        this.experience = experience;
    }
}

[Serializable]
public class RankRequirement
{
    public int rank;
    public int level;
    public int[] itemIds;
    public int[] itemQuantities;

    public RankRequirement()
    {
        this.rank = 1;
        this.level = 1;
        this.itemIds = new int[] { 0, 1, 2 };
        this.itemQuantities = new int[] { 3, 5, 4 };
    }

    public RankRequirement(int rank, int level, int[] itemIds, int[] itemQuantities)
    {
        this.rank = rank;
        this.level = level;
        this.itemIds = itemIds;
        this.itemQuantities = itemQuantities;
    }
}

public class LevelRequirements
{
    // TODO deserialize LevelRequirement array into a map
}

public class RankRequirements
{
    // TODO deserialize RankRequirement array into a map
}
