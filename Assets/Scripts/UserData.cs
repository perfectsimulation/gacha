using System;
using System.Collections.Generic;

public class UserData
{
    public string username;
    public int playerLevel = 0;
    public Progress progress = new Progress();
    public List<CardData> cards;
    public List<ItemData> items = new List<ItemData>();

    private CardData selectedCard;

    public void Init()
    {
        // TODO
    }

    public static UserData CreateNewInstance()
    {
        UserData userData = DataInitializer.CreateUser();
        return userData;
    }

    public static UserData Deserialize(string serializedData)
    {
        UserData userData = CreateNewInstance();
        return userData;
    }

    public override string ToString()
    {
        // Prepare properties for serialization
        SerializedUserData serialized = new SerializedUserData(this);
        string s = JsonHelper.ToJson<SerializedUserData>(serialized);
        return s;
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
        Node currentNode = this.FindFirstIncompleteNode();
        return currentNode.stageData.level;
    }

    public int GetCurrentStage()
    {
        Node currentNode = this.FindFirstIncompleteNode();
        return currentNode.stageData.stage;
    }

    public Node GetNodeById(int level, int stage)
    {
        string id = string.Format("{0}-{1}", level, stage);
        foreach (Node node in this.progress.nodes)
        {
            if (node.id == id)
                return node;
        }
        return new Node();
    }

    // Return all Nodes with the specified level
    public List<Node> GetStagesOfLevel(int level)
    {
        List<Node> stages = new List<Node>();
        foreach (Node n in this.progress.nodes)
        {
            if (n.stageData.level == level)
            {
                stages.Add(n);
            }
        }
        return stages;
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

    private Node FindFirstIncompleteNode()
    {
        // Go through each Node to find the first one with isComplete = false
        foreach (Node n in this.progress.nodes)
        {
            if (n.metaData.isComplete == false)
            {
                return n;
            }
        }
        return this.progress.nodes[0];
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
public class Progress
{
    public List<Node> nodes = new List<Node>();

    public Progress() { }

    public void AddNode(Node node)
    {
        this.nodes.Add(node);
    }
}

[Serializable]
public class Node
{
    public string id;
    public StageData stageData;
    public MetaData metaData;

    public Node() { }

    public Node(StageData stageData, MetaData metaData)
    {
        this.id = string.Format("{0}-{1}", stageData.level, stageData.stage);
        this.stageData = stageData;
        this.metaData = metaData;
    }
}

[Serializable]
public class SerializedUserData
{
    public string username;
    public int playerLevel = 0;
    public Progress progress = new Progress();
    public CardData[] cards;
    public ItemData[] items = new ItemData[] { };

    public SerializedUserData(UserData userData)
    {
        this.username = userData.username;
        this.playerLevel = userData.playerLevel;
        this.progress = userData.progress;

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
}
