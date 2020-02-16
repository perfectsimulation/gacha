public class StageManager
{
    private StageData stageData;
    private float score;

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
        string responseLevel = response[1];
        string responseStage = response[2];
        string responseScoreTier = response[3];
        string responseCardBonus = response[4];
        string responseNotes = response[5];

        int level = int.Parse(responseLevel);
        int stage = int.Parse(responseStage);
        int[] scoreTier = JsonHelper.FromJson<int>(responseScoreTier);
        CardBonus[] cardBonus = JsonHelper.FromJson<CardBonus>(responseCardBonus);
        NoteData[] notes = JsonHelper.FromJson<NoteData>(responseNotes);

        // Construct and set StageData from response
        StageData data = new StageData(level, stage, scoreTier, cardBonus[0], notes);
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
