using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ServerSuperIO.CommandCache;

namespace ServerSuperIO.CommandCache
{
    /// <summary>
    /// �̰߳�ȫ�������������ṩ�˴�һ�����һ��ֵ��ӳ�䡣
    /// </summary>
    /// <typeparam name="TKey">�ֵ��еļ�������</typeparam>
    /// <typeparam name="TValue">�ֵ��е�ֵ������</typeparam>
    public class CommandCache : ServerSuperIO.CommandCache.ICommandCache
    {
        #region Fields
        /// <summary>
        /// �ڲ��� Dictionary ����
        /// </summary>
        private List<ICommand> _CmdCache = new List<ICommand>();
        /// <summary>
        /// ���ڲ���ͬ�����ʵ� RW ������
        /// </summary>
        private ReaderWriterLock rwLock = new ReaderWriterLock();
        /// <summary>
        /// һ�� TimeSpan������ָ����ʱʱ�䡣 
        /// </summary>
        private readonly TimeSpan lockTimeOut = TimeSpan.FromMilliseconds(100);
        #endregion

        #region Methods
        /// <summary>
        /// ��ָ���ļ���ֵ��ӵ��ֵ��С�
        /// Exceptions��
        ///     ArgumentException - Dictionary ���Ѵ��ھ�����ͬ����Ԫ�ء�
        /// </summary>
        /// <param name="key">Ҫ��ӵ�Ԫ�صļ���</param>
        /// <param name="value">��ӵ�Ԫ�ص�ֵ�������������ͣ���ֵ����Ϊ ������</param>
        public void Add(string cmdkey, byte[] cmdbytes)
        {
            this.Add(cmdkey, cmdbytes, CommandPriority.Normal);
        }

        public void Add(string cmdkey, byte[] cmdbytes, CommandPriority priority)
        {
            rwLock.AcquireWriterLock(lockTimeOut);
            try
            {
                Command cmd = new Command(cmdkey, cmdbytes,priority);
                this._CmdCache.Add(cmd);
            }
            finally { rwLock.ReleaseWriterLock(); }
        }

        public void Add(ICommand cmd)
        {
            rwLock.AcquireWriterLock(lockTimeOut);
            try
            {
                if (cmd == null) return;

                this._CmdCache.Add(cmd);
            }
            finally { rwLock.ReleaseWriterLock(); }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="cmdkey"></param>
        public void Remove(string cmdkey)
        {
            if (_CmdCache.Count <= 0)
            {
                return;
            }

            rwLock.AcquireWriterLock(lockTimeOut);
            try
            {
                ICommand cmd = this._CmdCache.FirstOrDefault(c => c.CommandKey == cmdkey);
                if(cmd!=null)
                {
                    this._CmdCache.Remove(cmd);
                }
            }
            finally { rwLock.ReleaseWriterLock(); }
        }

        /// <summary>
        /// ���Ƴ����еļ���ֵ��
        /// </summary>
        public void Clear()
        {
            if (this._CmdCache.Count > 0)
            {
                rwLock.AcquireWriterLock(lockTimeOut);
                try
                {
                    this._CmdCache.Clear();
                }
                finally { rwLock.ReleaseWriterLock(); }
            }
        }

        /// <summary>
        /// �����ȼ��������
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public byte[] Get(CommandPriority priority)
        {
            if (this._CmdCache.Count <= 0)
            {
                return new byte[] {};
            }

            rwLock.AcquireReaderLock(lockTimeOut);
            try
            {
                byte[] data = new byte[] { };
                if (priority == CommandPriority.Normal)
                {
                    data = this._CmdCache[0].CommandBytes;
                    this._CmdCache.RemoveAt(0);
                }
                else if(priority==CommandPriority.High)
                {
                    ICommand cmd = this._CmdCache.FirstOrDefault(c => c.Priority == CommandPriority.High);
                    if (cmd != null)
                    {
                        data = cmd.CommandBytes;
                        this._CmdCache.Remove(cmd);
                    }
                }
                return data;
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }
        }

        public byte[] Get(string cmdkey)
        {
            if (this._CmdCache.Count <= 0)
            {
                return new byte[] { };
            }

            rwLock.AcquireReaderLock(lockTimeOut);
            try
            {
                ICommand cmd=this._CmdCache.FirstOrDefault(c => c.CommandKey == cmdkey);
                if (cmd == null)
                {
                    return new byte[] { };
                }
                else
                {
                    byte[] data = cmd.CommandBytes;
                    this._CmdCache.Remove(cmd);
                    return data;
                }
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// ˳��������
        /// </summary>
        /// <returns></returns>
        public byte[] Get()
        {
            return Get(CommandPriority.Normal);
        }

        public int Count
        {
            get { return this._CmdCache.Count; }
        }
        #endregion
    }
}
