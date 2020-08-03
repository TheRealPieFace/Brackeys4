﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator creator;
    Path path;

    private void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        RaycastHit hit = new RaycastHit();
        
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if(guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Undo.RecordObject(creator, "Add segment");
                path.AddSegment(new Vector3(hit.point.x, path.GetPointsInSegment(0)[0].y, hit.point.z));
            }
            
            
        }
    }

    void Draw()
    {
        for(int i = 0; i < path.NumSegments; i++)
        {
            Vector3[] points = path.GetPointsInSegment(i);
            Handles.color = Color.white;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
        }

        Handles.color = Color.red;
        for(int i = 0; i < path.NumPoints; i++)
        {
            Vector3 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f, Vector3.zero, Handles.SphereHandleCap);
            if(path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, new Vector3(newPos.x, path[i].y, newPos.z));
            }
        }
    }

    private void OnEnable()
    {
        creator = (PathCreator)target;
        if(creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }
}
