using ServerSuperIO.Common;

namespace ServerSuperIO.Device
{
    public enum RunState:byte
    {
        [EnumDescription("δ֪")]
        None = 0x00,
        [EnumDescription("�豸����")]
        Run = 0x01,
        [EnumDescription("�豸ֹͣ")]
        Stop = 0x02
    }
}
