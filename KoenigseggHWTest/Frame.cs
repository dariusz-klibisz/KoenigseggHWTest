using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenigseggHWTest
{
    class Frame
    {
        private const UInt16 frameIDMax = 2047;
        private const Byte framePayload = 6;
        private const Byte framePadding = 0xAA;
        private const Byte byteMask = 0xFF;
        private const Byte byteShift = 8;
        private enum FrameStructure
        {
            framePayloadByte,           /* 0 */
            frameRoutineByte,           /* 1 */
            framePinNrByteHigh,         /* 2 */
            framePinNrByteLow,          /* 3 */
            framePinCfgByteHigh,        /* 4 */
            framePinCfgByteLow,         /* 5 */
            framePinLevelByte,          /* 6 */
            framePaddingByte,           /* 7 */
            frameLength                 /* 8 */
        }
        public enum PinLevel
        {
            pinLvlLow,      /* 0 */
            pinLvlHigh,     /* 1 */
            pinLvlToggle,   /* 2 */
            PinLvlMax       /* 3 */
        }
        private UInt16 frameID = 0;
        private UInt16 framePeriod = 0;
        private Byte[] frameData = new Byte[(Byte)FrameStructure.frameLength]; /*{ 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA }; */

        public Frame(UInt16 aFrameID = 0, UInt16 aFramePeriod = 0)
        {
            SetFrameID(aFrameID);
            SetFramePeriod(aFramePeriod);
            /* Set data that wont change. */
            SetFramePayload(framePayload);
            SetFramePadding(framePadding);
        }

        public override string ToString()
        {
            return $"Frame ID: {frameID}(0x{frameID:X})\n" +
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

        public UInt16 GetFrameID()
        {
            return frameID;
        }

        public void SetFrameID(UInt16 aFrameID)
        {
            if(aFrameID <= frameIDMax)
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

        private Int16 GetFrameData(Byte aByteNr)
        {
            if(aByteNr < (Byte)FrameStructure.frameLength)
            {
                return frameData[aByteNr];
            }
            else
            {
                return -1;
            }
        }

        private void SetFrameData(FrameStructure aByteNr, Byte aData)
        {
            if(aByteNr < FrameStructure.frameLength)
            {
                frameData[(Byte)aByteNr] = aData;
            }
        }

        public void SetFramePayload(Byte aPayload)
        {
            if(aPayload < (Byte)FrameStructure.frameLength)
            {
                SetFrameData(FrameStructure.framePayloadByte, aPayload);
            }
        }

        public void SetUDSRoutine(Byte aRoutine)
        {
            SetFrameData(FrameStructure.frameRoutineByte, aRoutine);
        }

        public void SetPinNumber(UInt16 aPinNr)
        {
            if(aPinNr <= frameIDMax)
            {
                Byte pinNrHigh = (Byte)((aPinNr >> byteShift) & byteMask);
                Byte pinNrLow = (Byte)(aPinNr & byteMask);
                SetFrameData(FrameStructure.framePinNrByteHigh, pinNrHigh);
                SetFrameData(FrameStructure.framePinNrByteLow, pinNrLow);
            }
        }

        public void SetPinConfig(UInt16 aPinCfg)
        {
            Byte pinCfgHigh = (Byte)((aPinCfg >> byteShift) & byteMask);
            Byte pinCfgLow = (Byte)(aPinCfg & byteMask);
            SetFrameData(FrameStructure.framePinCfgByteHigh, pinCfgHigh);
            SetFrameData(FrameStructure.framePinCfgByteLow, pinCfgLow);
        }

        public void SetPinLevel(PinLevel aPinLvl)
        {
            if(aPinLvl < PinLevel.PinLvlMax)
            {
                SetFrameData(FrameStructure.framePinLevelByte, (Byte)aPinLvl);
            }
        }

        public void SetFramePadding(Byte aPad)
        {
            SetFrameData(FrameStructure.framePaddingByte, aPad);
        }
    }
}
