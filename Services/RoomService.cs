using DAL;
using DAL.EfClasses;
using System.Net.Http;

namespace Services;

public interface IRoomService
{
    Room CreateRoom(int masterId, string name);
    Room[] GetRoomsByMasterId(int masterId);
    Room? GetRoomById(int Id);
    void JoinRoom(Room room, Character character);
}

internal class RoomService(IUnitOfWork uow) : IRoomService
{
    public Room CreateRoom(int masterId, string name)
    {
        Room room = new Room
        {
            Name = name,
            MasterId = masterId
        };

        uow.RoomRepository.Insert(room);
        uow.Save();

        return room;
    }

    public Room[] GetRoomsByMasterId(int masterId)
    {
        return uow.RoomRepository.Get(room => room.MasterId == masterId, includeProperties: "Characters,Master").ToArray();
    }

    public Room? GetRoomById(int Id)
    {
        return uow.RoomRepository.Get(room => room.RoomId == Id, includeProperties: "Characters").FirstOrDefault();
    }

    public void JoinRoom(Room room, Character character)
    {
        room.Characters.Add(character);

        uow.RoomRepository.Update(room);
        uow.Save();
    }
}