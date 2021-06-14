using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(NpcBehaviorRoutine))]
public class DrawHandle : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        NpcBehaviorRoutine myObj = target as NpcBehaviorRoutine;
        
        Handles.DrawWireArc(myObj.transform.position + new Vector3(0,3,0), myObj.transform.forward, -myObj.transform.right, myObj.routineData.maxSearchAngle, 90/*myObj.searchCollider.radius*/);

        //Handles.color = Color.red;
        //NpcBehaviorRoutine myObj = (NpcBehaviorRoutine)target;
        //Handles.DrawWireArc(myObj.transform.position + new Vector3(0,3,0), myObj.transform.up, -myObj.transform.right, 180, myObj.searchCollider.radius);
    }
}
