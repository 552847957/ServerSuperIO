using ServerSuperIO.Communicate.COM;
using ServerSuperIO.Communicate.NET;
using ServerSuperIO.Device;
using ServerSuperIO.Persistence;

namespace ServerSuperIO.Device
{
    public interface IDeviceParameter : IVirtualDeviceParameter,IXmlPersistence
    {
        /// <summary>
        /// �豸ID
        /// </summary>
        int DeviceID { get;set;}

        /// <summary>
        /// �豸��ַ
        /// </summary>
        int DeviceAddr { get;set;}

        /// <summary>
        /// �豸����
        /// </summary>
        string DeviceName { get;set;}

        /// <summary>
        /// �Ƿ񱣴�ԭʼ����
        /// </summary>
        bool IsSaveOriginBytes { set;get;}

        /// <summary>
        /// �Ƿ񱨾�
        /// </summary>
        bool IsAlert { get;set;}

        /// <summary>
        /// ��������������ǲ�����ʾ����
        /// </summary>
        bool IsAlertSound { get;set;}

        /// <summary>
        /// ���ڲ�����Ϣ
        /// </summary>
        COMParameter COM { get;set; }

        /// <summary>
        /// �������ӵ�Զ�̲���
        /// </summary>
        SocketParameter NET { get;set; }

        /// <summary>
        /// ��ʽ������
        /// </summary>
        string DataFormat { get;set;}
    }
}