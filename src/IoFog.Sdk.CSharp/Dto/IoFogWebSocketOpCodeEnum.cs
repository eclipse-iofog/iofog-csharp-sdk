namespace IoFog.Sdk.CSharp.Dto
{
    internal enum IoFogWebSocketOpCodeEnum : byte
    {
        Ping = 0x9,
        Pong = 0xA,
        Ack = 0xB,
        ControlSignal = 0xC,
        Message = 0xD,
        Receipt = 0xE
    }
}
