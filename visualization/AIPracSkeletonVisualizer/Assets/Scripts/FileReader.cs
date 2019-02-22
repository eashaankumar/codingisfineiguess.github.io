using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class FileReader {

    public List<Vector3[]> Read(string path)
    {
        //StreamReader reader = new StreamReader("Assets/Resources/Positions/m01_s01_positions.txt");
        TextAsset file = Resources.Load(path) as TextAsset;
        List<Vector3[]> frames = new List<Vector3[]>();

        string[] fLines = Regex.Split (file.text.Replace("  ", " "), "\n|\r|\r\n" );
        /*while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] parts = line.Replace("  ", ",").Split(',');
            Vector3[] frame = new Vector3[22];
            int i = 1; // ignore first
            if (parts.Length > 66) i = 1;
            for(int joint = 0; joint < 22; joint++)
            {
                //Debug.Log(joint + " " + i);
                frame[joint] = new Vector3(float.Parse(parts[i]), float.Parse(parts[i + 1]), float.Parse(parts[i + 2]));
                i += 3;
            }
            frames.Add(frame);
        }
        reader.Close();
        */
        foreach (string line in file.text.Split('\n'))
        {
            if (line.Length == 0) continue;
            string[] parts = line.Replace("  ", ",").Split(',');
            Vector3[] frame = new Vector3[22];
            int i = 1; // ignore first
            if (parts.Length > 66) i = 1;
            for (int joint = 0; joint < 22; joint++)
            {
                //Debug.Log(joint + " " + i);
                frame[joint] = new Vector3(float.Parse(parts[i]), float.Parse(parts[i + 1]), float.Parse(parts[i + 2]));
                i += 3;
            }
            frames.Add(frame);
        }
        return frames;
    }
}
