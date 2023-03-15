using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    [SerializeField]
    int ArmLength;

    [SerializeField]
    Transform target;

    Transform[] Bones;

    Vector3[] Positions;

    float[] BoneLengths;

    float fullLength;

    // Rotation
    Vector3[] StartDirections;
    Quaternion[] StartRotations;
    Quaternion TargetStartRotation;

    int iterations = 10;
    float accuracy = 0.01f;

    private void Start()
    {
        InitializeArm();
    }

    private void InitializeArm()
    {
        Bones = new Transform[ArmLength + 1];
        Positions = new Vector3[ArmLength + 1];

        StartDirections = new Vector3[ArmLength + 1];
        StartRotations = new Quaternion[ArmLength + 1];

        TargetStartRotation = target.rotation;

        BoneLengths = new float[ArmLength];

        fullLength = 0;

        Transform cur = transform;
        for(int i = Bones.Length -1; i >= 0; i--)
        {
            Bones[i] = cur;
            StartRotations[i] = cur.rotation;

            if(i == Bones.Length - 1)
            {
                StartDirections[i] = target.position - cur.position;
            }
            else
            {
                BoneLengths[i] = (Bones[i + 1].position - cur.position).magnitude;
                fullLength += BoneLengths[i];

                StartDirections[i] = Bones[i+1].position - cur.position;
            }

            cur = cur.parent;
        }
        ArmLength = Bones.Length-1;
    }

    private void Update()
    {
        ResolveIK();
        //Debug.Log(Bones.Length);
        //Debug.Log(Positions.Length);
    }

    private void ResolveIK()
    {
        if(BoneLengths.Length != ArmLength)
        {
            InitializeArm();
        }

        // Get the positions of the bones and store it
        for(int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }


        float sqrDistanceToTarget = (target.position - Bones[0].position).sqrMagnitude; // Distance

        if(sqrDistanceToTarget >= fullLength * fullLength) // There is a superior distance than the full length of the arm
        {
            Vector3 dir = (target.position - Positions[0]).normalized;

            for(int i = 1; i < Positions.Length; i++)
            {
                //Positions[i] = new Vector3((Positions[i-1] + dir * BoneLengths[i-1]).x, Positions[i].y, (Positions[i - 1] + dir * BoneLengths[i - 1]).z);
                Positions[i] = Positions[i - 1] + dir * BoneLengths[i - 1];
            }

            Debug.Log("Superior distance");
        }
        else // Inferior distance than the full length of the arm
        {
            Debug.Log("Inferior distance");

            for(int iteration = 0; iteration < iterations; iteration++){
                // Back
                for(int i = Positions.Length-1; i > 0; i--)
                {
                    if(i == Positions.Length - 1)
                    {
                        Positions[i] = target.position;
                    }
                    else
                    {
                        Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BoneLengths[i];
                    }
                }
                // Front
                for(int i = 1; i < Positions.Length; i++)
                {
                    Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i-1]).normalized*BoneLengths[i-1];
                }

                float sqDistance = (Positions[Positions.Length-1] - target.position).sqrMagnitude;
                //Debug.Log(sqDistance);
                //Debug.Log(sqDistance < accuracy * accuracy);
                if (sqDistance < accuracy * accuracy)
                    break;
            }
        }

        
        /*
        for(int i = 0; i < Positions.Length; i++)
        {
            Bones[i].position = Positions[i];
        }

        for (int i = Positions.Length-1; i > 0; i--)
        {
            if(i != 0)
            {
                Bones[i-1].LookAt(Bones[i]);
                Debug.Log(Bones[i-1].name + " look at " + Bones[i].name);
            }
        }
        */
            

            for(int i = 0; i < Positions.Length; i++)
            {
                if( i == Positions.Length - 1)
                {
                    //Bones[i].rotation = target.rotation * Quaternion.Inverse(TargetStartRotation) * StartRotations[i];
                    Bones[i].LookAt(target, Vector3.right);
                    //Debug.Log(Bones[i].rotation);
                }
                else
                {
                    Bones[i].rotation = Quaternion.FromToRotation(StartDirections[i], Positions[i+1]- Positions[i])*StartRotations[i];
                    //Debug.Log("else : " + Bones[i].name);
            }
            }
            
        }

    private void OnDrawGizmos()
    {
        Transform current = this.transform;

        for(int i = 0; i < ArmLength && current != null && current.parent != null; i++)
        {
            Debug.DrawLine(current.position, current.parent.position, Color.green);
            current = current.parent;
        }
    }
}
