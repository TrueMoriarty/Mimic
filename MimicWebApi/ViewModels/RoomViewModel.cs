using DAL.EfClasses;
using MimicWebApi.ViewModels.Characters;

namespace MimicWebApi.ViewModels;

public class RoomViewModel
{
    public string Name { get; set; }
    public UserInfoViewModel MasterInfo { get; set; }

    public CharacterBaseViewModel[] Characters { get; set; }

    public RoomViewModel(Room room)
    {
        Name = room.Name;
        MasterInfo = new UserInfoViewModel(room.Master);
        Characters = room.Characters?.Select(c => new CharacterBaseViewModel(c)).ToArray();
    }
}