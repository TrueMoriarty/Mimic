using DAL.EfClasses;

namespace DAL;

public class UserDTO
{
    public int UserId { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }

    public User ToUser()
    {
        return new User()
        {
            Login = Login,
            Password = Password,
            Name = Name
        };
    }
}
