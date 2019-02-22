using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SkeletonTracking : MonoBehaviour {

    public string animationName = "m01_s01";
    public float animationSpeed = 5;
    public LineRenderer backboneLineRenderer, wingspanLineRenderer, baseLineRenderer;

    private Transform[] joints;
    private FileReader reader;
    private List<Vector3[]> positionFrames, angleFrames;
    private int currentFrame;

    // Use this for initialization
    void Start () {
        reader = new FileReader();
        this.joints = new Transform[22];
        /*for(int i = 1; i <= 22; i++)
        {
            string joint = "Sphere (" + i + ")";
            joints[i - 1] = transform.Find(joint);
        }*/
        foreach(Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject == gameObject || t.gameObject.name.Contains("Bone")) continue;
            int i = int.Parse(Regex.Replace(t.gameObject.name, "[^0-9]", ""));
            joints[i - 1] = t;
        }
        StartCoroutine(playAnimation(animationName));
        
	}

    IEnumerator playAnimation(string file)
    {
        positionFrames = reader.Read("Positions/" + file +"_positions");
        angleFrames = reader.Read("Angles/" + file + "_angles");
        currentFrame = 0;
        while (true)
        {
            yield return new WaitForSeconds(1/animationSpeed);
            if (currentFrame >= positionFrames.Count)
            {
                currentFrame = 0;
            }
            // Force final positions
            for (int i = 0; i < 22; i++)
            {
                joints[i].localPosition = positionFrames[currentFrame][i];
                joints[i].localEulerAngles = angleFrames[currentFrame][i];
            }
            currentFrame++;
        }
    }

    void Update()
    {
        // Draw skeleton
        /*Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);*/
        // Head
        //DrawBone(joints[6-1], joints[5-1]);
        backboneLineRenderer.numPositions = 6;
        backboneLineRenderer.SetPositions(new Vector3[] {
            joints[Joints.Waist].position,
            joints[Joints.Spine].position,
            joints[Joints.Chest].position,
            joints[Joints.Neck].position,
            joints[Joints.Head].position,
            joints[Joints.HeadTip].position,
        });
        wingspanLineRenderer.numPositions = 11;
        wingspanLineRenderer.SetPositions(new Vector3[] {
            joints[Joints.RightHand].position + joints[Joints.RightHand].forward * 40,
            joints[Joints.RightHand].position,
            joints[Joints.RightForeArm].position,
            joints[Joints.RightUpperArm].position,
            joints[Joints.RightCollar].position,
            joints[Joints.Neck].position,
            joints[Joints.LeftCollar].position,
            joints[Joints.LeftUpperArm].position,
            joints[Joints.LeftForeArm].position,
            joints[Joints.LeftHand].position,
            joints[Joints.LeftHand].position + joints[Joints.LeftHand].forward * 40,
        });
        baseLineRenderer.numPositions = 9;
        baseLineRenderer.SetPositions(new Vector3[] {
            joints[Joints.RightLegToes].position,
            joints[Joints.RightFoot].position,
            joints[Joints.RightLowerLeg].position,
            joints[Joints.RightUpperLeg].position,
            joints[Joints.Waist].position,
            joints[Joints.LeftUpperLeg].position,
            joints[Joints.LeftLowerLeg].position,
            joints[Joints.LeftFoot].position,
            joints[Joints.LeftLegToes].position,
        });
    }

    public void OnPlay(Text t)
    {
        StopAllCoroutines();
        animationName = t.text;
        StartCoroutine(playAnimation(animationName));
    }
}

public struct Joints
{
    public static int Waist = 0;
    public static int Spine = 1;
    public static int Chest = 2;
    public static int Neck = 3;
    public static int Head = 4;
    public static int HeadTip = 5;
    public static int RightCollar = 10;
    public static int RightUpperArm = 11;
    public static int RightForeArm = 12;
    public static int RightHand = 13;
    public static int LeftCollar = 6;
    public static int LeftUpperArm = 7;
    public static int LeftForeArm = 8;
    public static int LeftHand = 9;
    public static int RightUpperLeg = 18;
    public static int RightLowerLeg = 19;
    public static int RightFoot = 20;
    public static int RightLegToes = 21;
    public static int LeftUpperLeg = 14;
    public static int LeftLowerLeg = 15;
    public static int LeftFoot = 16;
    public static int LeftLegToes = 17;
}