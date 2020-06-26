﻿using System.Collections.Generic;

public static class DataInitializer
{
    /* USER */
    private static readonly float userExperienceDeltaOnStageComplete = 100f;
    private static readonly float cardExperienceDeltaOnStageComplete = 100f;

    private static Dictionary<int, int> playerLevelTiers = new Dictionary<int, int>() { };

    /* STAGE */
    private static readonly int[] scoreTier0 = new int[] { 100, 200, 400 };
    private static readonly int[] scoreTier1 = new int[] { 320, 888, 1000 };
    private static readonly int[] scoreTier2 = new int[] { 600, 1000, 3230 };

    private static readonly List<NoteData> notes0 = new List<NoteData> {
        new NoteData(0, 0, 8),
        new NoteData(2, 8, 5),
        new NoteData(4, 13, 3),
        new NoteData(2, 16, 8),
        new NoteData(0, 24, 5),
        new NoteData(-2, 29, 3),
        new NoteData(0, 34, 8),
        new NoteData(2, 42, 5),
        new NoteData(4, 48, 3),
        new NoteData(2, 52, 8),
        new NoteData(0, 60, 5),
        new NoteData(-2, 65, 3),
    };
    private static readonly List<NoteData> notes1 = new List<NoteData> {
        new NoteData(0, 0, 4),
        new NoteData(-2, 4, 4),
        new NoteData(-4, 8, 2),
        new NoteData(0, 10, 4),
        new NoteData(4, 14, 2),
        new NoteData(2, 16, 2),
        new NoteData(0, 18, 4),
        new NoteData(-2, 22, 4),
        new NoteData(-4, 26, 2),
        new NoteData(0, 28, 4),
        new NoteData(4, 31, 2),
        new NoteData(2, 32, 2),
    };

    private static readonly CardBonus cardBonus0 = new CardBonus(0.1f, 0.2f, 0.3f, 0.4f);
    private static readonly CardBonus cardBonus1 = new CardBonus(0.2f, 0.6f, 0.3f, 0.1f);
    private static readonly CardBonus cardBonus2 = new CardBonus(0.8f, 0.2f, 0.2f, 0.1f);

    private static readonly ItemData item0 = new ItemData(0, "vitamins", 1);
    private static readonly ItemData item1 = new ItemData(1, "minerals", 1);
    private static readonly ItemData item2 = new ItemData(2, "carbohydrates", 1);
    private static readonly ItemData item3 = new ItemData(3, "protein", 1);
    private static readonly ItemData item4 = new ItemData(4, "water", 1);
    private static readonly ItemData item5 = new ItemData(5, "air", 1);
    private static readonly ItemData item6 = new ItemData(6, "surface", 1);
    private static readonly ItemData item7 = new ItemData(7, "acid", 1);
    private static readonly ItemData item8 = new ItemData(8, "base", 1);
    private static readonly ItemData item9 = new ItemData(9, "salt", 1);
    private static readonly ItemData item10 = new ItemData(10, "cough", 1);
    private static readonly ItemData item11 = new ItemData(11, "sneeze", 1);
    private static readonly ItemData item12 = new ItemData(12, "kiss", 1);
    private static readonly ItemData item13 = new ItemData(13, "hug", 1);
    private static readonly ItemData item14 = new ItemData(14, "blood", 1);
    private static readonly ItemData item15 = new ItemData(15, "rash", 1);
    private static readonly ItemData item16 = new ItemData(16, "vomit", 1);

