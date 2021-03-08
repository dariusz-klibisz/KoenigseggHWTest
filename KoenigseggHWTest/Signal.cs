using System;
using Kvaser.Kvadblib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenigseggHWTest
{
    class Signal : IEquatable<Signal>
    {
        protected const Byte BITS_IN_BYTE = 8;
        protected const Byte BIT_LENGTH_MAX = 64;

        private string name;
        private Byte startBit;
        private Byte bitLength;
        private double scale;
        private double offset;
        private double min;
        private double max;
        private string unit;
        private Kvadblib.SignalEncoding encoding;
        private Decimal value = 0;
        private UInt64 rawValue = 0;
        private Kvadblib.SignalHnd handle;

        public Signal(string aName = "",
                      Byte aStartBit = 0,
                      Byte aBitLength = 0,
                      double aScale = 1.0,
                      double aOffset = 0.0,
                      double aMin = 0.0,
                      double aMax = 0.0,
                      string aUnit = "",
                      Kvadblib.SignalEncoding aEncoding = Kvadblib.SignalEncoding.Intel,
                      Kvadblib.SignalHnd aHandle = null)
        {
            SetName(aName);
            SetStartBit(aStartBit);
            SetBitLength(aBitLength);
            SetScale(aScale);
            SetOffset(aOffset);
            SetMin(aMin);
            SetMax(aMax);
            SetUnit(aUnit);
            SetEncoding(aEncoding);
            SetHandle(aHandle);
        }

        public override string ToString()
        {
            return $"Signal name: {name}\n" +
                $"Signal startBit: {startBit}\n" +
                $"Signal bitLength: {bitLength}\n" +
                $"Signal scale: {scale}\n" +
                $"Signal offset:{offset}\n" +
                $"Signal min:{min}\n" +
                $"Signal max:{max}\n" +
                $"Signal unit:{unit}\n" +
                $"Signal encoding:{encoding}\n" +
                $"Signal value:{value}\n" +
                $"Signal rawValue:{rawValue}\n" +
                $"Signal handle:{handle}\n";
        }

        public static bool operator ==(Signal obj1, Signal obj2)
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

        public static bool operator !=(Signal obj1, Signal obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Signal other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Name must be unique so it is enough to only compare it
            return GetName().Equals(other.GetName());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Signal);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void SetName(string aName)
        {
            name = aName;
        }

        public string GetName()
        {
            return name;
        }

        public void SetStartBit(Byte aBit)
        {
            if(aBit < ((Byte)Frame.FRAME_LENGTH_MAX * BITS_IN_BYTE))
            {
                startBit = aBit;
            }
        }

        public Byte GetStartBit()
        {
            return startBit;
        }

        public void SetBitLength(Byte aLength)
        {
            if(aLength <= BIT_LENGTH_MAX)
            {
                bitLength = aLength;
            }
        }

        public Byte GetBitLength()
        {
            return bitLength;
        }

        public void SetScale(double aScale)
        {
            scale = aScale;
        }

        public double GetScale()
        {
            return scale;
        }

        public void SetOffset(double aOffset)
        {
            offset = aOffset;
        }

        public double GetOffset()
        {
            return offset;
        }

        public void SetMin(double aMin)
        {
            min = aMin;
        }

        public double GetMin()
        {
            return min;
        }

        public void SetMax(double aMax)
        {
            max = aMax;
        }

        public double GetMax()
        {
            return max;
        }

        public void SetUnit(string aUnit)
        {
            unit = aUnit;
        }

        public string GetUnit()
        {
            return unit;
        }

        public void SetEncoding(Kvadblib.SignalEncoding aEncoding)
        {
            encoding = aEncoding;
        }

        public Kvadblib.SignalEncoding GetEncoding()
        {
            return encoding;
        }

        public void SetValue(decimal aValue)
        {
            value = aValue;
        }

        public decimal GetValue()
        {
            return value;
        }

        public void SetRawValue(UInt64 aValue)
        {
            rawValue = aValue;
        }

        public UInt64 GetRawValue()
        {
            return rawValue;
        }

        public void SetHandle(Kvadblib.SignalHnd aHandle)
        {
            if (null != aHandle)
            {
                handle = aHandle;
            }
        }

        public Kvadblib.SignalHnd GetHandle()
        {
            return handle;
        }
    }
}
