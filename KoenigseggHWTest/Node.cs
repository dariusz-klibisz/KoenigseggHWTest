using Kvaser.Kvadblib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoenigseggHWTest
{
    class Node : IEquatable<Node>
    {
        private string name { get; set; } = "";
        private List<Frame> frames;
        Kvadblib.NodeHnd handle;
        private Boolean enableTransmission;

        public Node(String aName = "",
                    Kvadblib.NodeHnd aHandle = null,
                    Boolean aEnableTrans = true)
        {
            SetName(aName);
            SetHandle(aHandle);
            frames = new List<Frame>();
            SetEnableTransmission(aEnableTrans, false);
        }

        public override string ToString()
        {
            string str = $"Node name: {name}\n";
            foreach (Frame frame in frames)
            {
                str += $"{frame}\n";
            }

            return str;
        }

        public static bool operator ==(Node obj1, Node obj2)
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

        public static bool operator !=(Node obj1, Node obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Node other)
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
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string aName)
        {
            name = aName;
        }

        public Boolean RemoveFrame(Frame aFrame)
        {
            Boolean ret = false;

            if ((0 != frames.Count) &&
               (null != aFrame))
            {
                if (frames.Contains(aFrame))
                {
                    if (frames.Remove(aFrame))
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public Boolean AddFrame(Frame aFrame)
        {
            Boolean ret = false;

            if ((null != aFrame) &&
               (!frames.Contains(aFrame)))
            {
                frames.Add(aFrame);
                ret = true;
            }

            return ret;
        }

        public void SetHandle(Kvadblib.NodeHnd aHandle)
        {
            if (null != aHandle)
            {
                handle = aHandle;
            }
        }

        public Kvadblib.NodeHnd GetNodeHandle()
        {
            return handle;
        }

        public UInt32 GetNrOfFrames()
        {
            return (UInt32)frames.Count;
        }

        public Status.ErrorCode GetFrame(string aName, out Frame aFrame)
        {
            foreach (Frame frame in frames)
            {
                if (frame.GetName() == aName)
                {
                    aFrame = frame;
                    return Status.ErrorCode.STATUS_OK;
                }
            }
            aFrame = null;
            return Status.ErrorCode.STATUS_OBJ_NOT_FOUND;
        }

        public Status.ErrorCode GetFrame(UInt32 aIdx, out Frame aFrame)
        {
            if (aIdx >= GetNrOfFrames())
            {
                aFrame = null;
                return Status.ErrorCode.STATUS_OBJ_NOT_FOUND;
            }
            else
            {
                aFrame = frames.ElementAt((int)aIdx);
                return Status.ErrorCode.STATUS_OK;
            }
        }

        public void SetEnableTransmission(Boolean aEnableTrans, Boolean aUpdateFrames)
        {
            enableTransmission = aEnableTrans;

            if(aUpdateFrames)
            {
                foreach (Frame frame in frames)
                {
                    frame.SetEnableTransmission(aEnableTrans);
                }
            }
        }

        public Boolean GetEnableTransmission()
        {
            return enableTransmission;
        }
    }
}
