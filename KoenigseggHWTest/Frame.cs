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
        
        private UInt16 id = 0;
        private String name = "";
        private UInt16 period = 0;
        private Byte padding = 0x00;
        private Byte byteLength = 0;
        private Byte[] data = new Byte[FRAME_LENGTH_MAX];
        private Kvadblib.MessageHnd handle;
        private List<Signal> signals;

        public Frame(UInt16 aID = 0,
                     string aName = "",
                     UInt16 aPeriod = 0,
                     Byte aByteLength = FRAME_LENGTH_MAX,
                     Kvadblib.MessageHnd aHandle = null)
        {
            SetID(aID);
            SetPeriod(aPeriod);
            SetName(aName);
            SetByteLength(aByteLength);
            SetHandle(aHandle);
            signals = new List<Signal>();
        }

        public override string ToString()
        {
            return $"Frame ID: {id}(0x{id:X})\n" +
                $"Frame name: {name}\n" +
                $"Frame period: {period}\n" +
                $"Data: {data[0]:X} " +
                $"{data[1]:X} " +
                $"{data[2]:X} " +
                $"{data[3]:X} " +
                $"{data[4]:X} " +
                $"{data[5]:X} " +
                $"{data[6]:X} " +
                $"{data[7]:X}\n" ;
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

            return GetID().Equals(other.GetID()) &&
                   GetName().Equals(other.GetName());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Frame);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public UInt16 GetID()
        {
            return id;
        }

        public void SetID(UInt16 aID)
        {
            if(aID <= FRAME_ID_MAX)
            {
                id = aID;
            }
        }

        public UInt16 GetPeriod()
        {
            return period;
        }

        public void SetPeriod(UInt16 aPeriod)
        {
            period = aPeriod;
        }

        protected Byte GetData(Byte aByteNr)
        {
            if(aByteNr < FRAME_LENGTH_MAX)
            {
                return data[aByteNr];
            }
            else
            {
                return 0;
            }
        }

        protected void SetData(Byte aByteNr, Byte aData)
        {
            if(aByteNr < FRAME_LENGTH_MAX)
            {
                data[(Byte)aByteNr] = aData;
            }
        }

        public Byte GetPadding()
        {
            return padding;
        }

        public void SetPadding(Byte aPadding)
        {
            padding =  aPadding;
        }

        public Byte GetByteLength()
        {
            return byteLength;
        }

        public void SetByteLength(Byte aLength)
        {
            if((aLength <= FRAME_LENGTH_MAX) && (aLength != 0))
            {
                byteLength = aLength;
            }
        }

        public String GetName()
        {
            return name;
        }

        public void SetName(String aName)
        {
            name = aName;
        }

        public void AddSignal(Signal aSignal)
        {
            signals.Add(aSignal);
        }

        public void RemoveSignal(Signal aSignal)
        {
            if(signals.Contains(aSignal))
            {
                signals.Remove(aSignal);
            }
        }

        public void SetHandle(Kvadblib.MessageHnd aHandle)
        {
            if (null != aHandle)
            {
                handle = aHandle;
            }
        }

        public Kvadblib.MessageHnd GetHandle()
        {
            return handle;
        }

        public UInt32 GetNrOfSignals()
        {
            return (UInt32)signals.Count;
        }

        public Status.ErrorCode GetSignal(string aName, out Signal aSignal)
        {
            foreach (Signal signal in signals)
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
                aSignal = signals.ElementAt((int)aIdx);
                return Status.ErrorCode.STATUS_OK;
            }
        }
    }
}
