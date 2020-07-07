using System;
using System.Collections.Generic;

// Final results of stage: score and item drops
public class StageResult
{
    // Raw score is continuously updated by TouchBoundary until the stage ends
    public int rawScore;

    // ElementalPowers are compared to calculate the final score
    public ElementalPower userElementalPower;
    public ElementalPower stageElementalPower;

    // List of odds of dropping items on stage completion
    public List<StageItemDrop> stageItemDrops;

    // Weighted score based on properties of the user and stage, respectively
    public float FinalScore
    {
        get { return this.finalScore; }
        private set { this.finalScore = this.CalculateFinalScore(); }
    }

    // List of dropped items awarded by odds provided in stage properties
    public List<ItemData> DroppedItems
    {
        get { return this.droppedItems; }
        private set { this.droppedItems = this.DropItems(); }
    }

    // Internal score keeper
    private float finalScore;
    private List<ItemData> droppedItems;

    // Default no-arg constructor with zero'd initial values
    public StageResult()
    {
        this.rawScore = 0;
        this.userElementalPower = new ElementalPower();
        this.stageElementalPower = new ElementalPower();
        this.stageItemDrops = new List<StageItemDrop>();
        this.finalScore = 0f;
        this.droppedItems = new List<ItemData>();
    }

    // Created in TouchBoundary at stage completion
    public StageResult(
        int rawScore,
        ElementalPower userElementalPower,
        ElementalPower stageElementalPower,
        List<StageItemDrop> stageItemDrops)
    {
        this.rawScore = rawScore;
        this.userElementalPower = userElementalPower;
        this.stageElementalPower = stageElementalPower;
        this.stageItemDrops = stageItemDrops;
        this.finalScore = this.CalculateFinalScore();
        this.droppedItems = this.DropItems();
    }

    // Calculates score from raw score obtained in TouchBoundary and
    // elemental powers of the user and stage, respectively
    private float CalculateFinalScore()
    {
        // Final Score = Raw Score * (Attack - Damage)
        float attack = this.InflictAttack();
        float damage = this.SustainDamage();
        float performance = attack - damage;

        // Final score won't replace raw score if it's worse
        return Math.Max(performance * (float)this.rawScore, (float)this.rawScore);

    }

    // Subtracts B from A
    private float ComparePower(int A, int B)
    {
        return (float)A - (float)B;
    }

    // Calculates the attack inflicted upon the stage rival by the user
    private float InflictAttack()
    {
        // Attack stage fire with user water
        float userWaterAttack = this.stageElementalPower.fire - this.userElementalPower.water;

        // Attack stage water with user air
        float userAirAttack = this.stageElementalPower.water - this.userElementalPower.air;

        // Attack stage earth with user fire
        float userFireAttack = this.stageElementalPower.earth - this.userElementalPower.fire;

        // Attack stage air with user earth
        float userEarthAttack = this.stageElementalPower.air - this.userElementalPower.earth;

        // Attack = sum of elemental attack inflicted by user on stage
        float sumUserElementalAttack =
            userWaterAttack
            + userAirAttack
            + userFireAttack
            + userEarthAttack;

        return sumUserElementalAttack;
    }

    // Calculates the damage sustained by user from stage attack
    private float SustainDamage()
    {
        // Damage user water with stage air
        float userWaterDamage = this.userElementalPower.water - this.stageElementalPower.air;

        // Damage user air with stage earth
        float userAirDamage = this.userElementalPower.air - this.stageElementalPower.earth;

        // Damage user fire with stage water
        float userFireDamage = this.userElementalPower.fire - this.stageElementalPower.water;

        // Damage user earth with stage fire
        float userEarthDamage = this.userElementalPower.earth - this.stageElementalPower.fire;

        // Damage = sum of weighted elemental damage inflicted by stage on user
        float sumUserElementalDamage =
            userWaterDamage
            + userAirDamage
            + userFireDamage
            + userEarthDamage;

        return sumUserElementalDamage;
    }

    // Awards an item if the randomly generated number associated with it falls
    // within the window of chance defined in the stageItemDrops property
    private List<ItemData> DropItems()
    {
        // Cache a random number generator
        Random rng = new Random();

        // Create a fresh, empty list for dropped items
        List<ItemData> itemsToDrop = new List<ItemData>();

        // Go through each item drop taken from stageItemDrops
        foreach (StageItemDrop itemDrop in this.stageItemDrops)
        {
            // Randomly generate a number from 0 to 100
            float dropNumber = rng.Next(101);

            // If the random number is less than or equal to the drop chance,
            // fortune has smiled upon the user
            if (dropNumber <= itemDrop.dropChance)
            {
                // Add a new item to the list of dropped items
                itemsToDrop.Add(itemDrop.item);
            }
        }

        // Return the list of dropped item data
        return itemsToDrop;
    }

}
