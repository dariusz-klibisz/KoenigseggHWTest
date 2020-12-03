using Kvaser.Kvadblib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoenigseggHWTest
{
    class Node
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

            if((0 != nodeFrames.Count) &&
               (null != aFrame))
            {
                if(nodeFrames.Contains(aFrame))
                {
                    if(nodeFrames.Remove(aFrame))
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

            if((null != aFrame) &&
               (!nodeFrames.Contains(aFrame)))
            {
                nodeFrames.Add(aFrame);
                ret = true;
            }

            return ret;
        }

        public void SetNodeHandle(Kvadblib.NodeHnd aNodeHandle)
        {
            if(null != aNodeHandle)
            {
                nodeHandle = aNodeHandle;
            }
        }

        public Kvadblib.NodeHnd GetNodeHandle()
        {
            return nodeHandle;
        }
    }
}
