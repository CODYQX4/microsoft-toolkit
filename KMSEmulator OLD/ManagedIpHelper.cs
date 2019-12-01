using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace KMSEmulator
{
    #region Managed IP Helper API
    public class TcpTable : IEnumerable<TcpRow>
    {
        #region Private Fields
        private readonly IEnumerable<TcpRow> _tcpRows;
        #endregion

        #region Constructors
        public TcpTable(IEnumerable<TcpRow> tcpRows)
        {
            _tcpRows = tcpRows;
        }
        #endregion

        #region Public Properties
        public IEnumerable<TcpRow> Rows
        {
            get { return _tcpRows; }
        }
        #endregion

        #region IEnumerable<TcpRow> Members
        public IEnumerator<TcpRow> GetEnumerator()
        {
            return _tcpRows.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tcpRows.GetEnumerator();
        }
        #endregion
    }

    public class TcpRow
    {
        #region Private Fields
        private readonly IPEndPoint _localEndPoint;
        private readonly IPEndPoint _remoteEndPoint;
        private readonly TcpState _state;
        private readonly int _processId;
        #endregion

        #region Constructors
        public TcpRow(IpHelper.TcpRow tcpRow)
        {
            _state = tcpRow.state;
            _processId = tcpRow.owningPid;

            int localPort = (tcpRow.localPort1 << 8) + (tcpRow.localPort2) + (tcpRow.localPort3 << 24) + (tcpRow.localPort4 << 16);
            long localAddress = tcpRow.localAddr;
            _localEndPoint = new IPEndPoint(localAddress, localPort);

            int remotePort = (tcpRow.remotePort1 << 8) + (tcpRow.remotePort2) + (tcpRow.remotePort3 << 24) + (tcpRow.remotePort4 << 16);
            long remoteAddress = tcpRow.remoteAddr;
            _remoteEndPoint = new IPEndPoint(remoteAddress, remotePort);
        }
        #endregion

        #region Public Properties
        public IPEndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
        }

        public IPEndPoint RemoteEndPoint
        {
            get { return _remoteEndPoint; }
        }

        public TcpState State
        {
            get { return _state; }
        }

        public int ProcessId
        {
            get { return _processId; }
        }
        #endregion
    }

    public static class ManagedIpHelper
    {
        #region Public Methods
        public static TcpTable GetExtendedTcpTable(bool sorted)
        {
            List<TcpRow> tcpRows = new List<TcpRow>();

            IntPtr tcpTable = IntPtr.Zero;
            int tcpTableLength = 0;

            if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, sorted, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) != 0)
            {
                try
                {
                    tcpTable = Marshal.AllocHGlobal(tcpTableLength);
                    if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, true, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) == 0)
                    {
                        IpHelper.TcpTable table = (IpHelper.TcpTable)Marshal.PtrToStructure(tcpTable, typeof(IpHelper.TcpTable));

                        IntPtr rowPtr = (IntPtr)((long)tcpTable + Marshal.SizeOf(table.length));
                        for (int i = 0; i < table.length; ++i)
                        {
                            tcpRows.Add(new TcpRow((IpHelper.TcpRow)Marshal.PtrToStructure(rowPtr, typeof(IpHelper.TcpRow))));
                            rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(typeof(IpHelper.TcpRow)));
                        }
                    }
                }
                finally
                {
                    if (tcpTable != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(tcpTable);
                    }
                }
            }

            return new TcpTable(tcpRows);
        }
        #endregion
    }
    #endregion

    #region P/Invoke IP Helper API
    /// <summary>
    /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366073.aspx"/>
    /// </summary>
    public static class IpHelper
    {
        #region Public Fields
        public const string DllName = "iphlpapi.dll";
        public const int AfInet = 2;
        #endregion

        #region Public Methods
        /// <summary>
        /// <see cref="http://msdn2.microsoft.com/en-us/library/aa365928.aspx"/>
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, int ipVersion, TcpTableType tcpTableType, int reserved);
        #endregion

        #region Public Enums
        /// <summary>
        /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366386.aspx"/>
        /// </summary>
        public enum TcpTableType
        {
            BasicListener,
            BasicConnections,
            BasicAll,
            OwnerPidListener,
            OwnerPidConnections,
            OwnerPidAll,
            OwnerModuleListener,
            OwnerModuleConnections,
            OwnerModuleAll,
        }
        #endregion

        #region Public Structs
        /// <summary>
        /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366921.aspx"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TcpTable
        {
            public uint length;
            public TcpRow row;
        }

        /// <summary>
        /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366913.aspx"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TcpRow
        {
            public TcpState state;
            public uint localAddr;
            public byte localPort1;
            public byte localPort2;
            public byte localPort3;
            public byte localPort4;
            public uint remoteAddr;
            public byte remotePort1;
            public byte remotePort2;
            public byte remotePort3;
            public byte remotePort4;
            public int owningPid;
        }
        #endregion
    }
    #endregion
}
