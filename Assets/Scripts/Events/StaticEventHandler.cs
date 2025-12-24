using System;

public static class StaticEventHandler
{
    public static event Action<RoomChangedEventArgs> OnRoomChanged;
    public static event Action<Room> OnRoomEnemiesDefeated;

    public static void CallRoomChangedEvent(Room room)
    {
        OnRoomChanged?.Invoke(new RoomChangedEventArgs { room = room });
    }

    public static void CallRoomEnemiesDefeated(Room room)
    {
        OnRoomEnemiesDefeated?.Invoke(room);
    }
}

public class RoomChangedEventArgs : EventArgs
{
    public Room room;
}

