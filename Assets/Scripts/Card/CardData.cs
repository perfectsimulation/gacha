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
    public ElementalPower elementalPower;
    public LevelRequirement[] levelReqs;
    public RankRequirement[] rankReqs;

    public CardData()
    {
        this.name = "card";
        this.experience = 0f;
        this.level = 1;
        this.rarity = 0;
        this.rank = 0;
        this.elementalPower = new ElementalPower();
    }

    public CardData(string name, float experience, int rarity, ElementalPower elementalPower, LevelRequirement[] levelReqs, RankRequirement[] rankReqs)
    {
        this.name = name;
        this.experience = experience;
        this.levelReqs = levelReqs;
        this.rankReqs = rankReqs;
        this.level = this.GetLevelFromExperience(experience);
        this.rank = this.GetRankFromLevel(this.level);
        this.rarity = rarity;
        this.elementalPower = elementalPower;
    }

    // Increments card experience and checks if card gained enough experience to level up
    public void IncrementExperience(float delta)
    {
        this.experience += delta;
        this.CheckForLevelUp();
    }

    // Checks for level up by comparing current experience against required experience
    private void CheckForLevelUp()
    {
        // Get the theoretical card level based on current experience
        int levelForExperience = this.GetLevelFromExperience(this.experience);
        // Compare current card level with theoretical card level
        if (this.level < levelForExperience)
        {
            // Level up time! TODO celebrate good times
            this.LevelUp();
            // Check if card has gained enough levels to rank up
            this.CheckForRankUp();
        }
    }

    // Increments current level by one
    private void LevelUp()
    {
        this.level++;
    }

    // Checks for rank up by comparing current level against required level
    private void CheckForRankUp()
    {
        // Get the theoretical card rank based on current level
        int rankForLevel = this.GetRankFromLevel(this.level);
        if (this.rank < rankForLevel)
        {
            // Rank up time! TODO check item requirements
            this.RankUp();
        }
    }

    // Increments current rank by one
    private void RankUp()
    {
        this.rank++;
    }

    // Returns theoretical level by checking level requirements
    private int GetLevelFromExperience(float experience)
    {
        // Go through level requirements starting from the highest requirement
        for (int i = this.levelReqs.Length - 1; i >= 0; i--)
        {
            // If experience exceeds the required experience of this level
            if (experience > this.levelReqs[i].experience)
            {
                // This is the first level we exceeded, and we started from the top
                return this.levelReqs[i].level;
            }
        }

        // Should never execute
        return 1;
    }

    // Returns theoretical rank by checking rank requirements
    private int GetRankFromLevel(int level)
    {
        // Go through rank requirements starting from the highest requirement
        for (int i = this.rankReqs.Length - 1; i >= 0; i--)
        {
            // If level exceeds the required level of this rank
            if (level > this.rankReqs[i].level)
            {
                // This is the first rank we exceeded, and we started from the top
                return this.rankReqs[i].rank;
            }
        }

        // Should never execute
        return 1;
    }
}

public class ElementalPower
{
    public int water;
    public int air;
    public int fire;
    public int earth;

    public ElementalPower()
    {
        this.water = 0;
        this.air = 0;
        this.fire = 0;
        this.earth = 0;
    }

    public ElementalPower(int water, int air, int fire, int earth)
    {
        this.water = water;
        this.air = air;
        this.fire = fire;
        this.earth = earth;
    }
}

[Serializable]
public class LevelRequirement
{
    public int level;
    public float experience;

    public LevelRequirement()
    {
        this.level = 0;
        this.experience = 0f;
    }

    // Experience required for this level
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
        this.rank = 0;
        this.level = 0;
        this.items = new List<ItemData>();
    }

    // Level and items required for this rank
    public RankRequirement(int rank, int level, List<ItemData> items)
    {
        this.rank = rank;
        this.level = level;
        this.items = items;
    }
}
