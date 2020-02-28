using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    private StageData stageData;
    private MetaData metaData;
    private float score;
    private bool isCountdownComplete = false;
    private bool isStageOver = false;
    private List<ItemData> droppedItems = new List<ItemData>();

    public StageData GetStageData()
    {
        if (this.stageData == null)
        {
            // No stage found, create new stage data
            StageData newStageData = new StageData();
            MetaData newMetaData = new MetaData();
            this.stageData = newStageData;
            this.metaData = newMetaData;
        }
        return this.stageData;
    }

    public MetaData GetMetaData()
    {
        if (this.metaData == null)
        {
            // No meta data found, create new meta data
            StageData newStageData = new StageData();
            MetaData newMetaData = new MetaData();
            this.stageData = newStageData;
            this.metaData = newMetaData;
        }
        return this.metaData;
    }

    public void SetStageData(StageData stageData)
    {
        this.stageData = stageData;
    }

    public void SetMetaData(MetaData metaData)
    {
        this.metaData = metaData;
    }

    public void ClearStage()
    {
        this.stageData = null;
        this.metaData = null;
        this.score = 0f;
        this.isCountdownComplete = false;
        this.isStageOver = false;
        this.droppedItems = new List<ItemData>();
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

    // Called by End Boundary
    public void SetStageOver()
    {
        this.isStageOver = true;
        // Drop items
        this.DropItems();
        // Set clear the stage when the score is at least equal to the lowest score tier value
        this.metaData.isComplete = this.score >= this.stageData.scoreTier[0];
        // Set new high score when the score is greater than the current high score
        this.metaData.highScore = Mathf.Max(this.score, this.metaData.highScore);
        this.metaData.DecrementDailyAttempt();

        // Save updated user data
        UserManager userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        UserData userData = userManager.GetUserData();
        Persistence.SaveUserData(userData);
    }

    public bool IsStageOver()
    {
        return this.isStageOver;
    }

    public List<ItemData> GetItemDrops()
    {
        return this.droppedItems;
    }

    // After the stage is over, drop and add items to list
    private void DropItems()
    {
        // Go through each ItemData in ItemDrops
        foreach (ItemDrop itemDrop in this.stageData.itemDrops)
        {
            // Randomly generate a number
            float dropNumber = Random.Range(0f, 1f);

            // Add ItemData if dropNumber is less than or equal to the drop chance
            if (dropNumber <= itemDrop.dropChance)
            {
                this.droppedItems.Add(itemDrop.item);
            }
        }

    }

}
