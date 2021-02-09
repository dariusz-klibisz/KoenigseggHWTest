using System;
using Kvaser.Kvadblib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenigseggHWTest
{
    class Frame : IEquatable<Frame>
    {
        protected const UInt16 FRAME_ID_MAX = 2047;
        public const Byte FRAME_LENGTH_MAX = 8;
        protected const Byte BYTE_MASK = 0xFF;
        protected const Byte BYTE_SHIFT = 8;
        
        private UInt16 frameID = 0;
        private String frameName = "";
        private UInt16 framePeriod = 0;
        private Byte framePadding = 0x00;
        private Byte frameByteLength = 0;
        private Byte[] frameData = new Byte[FRAME_LENGTH_MAX];
        private Kvadblib.MessageHnd frameHandle;
        private List<Signal> frameSignals = new List<Signal>();

        public Frame(UInt16 aFrameID = 0, string aName = "", UInt16 aFramePeriod = 0, Byte aFrameByteLength = FRAME_LENGTH_MAX, Kvadblib.MessageHnd aFrameHandle = null)
        {
            SetFrameID(aFrameID);
            SetFramePeriod(aFramePeriod);
            SetFrameName(aName);
            SetFrameByteLength(aFrameByteLength);
            SetFrameHandle(aFrameHandle);
        }

        public override string ToString()
        {
            return $"Frame ID: {frameID}(0x{frameID:X})\n" +
                $"Frame name: {frameName}\n" +
                $"Frame period: {framePeriod}\n" +
                $"Data: {frameData[0]:X} " +
                $"{frameData[1]:X} " +
                $"{frameData[2]:X} " +
                $"{frameData[3]:X} " +
                $"{frameData[4]:X} " +
                $"{frameData[5]:X} " +
                $"{frameData[6]:X} " +
                $"{frameData[7]:X}\n" ;
        }

        public static bool operator ==(Frame obj1, Frame obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            if (ReferenceEquals(obj1, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2, null))
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Frame obj1, Frame obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Frame other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return GetFrameID().Equals(other.GetFrameID()) &&
                   GetFrameName().Equals(other.GetFrameName());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Frame);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public UInt16 GetFrameID()
        {
            return frameID;
        }

        public void SetFrameID(UInt16 aFrameID)
        {
            if(aFrameID <= FRAME_ID_MAX)
            {
                frameID = aFrameID;
            }
        }

        public UInt16 GetFramePeriod()
        {
            return framePeriod;
        }

        public void SetFramePeriod(UInt16 aFramePeriod)
        {
            framePeriod = aFramePeriod;
        }

        protected Byte GetFrameData(Byte aByteNr)
        {
            if(aByteNr < FRAME_LENGTH_MAX)
            {
                return frameData[aByteNr];
            }
            else
            {
                return 0;
            }
        }

        protected void SetFrameData(Byte aByteNr, Byte aData)
        {
            if(aByteNr < FRAME_LENGTH_MAX)
            {
                frameData[(Byte)aByteNr] = aData;
            }
        }

        public Byte GetFramePaddingValue()
        {
            return framePadding;
        }

        public void SetFramePaddingValue(Byte aPad)
        {
            framePadding =  aPad;
        }

        public Byte GetFrameByteLength()
        {
            return frameByteLength;
        }

        public void SetFrameByteLength(Byte aLength)
        {
            if((aLength <= FRAME_LENGTH_MAX) && (aLength != 0))
            {
                frameByteLength = aLength;
            }
        }

        public String GetFrameName()
        {
            return frameName;
        }

        public void SetFrameName(String aName)
        {
            frameName = aName;
        }

        public void AddSignal(Signal aSignal)
        {
            frameSignals.Add(aSignal);
        }

        public void RemoveSignal(Signal aSignal)
        {
            if(frameSignals.Contains(aSignal))
            {
                frameSignals.Remove(aSignal);
            }
        }

        public void SetFrameHandle(Kvadblib.MessageHnd aFrameHandle)
        {
            if (null != aFrameHandle)
            {
                frameHandle = aFrameHandle;
            }
        }

        public Kvadblib.MessageHnd GetFrameHandle()
        {
            return frameHandle;
        }

        public UInt32 GetNrOfSignals()
        {
            return (UInt32)frameSignals.Count;
        }

        public Status.ErrorCode GetSignal(string aName, out Signal aSignal)
        {
            foreach (Signal signal in frameSignals)
            {
                if (signal.GetName() == aName)
                {
                    aSignal = signal;
                    return Status.ErrorCode.STATUS_OK;
                }
            }
            aSignal = null;
            return Status.ErrorCode.STATUS_OBJ_NOT_FOUND;
        }

        public Status.ErrorCode GetSignal(UInt32 aIdx, out Signal aSignal)
        {
            if (aIdx >= GetNrOfSignals())
            {
                aSignal = null;
                return Status.ErrorCode.STATUS_OBJ_NOT_FOUND;
            }
            else
            {
                aSignal = frameSignals.ElementAt((int)aIdx);
                return Status.ErrorCode.STATUS_OK;
            }
        }
    }
}
