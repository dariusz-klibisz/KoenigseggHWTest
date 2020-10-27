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

        public TestFrame(UInt16 aFrameID = 0, UInt16 aFramePeriod = 0)
        {
            SetFrameID(aFrameID);
            SetFramePeriod(aFramePeriod);
            /* Set data that wont change. */
            SetFramePayload(FRAME_PAYLOAD);
            SetFramePadding(FRAME_PADDING);
        }

        public void SetFramePayload(Byte aPayload)
        {
            if (aPayload < (Byte)FrameStructure.FRAME_LENGTH)
            {
                SetFrameData((Byte)FrameStructure.FRAME_PAYLOAD_BYTE, aPayload);
            }
        }

        public void SetUDSRoutine(Byte aRoutine)
        {
            SetFrameData((Byte)FrameStructure.FRAME_ROUTINE_BYTE, aRoutine);
        }

        public void SetPinNumber(UInt16 aPinNr)
        {
            if (aPinNr <= FRAME_ID_MAX)
            {
                Byte pinNrHigh = (Byte)((aPinNr >> BYTE_SHIFT) & BYTE_MASK);
                Byte pinNrLow = (Byte)(aPinNr & BYTE_MASK);
                SetFrameData((Byte)FrameStructure.FRAME_PIN_NR_BYTE_HIGH, pinNrHigh);
                SetFrameData((Byte)FrameStructure.FRAME_PIN_NR_BYTE_LOW, pinNrLow);
            }
        }

        public void SetPinConfig(UInt16 aPinCfg)
        {
            Byte pinCfgHigh = (Byte)((aPinCfg >> BYTE_SHIFT) & BYTE_MASK);
            Byte pinCfgLow = (Byte)(aPinCfg & BYTE_MASK);
            SetFrameData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_HIGH, pinCfgHigh);
            SetFrameData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_LOW, pinCfgLow);
        }

        public UInt16 GetPinConfig()
        {
            Byte byteHigh = GetFrameData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_HIGH);
            Byte byteLow = GetFrameData((Byte)FrameStructure.FRAME_PIN_CFG_BYTE_LOW);
            return (UInt16)((UInt16)byteHigh << BYTE_SHIFT | byteLow);
        }

        public void SetPinLevel(PinLevel aPinLvl)
        {
            if (aPinLvl < PinLevel.FRAME_PIN_LVL_MAX)
            {
                SetFrameData((Byte)FrameStructure.FRAME_PIN_LVL_BYTE, (Byte)aPinLvl);
            }
        }

        public void SetFramePadding(Byte aPad)
        {
            SetFrameData((Byte)FrameStructure.FRAME_PADDING_BYTE, aPad);
        }
    }
}
