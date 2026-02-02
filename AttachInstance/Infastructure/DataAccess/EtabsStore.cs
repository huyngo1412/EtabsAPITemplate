using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.Infastructure.DataAccess
{
    internal sealed class EtabsStore
    {
        // Stories
        public int NumberStories;
        public string[] StoryNames = System.Array.Empty<string>();
        public double[] StoryElevations = System.Array.Empty<double>();
        public double[] StoryHeights = System.Array.Empty<double>();
        public bool[] IsMasterStory = System.Array.Empty<bool>();
        public string[] SimilarToStory = System.Array.Empty<string>();
        public bool[] SpliceAbove = System.Array.Empty<bool>();
        public double[] SpliceHeight = System.Array.Empty<double>();

        // Frames
        public int NumberFrames;
        public string[] FrameName = System.Array.Empty<string>();
        public string[] PropName = System.Array.Empty<string>();
        public string[] FrameStoryName = System.Array.Empty<string>();
        public string[] PointName1 = System.Array.Empty<string>();
        public string[] PointName2 = System.Array.Empty<string>();
        public double[] Point1X = System.Array.Empty<double>();
        public double[] Point1Y = System.Array.Empty<double>();
        public double[] Point1Z = System.Array.Empty<double>();
        public double[] Point2X = System.Array.Empty<double>();
        public double[] Point2Y = System.Array.Empty<double>();
        public double[] Point2Z = System.Array.Empty<double>();
        public double[] Angle = System.Array.Empty<double>();
        public double[] Offset1X = System.Array.Empty<double>();
        public double[] Offset2X = System.Array.Empty<double>();
        public double[] Offset1Y = System.Array.Empty<double>();
        public double[] Offset2Y = System.Array.Empty<double>();
        public double[] Offset1Z = System.Array.Empty<double>();
        public double[] Offset2Z = System.Array.Empty<double>();
        public int[] CardinalPoint = System.Array.Empty<int>();
        public string CoordinateSystem = "Global";

        // Areas
        public int NumberAreas;
        public string[] AreaName = System.Array.Empty<string>();
        public int NumberBoundaryPts;
        public int[] PointDelimiter = System.Array.Empty<int>();
        public string[] PointNames = System.Array.Empty<string>();
        public double[] PointX = System.Array.Empty<double>();
        public double[] PointY = System.Array.Empty<double>();
        public double[] PointZ = System.Array.Empty<double>();
        public eAreaDesignOrientation[]? TypeAreaArray;

        // LoadPatterns
        public int NumberLoadPattern;
        public string[] NameLoadPattern = System.Array.Empty<string>();

        // Frame Properties
        public int NumberFrameProps;
        public string[] NameFrameProperties = System.Array.Empty<string>();
        public eFramePropType[] PropType = System.Array.Empty<eFramePropType>();
        public double[] t3 = System.Array.Empty<double>();
        public double[] t2 = System.Array.Empty<double>();
        public double[] tf = System.Array.Empty<double>();
        public double[] tw = System.Array.Empty<double>();
        public double[] t2b = System.Array.Empty<double>();
        public double[] tfb = System.Array.Empty<double>();

        
    }
}
