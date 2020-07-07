using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    // Stage data
    public StageData StageData { get; set; }

    // Meta data
    public MetaData MetaData { get; set; }

    // Stage results including final score and dropped items
    public StageResult StageResult { get; set; }

    // Raw score before any manipulation
    public int RawScore { get; set; }

    // True when the camera begins moving across the stanza
    public bool IsCountdownComplete { get; set; } = false;

    // True when the TouchBoundary and EndBoundary have collided
    public bool IsStageOver { get; set; } = false;

    // Called by EndBoundary on stage completion
    public void EndStage()
    {
        // The stage ends when the EndBoundary collides with the TouchBoundary
        this.IsStageOver = true;

        // Cache the user manager
        UserManager userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        UserData userData = userManager.GetUserData();

        // Cache the elementalPower of the selected card of the user
        ElementalPower userElementalPower = userData.SelectedCard.elementalPower;

        // Cache the elementalPower of the stage
        ElementalPower stageElementalPower = this.StageData.elementalPower;

        // Cache the stageItemDrops of the stage
        List<StageItemDrop> stageItemDrops = this.StageData.stageItemDrops;

        // Calculate stage results using user and stage properties
        this.StageResult = new StageResult(
            this.RawScore,
            userElementalPower,
            stageElementalPower,
            stageItemDrops);

        // If score meets the minimum of the stage score tier, clear the stage
        this.MetaData.isComplete = this.StageResult.FinalScore >= this.StageData.scoreTier[0];

        // Set high score when the score is greater than the current high score
        this.MetaData.highScore = Mathf.Max(
            this.StageResult.FinalScore,
            this.MetaData.highScore);

        // Decrement daily attempts for this stage
        this.MetaData.DecrementDailyAttempt();

        // Save updated user data
        Persistence.SaveUserData(userData);
    }

    // Reset all cached properties
    public void ClearCache()
    {
        this.StageData = null;
        this.MetaData = null;
        this.StageResult = new StageResult();
        this.RawScore = 0;
        this.IsCountdownComplete = false;
        this.IsStageOver = false;
    }

}
