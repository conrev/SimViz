using UnityEngine;
using System;
using System.Collections.Generic;

namespace DroneToolbox
{

    namespace FlightPath
    {

        [Serializable]
        public class DroneData
        {
            List<Frame> frameData;

        }
        class Frame
        {
            // stores the data of a single frame 
            // in drone flight path
            public uint deltaTime;
            public uint frameNumber;
            public double latitude;
            public double longitude;
            public double relativeAltitude;
            public double absoluteAltitude;
        }

    }
}
