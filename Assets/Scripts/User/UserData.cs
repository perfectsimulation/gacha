using System;
using System.Collections.Generic;

public class UserData
{
    public string username;
    public int playerLevel = 0;
    public float experience = 0f;
    public List<MetaData> metaData = new List<MetaData>();
    public List<CardData> cards = new List<CardData>();
    public List<ItemData> items = new List<ItemData>();

    private CardData selectedCard;

    public UserData() { }

    public UserData(SerializedUserData serializedUserData)
    {
        this.username = serializedUserData.username;
        this.playerLevel = serializedUserData.playerLevel;
        this.experience = serializedUserData.experience;

        List<SerializedMetaData> serializedMetas = Serializer.ListFromArray<SerializedMetaData>(serializedUserData.metaData);
        foreach (SerializedMetaData serializedMeta in serializedMetas)
        {
            MetaData meta = new MetaData(serializedMeta);
            this.metaData.Add(meta);
        }

        this.cards = Serializer.ListFromArray<CardData>(serializedUserData.cards);
        this.items = Serializer.ListFromArray<ItemData>(serializedUserData.items);
    }

    public void Init()
    {
        // TODO
    }

    public static UserData CreateNewInstance()
    {
        UserData userData = DataInitializer.CreateUser();
        return userData;
    }

    public override string ToString()
    {
        // Prepare properties for serialization
        SerializedUserData serialized = new SerializedUserData(this);
        return StringUtility.ToJson<SerializedUserData>(serialized);
    }

    public void SetUsername(string username)
    {
        this.username = username;
    }

    public void SetCardData(List<CardData> cardData)
    {
        this.cards = cardData;
    }

    public List<CardData> GetUserCards()
    {
        return this.cards;
    }

    public void SetSelectedCardData(CardData cardData)
    {
        this.selectedCard = cardData;
    }

    public CardData GetSelectedCardData()
    {
        return this.selectedCard;
        // TODO error handle when selectedCard is null
    }

    public int GetCurrentLevel()
    {
        // Go through each meta to find the first one with isComplete = false
        foreach (MetaData meta in this.metaData)
        {
            if (meta.isComplete == false)
            {
                return meta.level;
            }
        }

        return 1;
    }

    public int GetCurrentStage()
    {
        // Go through each meta to find the first one with isComplete = false
        foreach (MetaData meta in this.metaData)
        {
            if (meta.isComplete == false)
            {
                return meta.stage;
            }
        }

        return 1;
    }

    public MetaData GetMetaDataById(int level, int stage)
    {
        string id = DataInitializer.FormatStageId(level, stage);
        foreach (MetaData data in this.metaData)
        {
            string metaId = DataInitializer.FormatStageId(data.level, data.stage);
            if (metaId == id)
                return data;
        }

        return new MetaData();
    }

    public void IncrementExperience()
    {
        this.IncrementUserExperience(DataInitializer.GetUserExperienceDeltaOnStageComplete());
        this.IncrementCardExperience(DataInitializer.GetCardExperienceDeltaOnStageComplete());
    }

    public void AddItemsToInventory(List<ItemData> items)
    {
        // Add each new item to inventory
        foreach (ItemData item in items)
        {
            if (this.HasAtLeastOneOfItem(item))
            {
                // Increment quantity of this item in the user's inventory
                this.IncrementItemQuantity(item);
            }
            else
            {
                // User does not have any of this item yet, so add it to user's inventory
                this.items.Add(item);
            }
        }
    }

    private void IncrementUserExperience(float delta)
    {
        this.experience += delta;
        this.CheckForLevelUp();
    }

    private void IncrementCardExperience(float delta)
    {
        // Increment experience on selected card if one exists
        if (this.selectedCard != null)
        {
            foreach (CardData card in this.cards)
            {
                if (this.selectedCard.name == card.name)
                {
                    card.IncrementExperience(delta);
                }
            }
        }
    }

    private void CheckForLevelUp()
    {
        int levelForExperience = DataInitializer.GetPlayerLevelByExperience(this.experience);
        if (this.playerLevel < levelForExperience)
        {
            // Level up time! TODO pretty celebration
            this.playerLevel = levelForExperience;
        }
    }

    private string FindFirstIncompleteStageId()
    {
        // Go through each MetaData to find the first one with isComplete = false
        foreach (MetaData meta in this.metaData)
        {
            if (meta.isComplete == false)
            {
                return DataInitializer.FormatStageId(meta.level, meta.stage);
            }
        }

        return string.Empty;
    }

    private bool HasAtLeastOneOfItem(ItemData itemData)
    {
        foreach (ItemData item in this.items)
        {
            if (item.id == itemData.id)
            {
                return true;
            }
        }

        return false;
    }

    private void IncrementItemQuantity(ItemData itemData)
    {
        foreach (ItemData item in this.items)
        {
            if (item.id == itemData.id)
            {
                item.IncreaseQuantity(itemData.quantity);
                return;
            }
        }

    }

}

[Serializable]
public class SerializedUserData
{
    public string username;
    public int playerLevel = 0;
    public float experience = 0f;
    public SerializedMetaData[] metaData = new SerializedMetaData[] { };
    public CardData[] cards;
    public ItemData[] items = new ItemData[] { };

    public SerializedUserData() { }

    public SerializedUserData(UserData userData)
    {
        this.username = userData.username;
        this.playerLevel = userData.playerLevel;
        this.experience = userData.experience;

        // Get meta data
        int metaCount = userData.metaData.Count;
        this.metaData = new SerializedMetaData[metaCount];
        int metaIndex = 0;
        foreach (MetaData meta in userData.metaData)
        {
            this.metaData[metaIndex] = new SerializedMetaData(meta);
            metaIndex++;
        }

        // Get card array from card list
        int cardCount = userData.cards.Count;
        CardData[] cards = new CardData[cardCount];
        int cardIndex = 0;
        foreach (CardData card in userData.cards)
        {
            cards[cardIndex] = card;
            cardIndex++;
        }

        this.cards = cards;

        // Get item array from item list
        int itemCount = userData.items.Count;
        ItemData[] items = new ItemData[itemCount];
        int itemIndex = 0;
        foreach (ItemData item in userData.items)
        {
            items[itemIndex] = item;
            itemIndex++;
        }

        this.items = items;
    }

    public SerializedUserData(string json)
    {
        SerializedUserData data = StringUtility.FromJson<SerializedUserData>(json);
        this.username = data.username;
        this.playerLevel = data.playerLevel;
        this.experience = data.experience;
        this.metaData = data.metaData;
        this.cards = data.cards;
        this.items = data.items;
    }
}
