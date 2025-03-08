using DAL.EfClasses;
using MimicWebApi.ViewModels.Characters;

namespace MimicWebApi.ViewModels;

public class RoomViewModel
{
    public int RoomId { get; set; }
    public string Name { get; set; }
    public UserInfoViewModel MasterInfo { get; set; }

    public CharacterBaseViewModel[] Characters { get; set; }

    public RoomViewModel(Room room)
    {
        RoomId = room.RoomId;
        Name = room.Name;
        if (room.Master is not null)
            MasterInfo = new UserInfoViewModel(room.Master);
        Characters = room.Characters?.Select(c => new CharacterBaseViewModel(c)).ToArray();
    }
}