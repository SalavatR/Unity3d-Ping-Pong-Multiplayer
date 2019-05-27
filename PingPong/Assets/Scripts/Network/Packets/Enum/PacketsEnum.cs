namespace Network.Packets.Enum
{
    public enum PacketsEnum : ushort
    {
        RocketMove = 1,
        StartGame,
        Disconnect,
        BallMove,
        Score,
        OnConnect,
        RoomReady,
        GameSettings,
        ClientReady,
        GameOver,
        BallCharacteristics
    }
}
