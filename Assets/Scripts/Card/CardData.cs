using System;
using System.Collections.Generic;

[Serializable]
public class CardData
{
    // TODO: implement level and experience
    // TODO: design interesting properties - placeholder values for now
    public string name;
    public float experience;
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
        this.experience = 0f;
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

    public CardData(string name, float experience, int rank, int rarity, int water, int air, int hot, int cold, int host, int detectability, int severity, LevelRequirement[] levelReqs, RankRequirement[] rankReqs)
    {
        this.name = name;
        this.experience = experience;
        this.levelReqs = levelReqs;
        this.rankReqs = rankReqs;
        this.level = this.GetLevelFromExperience(experience);
        this.rank = this.GetRankFromLevel(this.level);
        this.rarity = rarity;
        this.waterAffinity = water;
        this.airAffinity = air;
        this.hotAffinity = hot;
        this.coldAffinity = cold;
        this.hostAffinity = host;
        this.detectability = detectability;
        this.severity = severity;
    }

    public void IncrementExperience(float delta)
    {
        this.experience += delta;
        this.CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        int levelForExperience = this.GetLevelFromExperience(this.experience);
        if (this.level < levelForExperience)
        {
            // Level up time! TODO celebrate good times
            this.level = levelForExperience;
            this.CheckForRankUp();
        }
    }

    private void CheckForRankUp()
    {
        int rankForLevel = this.GetRankFromLevel(this.level);
        if (this.rank < rankForLevel)
        {
            // Rank up time! TODO come on!
            this.rank = rankForLevel;
        }
    }

    private int GetLevelFromExperience(float experience)
    {
        // Go through level requirements starting from the highest requirement
        for (int i = this.levelReqs.Length - 1; i >= 0; i--)
        {
            if (experience > this.levelReqs[i].experience)
            {
                return this.levelReqs[i].level;
            }
        }

        return 1;
    }

    private int GetRankFromLevel(int level)
    {
        // Go through rank requirements starting from the highest requirement
        for (int i = this.rankReqs.Length - 1; i >= 0; i--)
        {
            if (level > this.rankReqs[i].level)
            {
                return this.rankReqs[i].level;
            }
        }

        return 1;
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
    public List<ItemData> items;

    public RankRequirement()
    {
        this.rank = 1;
        this.level = 1;
        this.items = new List<ItemData>();
    }

    public RankRequirement(int rank, int level, List<ItemData> items)
    {
        this.rank = rank;
        this.level = level;
        this.items = items;
    }
}
