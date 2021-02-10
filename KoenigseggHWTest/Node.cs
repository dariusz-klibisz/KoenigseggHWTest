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
        private string nodeName { get; set; } = "";
        private List<Frame> nodeFrames;
        Kvadblib.NodeHnd nodeHandle;

        public Node(String aName = "", Kvadblib.NodeHnd aNodeHandle = null)
        {
            SetNodeName(aName);
            SetNodeHandle(aNodeHandle);
            nodeFrames = new List<Frame>();
        }

        public override string ToString()
        {
            string str = $"Node name: {nodeName}\n";
            foreach (Frame frame in nodeFrames)
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
            return GetNodeName().Equals(other.GetNodeName());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string GetNodeName()
        {
            return nodeName;
        }

        public void SetNodeName(string aName)
        {
            nodeName = aName;
        }

        public Boolean RemoveFrame(Frame aFrame)
        {
            Boolean ret = false;

            if ((0 != nodeFrames.Count) &&
               (null != aFrame))
            {
                if (nodeFrames.Contains(aFrame))
                {
                    if (nodeFrames.Remove(aFrame))
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
               (!nodeFrames.Contains(aFrame)))
            {
                nodeFrames.Add(aFrame);
                ret = true;
            }

            return ret;
        }

        public void SetNodeHandle(Kvadblib.NodeHnd aNodeHandle)
        {
            if (null != aNodeHandle)
            {
                nodeHandle = aNodeHandle;
            }
        }

        public Kvadblib.NodeHnd GetNodeHandle()
        {
            return nodeHandle;
        }

        public UInt32 GetNrOfFrames()
        {
            return (UInt32)nodeFrames.Count;
        }

        public Status.ErrorCode GetFrame(string aName, out Frame aFrame)
        {
            foreach (Frame frame in nodeFrames)
            {
                if (frame.GetFrameName() == aName)
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
                aFrame = nodeFrames.ElementAt((int)aIdx);
                return Status.ErrorCode.STATUS_OK;
            }
        }
    }
}
