public class UserManager
{
    private UserData userData;

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

    // Called after user data has been loaded
    public void SetUserData(UserData userData)
    {
        this.userData = userData;
    }

}