    private static readonly LevelRequirement[] card0LevelReqs = new LevelRequirement[] { new LevelRequirement(), new LevelRequirement(3, 300f), new LevelRequirement(4, 400f), new LevelRequirement(5, 500f), new LevelRequirement(6, 600f), new LevelRequirement(7, 700f), new LevelRequirement(8, 800f), new LevelRequirement(9, 900f), new LevelRequirement(10, 1000f) };
    private static readonly LevelRequirement[] card1LevelReqs = new LevelRequirement[] { new LevelRequirement(), new LevelRequirement(3, 300f), new LevelRequirement(4, 400f), new LevelRequirement(5, 500f), new LevelRequirement(6, 600f), new LevelRequirement(7, 700f), new LevelRequirement(8, 800f), new LevelRequirement(9, 900f), new LevelRequirement(10, 1000f), new LevelRequirement(11, 1100f), new LevelRequirement(12, 1200f), new LevelRequirement(13, 1300f), new LevelRequirement(14, 1400f), new LevelRequirement(15, 1500f), new LevelRequirement(16, 1600f), new LevelRequirement(17, 1700f), new LevelRequirement(18, 1800f), new LevelRequirement(19, 1900f), new LevelRequirement(20, 2000f) };
    private static readonly RankRequirement card0rankReq0 = new RankRequirement(1, 3, new List<ItemData> { item0, item1, item3 });
    private static readonly RankRequirement card1rankReq0 = new RankRequirement(1, 3, new List<ItemData> { item4, item5, item8 });
    private static readonly RankRequirement card1rankReq1 = new RankRequirement(2, 7, new List<ItemData> { item11, item13, item14 });
    private static readonly CardData card0 = new CardData("big red", 0f, 0, 0, 29, 1, 29, 1, 20, 20, 20, card0LevelReqs, new RankRequirement[] { card0rankReq0 });
    private static readonly CardData card1 = new CardData("jungle boi", 0f, 1, 0, 10, 20, 30, 40, 25, 4, 0, card1LevelReqs, new RankRequirement[] { card1rankReq0, card1rankReq1 });

    private static readonly StageData stage0_0 = new StageData("level 0 - stage 0", 0, 0, scoreTier0, cardBonus0, notes0, GetItemDropsForStage(0, 0));
    private static readonly StageData stage0_1 = new StageData("level 0 - stage 1", 0, 1, scoreTier0, cardBonus2, notes1, GetItemDropsForStage(0, 1));
    private static readonly StageData stage0_2 = new StageData("level 0 - stage 2", 0, 2, scoreTier1, cardBonus1, notes1, GetItemDropsForStage(0, 2));
    private static readonly StageData stage0_3 = new StageData("level 0 - stage 3", 0, 3, scoreTier1, cardBonus1, notes0, GetItemDropsForStage(0, 3));
    private static readonly StageData stage0_4 = new StageData("level 0 - stage 4", 0, 4, scoreTier2, cardBonus0, notes1, GetItemDropsForStage(0, 4));
    private static readonly StageData stage0_5 = new StageData("level 0 - stage 5", 0, 5, scoreTier2, cardBonus2, notes1, GetItemDropsForStage(0, 5));

    private static readonly StageData stage1_0 = new StageData("level 1 - stage 0", 1, 0, scoreTier2, cardBonus1, notes0, GetItemDropsForStage(1, 0));
    private static readonly StageData stage1_1 = new StageData("level 1 - stage 1", 1, 1, scoreTier2, cardBonus1, notes0, GetItemDropsForStage(1, 1));
    private static readonly StageData stage1_2 = new StageData("level 1 - stage 2", 1, 2, scoreTier1, cardBonus2, notes0, GetItemDropsForStage(1, 2));
    private static readonly StageData stage1_3 = new StageData("level 1 - stage 3", 1, 3, scoreTier1, cardBonus2, notes0, GetItemDropsForStage(1, 3));
    private static readonly StageData stage1_4 = new StageData("level 1 - stage 4", 1, 4, scoreTier0, cardBonus0, notes1, GetItemDropsForStage(1, 4));

    public static UserData CreateUser()
    {
        UserData user = new UserData();
        user.SetUsername("satan");
        user.experience = 0f;
        user.playerLevel = GetPlayerLevelByExperience(0f);
        user.metaData = CreateMetaDataList();
        user.cards = new List<CardData> { card0, card1 };
        user.items = new List<ItemData>();
        return user;
    }

    public static StageData GetStageData(int level, int stage)
    {
        switch (level)
        {
            case 0:
                switch (stage)
                {
                    case 0:
                        return stage0_0;
                    case 1:
                        return stage0_1;
                    case 2:
                        return stage0_2;
                    case 3:
                        return stage0_3;
                    case 4:
                        return stage0_4;
                    case 5:
                        return stage0_5;
                    default:
                        return stage0_0;
                }
            case 1:
                switch (stage)
                {
                    case 0:
                        return stage1_0;
                    case 1:
                        return stage1_1;
                    case 2:
                        return stage1_2;
                    case 3:
                        return stage1_3;
                    case 4:
                        return stage1_4;
                    default:
                        return stage1_0;
                }
            default:
                return stage0_0;
        }

    }

