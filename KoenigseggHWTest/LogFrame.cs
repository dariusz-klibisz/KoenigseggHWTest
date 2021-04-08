using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvaser.Kvadblib;
using canlibCLSNET;

namespace KoenigseggHWTest
{
    class LogFrame : Frame
    {
        private int channel;
        private double time;

        public LogFrame(UInt16 aID = 0,
                        string aName = "",
                        UInt16 aPeriod = 0,
                        Byte aByteLength = FRAME_LENGTH_MAX,
                        Kvadblib.MessageHnd aHandle = null,
                        Boolean aEnableTrans = true,
                        int aChannel = 0,
                        double aTime = 0.0) : base(aID, aName, aPeriod, aByteLength, aHandle, aEnableTrans)
        {
            SetChannel(aChannel);
            SetTime(aTime);
        }

        public int GetChannel()
        {
            return channel;
        }

        public void SetChannel(int aChannel)
        {
            channel = aChannel;
        }

        public double GetTime()
        {
            return time;
        }

        public void SetTime(double aTime)
        {
            time = aTime;
        }

        public void SendFrame(int[] aCanChanHdl)
        {
            Canlib.canWrite(aCanChanHdl[channel], id, data, byteLength, 0);
        }
    }
}
