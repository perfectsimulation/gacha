using System.Linq;
using UnityEngine;

public class StageManager
{
    private StageData stageData;
    private float score;
    private bool isCountdownComplete = false;
    private bool isStageOver = false;
    private ItemData[] droppedItems;

    public StageData GetStageData()
    {
        if (this.stageData == null)
        {
            // No user found, so create a new user
            StageData newStageData = new StageData();
            this.stageData = newStageData;
        }
        return this.stageData;
    }

    // Called after user data has been retrieved from database
    public void SetStageData(string[] response)
    {
        // Parse GET stage data response
        string responseDescription = response[1];
        string responseLevel = response[2];
        string responseStage = response[3];
        string responseScoreTier = response[4];
        string responseCardBonus = response[5];
        string responseNotes = response[6];
        string responseItemDrops = response[7];

        string description = responseDescription;
        int level = int.Parse(responseLevel);
        int stage = int.Parse(responseStage);
        int[] scoreTier = JsonHelper.FromJson<int>(responseScoreTier);
        CardBonus[] cardBonus = JsonHelper.FromJson<CardBonus>(responseCardBonus);
        NoteData[] notes = JsonHelper.FromJson<NoteData>(responseNotes);
        ItemDrop[] itemDrops = JsonHelper.FromJson<ItemDrop>(responseItemDrops);

        // Construct and set StageData from response
        StageData data = new StageData(description, level, stage, scoreTier, cardBonus[0], notes, itemDrops);
        this.stageData = data;
    }

    public void ClearStage()
    {
        this.stageData = null;
    }

    public void SetScore(float score)
    {
        this.score = score;
    }

    public float GetScore()
    {
        return this.score;
    }

    public void SetCountdownComplete()
    {
        this.isCountdownComplete = true;
    }

    public bool IsCountdownComplete()
    {
        return this.isCountdownComplete;
    }

    public void SetStageOver()
    {
        this.isStageOver = true;
        // Drop items
        this.DropItems();
    }

    public bool IsStageOver()
    {
        return this.isStageOver;
    }

    public ItemData[] GetItemDrops()
    {
        return this.droppedItems;
    }

    // Turn StageData into JSON
    public string SerializeStageData(StageData data)
    {
        StageData[] dataArray = new StageData[] { data };
        string json = JsonHelper.ToJson(dataArray);
        return json;
    }

    // Turn JSON into StageData
    public StageData DeserializeStageData(string json)
    {
        StageData[] data = JsonHelper.FromJson<StageData>(json);
        return data[0];
    }

    // Turn NoteData into JSON
    public string SerializeNoteData(NoteData[] data)
    {
        NoteData[] dataArray = data;
        string json = JsonHelper.ToJson(dataArray);
        return json;
    }

    // Turn JSON into NoteData
    public NoteData DeserializeNoteData(string json)
    {
        NoteData[] data = JsonHelper.FromJson<NoteData>(json);
        return data[0];
    }

    // After the stage is over, drop items
    private void DropItems()
    {
        ItemDrop[] itemDrops = this.stageData.itemDrops;
        bool[] wasItemDropped = new bool[this.stageData.itemDrops.Length];

        for (int i = 0; i < this.stageData.itemDrops.Length; i++)
        {
            // Generate a random number
            float drop = Random.Range(0f, 1f);

            // Drop the item if the random number is less than or equal to the drop rate
            if (drop <= this.stageData.itemDrops[i].dropChance)
            {
                // Drop the item
                wasItemDropped[i] = true;
            }
            else
            {
                // Do not drop the item
                wasItemDropped[i] = false;
            }
        }

        // Create this.droppedItems with length equal to number of true elements in wasItemDropped
        this.droppedItems = new ItemData[wasItemDropped.Where(i => i).Count()];

        // Go through wasItemDropped to add ItemData into this.droppedItems
        int index = 0;
        for (int i = 0; i < wasItemDropped.Length; i++)
        {
            if (wasItemDropped[i])
            {
                this.droppedItems[index] = this.stageData.itemDrops[i].item;
                // Keep track of index of the last item added
                index++;
            }
        }

    }
}
