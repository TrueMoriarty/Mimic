using DAL.EfClasses;

namespace MimicWebApi.Views;

public class UserInfoViewModel
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string IconUrl { get; set; }

    public UserInfoViewModel(User user)
    {
        UserId = user.UserId;
        UserName = user.Name!;
        IconUrl = user.Icon?.Url;
    }
}