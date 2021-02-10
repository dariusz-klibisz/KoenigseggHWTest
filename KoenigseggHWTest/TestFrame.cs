using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenigseggHWTest
{
    class TestFrame : Frame
    {
        private const Byte FRAME_PADDING = 0xAA;
        private const Byte FRAME_PAYLOAD = 6;

        /*{ 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA }; */
        private enum FrameStructure
        {
            FRAME_PAYLOAD_BYTE,           /* 0 */
            FRAME_ROUTINE_BYTE,           /* 1 */
            FRAME_PIN_NR_BYTE_HIGH,       /* 2 */
            FRAME_PIN_NR_BYTE_LOW,        /* 3 */
            FRAME_PIN_CFG_BYTE_HIGH,      /* 4 */
            FRAME_PIN_CFG_BYTE_LOW,       /* 5 */
            FRAME_PIN_LVL_BYTE,           /* 6 */
            FRAME_PADDING_BYTE,           /* 7 */
            FRAME_LENGTH                  /* 8 */
        }
        public enum PinLevel
        {
            FRAME_PIN_LVL_LOW,      /* 0 */
            FRAME_PIN_LVL_HIGH,     /* 1 */
            FRAME_PIN_LVL_TOGGLE,   /* 2 */
            FRAME_PIN_LVL_MAX       /* 3 */
        }

        public TestFrame(UInt16 aID = 0, UInt16 aPeriod = 0)
        {
            SetID(aID);
            SetPeriod(aPeriod);
            /* Set data that wont change. */
            SetFramePayload(FRAME_PAYLOAD);
            SetFramePadding(FRAME_PADDING);
        }

        public void SetFramePayload(Byte aPayload)
        {
            if (aPayload < (Byte)FrameStructure.FRAME_LENGTH)
            {
                SetData((Byte)FrameStructure.FRAME_PAYLOAD_BYTE, aPayload);
            }
        }

        public void SetUDSRoutine(Byte aRoutine)
        {
            SetData((Byte)FrameStructure.FRAME_ROUTINE_BYTE, aRoutine);
        }

        public void SetPinNumber(UInt16 aPinNr)
        {
            if (aPinNr <= FRAME_ID_MAX)
            {
                Byte pinNrHigh = (Byte)((aPinNr >> BYTE_SHIFT) & BYTE_MASK);
                Byte pinNrLow = (Byte)(aPinNr & BYTE_MASK);
                SetData((Byte)FrameStructure.FRAME_PIN_NR_BYTE_HIGH, pinNrHigh);
                SetData((Byte)FrameStructure.FRAME_PIN_NR_BYTE_LOW, pinNrLow);
            }
        }

        public void SetPinConfig(UInt16 aPinCfg)
        {
            Byte pinCfgHigh = (Byte)((aPinCfg >> BYTE_SHIFT) & BYTE_MASK);
            Byte pinCfgLow = (Byte)(aPinCfg & BYTE_MASK);
            SetData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_HIGH, pinCfgHigh);
            SetData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_LOW, pinCfgLow);
        }

        public UInt16 GetPinConfig()
        {
            Byte byteHigh = GetData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_HIGH);
            Byte byteLow = GetData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_LOW);
            return (UInt16)((UInt16)byteHigh << BYTE_SHIFT | byteLow);
        }

        public void SetPinLevel(PinLevel aPinLvl)
        {
            if (aPinLvl < PinLevel.FRAME_PIN_LVL_MAX)
            {
                SetData((Byte)FrameStructure.FRAME_PIN_LVL_BYTE, (Byte)aPinLvl);
            }
        }

        public void SetFramePadding(Byte aPad)
        {
            SetData((Byte)FrameStructure.FRAME_PADDING_BYTE, aPad);
        }
    }
}
