using ServerSuperIO.Common;

namespace ServerSuperIO.Device
{
    public enum DeviceType : byte
    {
        [EnumDescription("��ͨ�豸")]
        Common = 0x00,
        [EnumDescription("�����豸")]
        Virtual = 0x01,
    }
}
