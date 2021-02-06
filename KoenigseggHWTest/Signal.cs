using System;
using Kvaser.Kvadblib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenigseggHWTest
{
    class Signal
    {
        protected const Byte BITS_IN_BYTE = 8;
        protected const Byte BIT_LENGTH_MAX = 64;

        private string name = "";
        private UInt16 startBit = 0;
        private UInt32 bitLength = 0;
        private double scale = 1;
        private double offset = 0;
        private double min = 0;
        private double max = 0;
        private string unit = "";
        private Decimal value = 0;
        private UInt64 rawValue = 0;
        private Kvadblib.SignalHnd signalHandle;

        public Signal(string aName = "",
                      UInt16 aStartBit = 0,
                      UInt16 aBitLength = 0,
                      double aScale = 1.0,
                      double aOffset = 0.0,
                      double aMin = 0.0,
                      double aMax = 0.0,
                      string aUnit = "",
                      Kvadblib.SignalHnd aSignalHandle = null)
        {
            SetName(aName);
            SetStartBit(aStartBit);
            SetBitLength(aBitLength);
            SetScale(aScale);
            SetOffset(aOffset);
            SetMin(aMin);
            SetMax(aMax);
            SetUnit(aUnit);
            SetSignalHandle(aSignalHandle);
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
                $"Signal value:{value}\n" +
                $"Signal rawValue:{rawValue}\n" +
                $"Signal signalHandle:{signalHandle}\n";
        }

        public void SetName(string aName)
        {
            name = aName;
        }

        public string GetName()
        {
            return name;
        }

        public void SetStartBit(UInt16 aBit)
        {
            if(aBit < ((UInt16)Frame.FRAME_LENGTH_MAX * BITS_IN_BYTE))
            {
                startBit = aBit;
            }
        }

        public UInt16 GetStartBit()
        {
            return startBit;
        }

        public void SetBitLength(UInt32 aLength)
        {
            if(aLength <= BIT_LENGTH_MAX)
            {
                bitLength = aLength;
            }
        }

        public UInt32 GetBitLength()
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

        public void SetSignalHandle(Kvadblib.SignalHnd aSignalHandle)
        {
            if (null != aSignalHandle)
            {
                signalHandle = aSignalHandle;
            }
        }

        public Kvadblib.SignalHnd GetSignalHandle()
        {
            return signalHandle;
        }
    }
}
