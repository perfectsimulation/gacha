using UnityEngine.SceneManagement;

public class StageManager
{
    private StageData stageData;
    private int score;

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
    public void SetStageData(string json)
    {
        StageData data = this.DeserializeStageData(json);
        this.stageData = data;
        // Switch to the Stage scene now that stage data has been loaded
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClearStage()
    {
        this.stageData = null;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public int GetScore()
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
