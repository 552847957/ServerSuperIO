using ServerSuperIO.Common;

namespace ServerSuperIO.CommandCache
{
    public enum CommandPriority
    {
        [EnumDescription("��������")]
        Normal = 0x00,
        [EnumDescription("���ȷ���")]
        High
    }
}
