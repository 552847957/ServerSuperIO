using System;
using ServerSuperIO.CommandCache;

namespace ServerSuperIO.CommandCache
{
    public class Command : ServerSuperIO.CommandCache.ICommand 
    {
        private byte[] _CommandBytes = new byte[] { };

        /// <summary>
        /// ����
        /// </summary>
        public byte[] CommandBytes
        {
            get { return _CommandBytes; }
        }

        private string _CommandKey = String.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string CommandKey
        {
            get { return _CommandKey; }
        }

        private CommandPriority _Priority = CommandPriority.Normal;
        /// <summary>
        /// �������ȼ�����ʱ����
        /// </summary>
        public CommandPriority Priority
        {
            get { return _Priority; }
        }

        /// <summary>
        /// �豸����
        /// </summary>
        /// <param name="cmdkeys">��������</param>
        /// <param name="cmdbytes">�����ֽ�����</param>
        public Command(string cmdkey, byte[] cmdbytes)
        {
            this._CommandKey = cmdkey;
            this._CommandBytes = cmdbytes;
            this._Priority = CommandPriority.Normal;
        }

        /// <summary>
        /// �豸����
        /// </summary>
        /// <param name="cmdkeys">��������</param>
        /// <param name="cmdbytes">�����ֽ�����</param>
        public Command(string cmdkey, byte[] cmdbytes,CommandPriority priority)
        {
            this._CommandKey = cmdkey;
            this._CommandBytes = cmdbytes;
            this._Priority = priority;
        }
    }
}
