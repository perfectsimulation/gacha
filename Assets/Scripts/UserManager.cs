public class UserManager
{
    public UserData userData;

    public UserData GetUserData()
    {
        if (this.userData == null)
        {
            // No user found, so create a new user
            UserData newUserData = UserData.CreateNewInstance();
            this.userData = newUserData;
        }
        return this.userData;
    }

    // Called after user data has been retrieved from database
    public void SetUserData(string json)
    {
        UserData data = this.DeserializeUserData(json);
        this.userData = data;
    }

    // Turn UserData into JSON
    public string SerializeUserData(UserData data)
    {
        UserData[] dataArray = new UserData[] { data };
        string json = JsonHelper.ToJson(dataArray);
        return json;
    }

    // Turn JSON into UserData
    public UserData DeserializeUserData(string json)
    {
        UserData[] data = JsonHelper.FromJson<UserData>(json);
        return data[0];
    }

}
