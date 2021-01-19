namespace GameAPI.Data.Repositories
{
    public interface IUserRepository
    {
        UserRecord CreateUser();
        UserRecord GetUser(int id);
        void UpdateUser(UserRecord user);
    }
}
