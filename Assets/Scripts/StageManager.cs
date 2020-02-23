public class StageManager
{
    private StageData stageData;
    private float score;
    private bool isCountdownComplete = false;
    private bool isStageOver = false;

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
    }

    public bool IsStageOver()
    {
        return this.isStageOver;
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

}