    public static List<StageData> GetStagesOfLevel(int level)
    {
        switch (level)
        {
            case 0:
                return new List<StageData>() { stage0_0, stage0_1, stage0_2, stage0_3, stage0_4, stage0_5 };
            case 1:
                return new List<StageData>() { stage1_0, stage1_1, stage1_2, stage1_3, stage1_4 };
            default:
                return new List<StageData>() { stage0_0, stage0_1, stage0_2, stage0_3, stage0_4, stage0_5 };
        }
    }

    public static string FormatStageId(int level, int stage)
    {
        return string.Format("{0}-{1}", level, stage);
    }

    public static void InitializeCardLibrary()
    {
        // Create all new CardData
        // Save it to local device
    }

    public static float GetUserExperienceDeltaOnStageComplete()
    {
        return userExperienceDeltaOnStageComplete;
    }

    public static float GetCardExperienceDeltaOnStageComplete()
    {
        return cardExperienceDeltaOnStageComplete;
    }

    public static int GetPlayerLevelByExperience(float exp)
    {
        int level = 0;
        foreach (KeyValuePair<int, int> tier in playerLevelTiers)
        {
            if (exp >= tier.Value)
            {
                level = tier.Key;
            }
        }

        return level;
    }

    private static List<StageData> CreateStageDataList()
    {
        List<StageData> stageData = new List<StageData>();
        for (int level = 0; level < 2; level++)
        {
            for (int stage = 0; stage < 4; stage++)
            {
                StageData data = GetStageData(level, stage);
                stageData.Add(data);
            }
        }
        return stageData;
    }

    private static List<MetaData> CreateMetaDataList()
    {
        List<MetaData> metaData = new List<MetaData>();
        for (int level = 0; level < 2; level++)
        {
            for (int stage = 0; stage < 4; stage++)
            {
                MetaData data = CreateMetaData(level, stage);
                metaData.Add(data);
            }
        }
        return metaData;
    }

    private static MetaData CreateMetaData(int level, int stage)
    {
        List<string> prerequisiteStageIds = new List<string>();
        switch (level)
        {
            default:
                switch (stage)
                {
                    case 0:
                        break;
                    default:
                        prerequisiteStageIds.Add(FormatStageId(level, stage - 1));
                        break;
                }
                break;
        }

        return new MetaData(level, stage, prerequisiteStageIds);
    }

    private static List<ItemDrop> GetItemDropsForStage(int level, int stage)
    {
        // TODO level design
        List<ItemDrop> itemDrops = new List<ItemDrop>();
        switch (level)
        {
            case 0:
                switch (stage)
                {
                    case 0:
                        itemDrops.Add(new ItemDrop(item0, 0.1f));
                        itemDrops.Add(new ItemDrop(item3, 0.3f));
                        itemDrops.Add(new ItemDrop(item7, 0.7f));
                        break;
                    case 1:
                        itemDrops.Add(new ItemDrop(item2, 0.7f));
                        itemDrops.Add(new ItemDrop(item13, 0.8f));
                        itemDrops.Add(new ItemDrop(item4, 0.1f));
                        itemDrops.Add(new ItemDrop(item14, 0.1f));
                        break;
                    case 2:
                        itemDrops.Add(new ItemDrop(item5, 0.4f));
                        itemDrops.Add(new ItemDrop(item15, 0.3f));
                        itemDrops.Add(new ItemDrop(item16, 0.7f));
                        itemDrops.Add(new ItemDrop(item12, 0.1f));
                        break;
                    default:
                        itemDrops.Add(new ItemDrop(item1, 1f));
                        itemDrops.Add(new ItemDrop(item8, 0.5f));
                        itemDrops.Add(new ItemDrop(item6, 0.1f));
                        break;
                }
                break;
            default:
                itemDrops.Add(new ItemDrop(item9, 0.7f));
                itemDrops.Add(new ItemDrop(item10, 0.2f));
                itemDrops.Add(new ItemDrop(item11, 0.2f));
                break;
        }

        return itemDrops;
    }
}
