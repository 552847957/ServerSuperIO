using ServerSuperIO.Common;

namespace ServerSuperIO.Device
{
    public enum WorkMode
    {
        [EnumDescription("�����ģʽ")]
        TcpServer=0x00,
        [EnumDescription("�ͻ���ģʽ")]
        TcpClient=0x01
    }
}
