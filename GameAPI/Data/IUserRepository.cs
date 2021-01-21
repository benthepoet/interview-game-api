namespace GameAPI.Data
{
    public interface IUserRepository
    {
        User CreateUser();
        User GetUser(int id);
        void UpdateUser(User user);
    }
}
