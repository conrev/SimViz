using System;
using System.IO;
using UnityEngine;
using DroneToolbox.FlightPath;
using System.Text.RegularExpressions;

namespace DroneToolbox
{
    public class DroneFileReader
    {
        public static DroneData ReadSRT(string filepath)
        {
            // read srt files on the streaming assets folder
            // assuming this format:

            // 1
            // 00:00:00,000 --> 00:00:00,033
            // <font size="28">FrameCnt: 1, DiffTime: 33ms
            // 2025-02-04 15:25:13.561
            // [iso: 100] [shutter: 1/2500.0] [fnum: 1.7] [ev: 0] [color_md: default] [focal_len: 24.00] [latitude: -38.074966] [longitude: 141.845763] [rel_alt: 40.800 abs_alt: 111.639] [ct: 5256] </font>

            string fullPath = Application.streamingAssetsPath + "\\" + filepath;
            using (StreamReader reader = new StreamReader(fullPath))
            {
                int frameNum = 0;
                while (reader.Peek() >= 0)
                {
                    Frame frame = new Frame();
                    String line;

                    while ((line = reader.ReadLine()).Trim() != "")
                    {

                        var match = Regex.Match(line, @"\[([lL]atitude:.*?([+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)))\]");
                        if (match.Success)
                            frame.latitude = double.Parse(match.Groups[2].Value);
                    }

                    Debug.Log("FrameData " + frameNum + ": {latitude: " + frame.latitude + "}");
                    frameNum++;
                }

                return new DroneData();
            }
        }
    }

}
