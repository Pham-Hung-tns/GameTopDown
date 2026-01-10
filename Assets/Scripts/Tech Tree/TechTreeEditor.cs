using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TechTree))]
public class TechTreeEditor : Editor
{
    // positioning
    Vector2 nodeSize = new Vector2(100f,70f);
    float minTreeHeight = 720f;
    float minTreeWidth = 1000f;
    Vector2 incomingEdgVec = new Vector2(100f, 10f);
    Vector2 outgoingEdgVec = new Vector2(-12f, 10f);
    Vector2 upArrowVec = new Vector2(-10f,-10f);
    Vector2 downArrowVec = new Vector2(-10f, 10f);
    Vector2 nextLineVec = new Vector2(0f, 20f);
    Vector2 indentVec = new Vector2(102f, 0f);
    Vector2 nodeContentSize = new Vector2(40f, 20f);
    Vector2 nodeLabelSize = new Vector2(100f, 20f);

    // scrolling and moving
    Vector2 mouseSelectionOffset;
    Vector2 scrollPosition = Vector2.zero;
    Vector2 scrollStartPos;

    TechNode activeNode;
    TechNode selectedNode;

    public override void OnInspectorGUI()
    {
        
        TechTree targetTree = (TechTree)target;

        // Mouse Events
        Event currentEvent = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        EventType UIEvent = currentEvent.GetTypeForControl(controlID);

        // Node styles
        GUIStyle nodeStyle = new GUIStyle(EditorStyles.helpBox);
        GUIStyle selectedNodeStyle = new GUIStyle(EditorStyles.helpBox);
        selectedNodeStyle.fontStyle = FontStyle.BoldAndItalic;

        // The techtree view
        EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.MinHeight(720));

        for(int nodeIdx = 0; nodeIdx < targetTree.tree.Count; nodeIdx++)
        {
            if (targetTree.tree[nodeIdx] == null) continue;
            // Draw node
            Rect nodeRect = new Rect(targetTree.tree[nodeIdx].UIposition - scrollPosition, nodeSize);
            string techName = targetTree.tree[nodeIdx].tech != null ? targetTree.tree[nodeIdx].tech.name : "NULL TECH";
            EditorGUI.BeginFoldoutHeaderGroup(nodeRect, true, techName, (selectedNode==targetTree.tree[nodeIdx]? selectedNodeStyle : nodeStyle));
            
            if (targetTree.tree[nodeIdx].tech == null)
            {
                EditorGUI.EndFoldoutHeaderGroup();
                continue;
            }
            
            EditorGUI.LabelField(new Rect(targetTree.tree[nodeIdx].UIposition - scrollPosition + nextLineVec, nodeLabelSize), "Research cost: ");

            targetTree.tree[nodeIdx].researchCost = EditorGUI.IntField(new Rect(targetTree.tree[nodeIdx].UIposition - scrollPosition + nextLineVec + indentVec, nodeContentSize), targetTree.tree[nodeIdx].researchCost);
            EditorGUI.LabelField(new Rect(targetTree.tree[nodeIdx].UIposition - scrollPosition + nextLineVec*2, nodeLabelSize),"Invested");

            targetTree.tree[nodeIdx].researchInvested = EditorGUI.IntField(new Rect(targetTree.tree[nodeIdx].UIposition - scrollPosition + nextLineVec * 2 + indentVec, nodeContentSize), targetTree.tree[nodeIdx].researchInvested);

            EditorGUI.EndFoldoutHeaderGroup();

            foreach(Tech req in targetTree.tree[nodeIdx].requirements)
            {
                int reqIdx = targetTree.FindTechIndex(req);
                if(reqIdx != -1)
                {
                    // Draw connecting curve
                    Handles.DrawBezier(targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec,
                        targetTree.tree[reqIdx].UIposition - scrollPosition + incomingEdgVec,
                        targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec + Vector2.left * 100,
                        targetTree.tree[reqIdx].UIposition - scrollPosition + incomingEdgVec + Vector2.right * 100,
                        Color.white
                        , null
                        , 3f);

                    // Draw arrow
                    Handles.DrawLine(targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec, targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec + upArrowVec);
                    Handles.DrawLine(targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec, targetTree.tree[nodeIdx].UIposition - scrollPosition + outgoingEdgVec + downArrowVec);
                }

                else Debug.LogWarning("missing tech " + req.name + " in tech tree!");
            }

            // mouse events
            if (nodeRect.Contains(currentEvent.mousePosition))
            {
                if(UIEvent == EventType.MouseDown)
                {
                    if (currentEvent.button == 0)
                    {
                        activeNode = targetTree.tree[nodeIdx];
                        mouseSelectionOffset = activeNode.UIposition - currentEvent.mousePosition;
                    }
                    else if (currentEvent.button == 1)
                    {
                        selectedNode = targetTree.tree[nodeIdx];
                        Repaint();
                    }
                }
                else
                // Create/Destroy connections
                if (UIEvent == EventType.MouseUp)
                {
                    if(currentEvent.button == 1 && selectedNode != null && selectedNode != targetTree.tree[nodeIdx])
                    {
                        if(targetTree.tree[nodeIdx].requirements.Contains(selectedNode.tech))
                            targetTree.tree[nodeIdx].requirements.Remove(selectedNode.tech);
                        else if(selectedNode.requirements.Contains(targetTree.tree[nodeIdx].tech))
                            selectedNode.requirements.Remove(targetTree.tree[nodeIdx].tech);
                        else
                        if(targetTree.IsConnectible( targetTree.tree.IndexOf(selectedNode), nodeIdx))
                        {

                            targetTree.tree[nodeIdx].requirements.Add(selectedNode.tech);

                            for(int k=0; k < targetTree.tree.Count; k++)
                                targetTree.CorrectRequirementsCascades(k);
                        }
                    }
                }
            }
        }

        // Scroll in the Tech Tree view
        if(currentEvent.button == 2)
        {
            if(currentEvent.type == EventType.MouseDown)
            {
                scrollStartPos = currentEvent.mousePosition + scrollPosition;
            }
            else if(currentEvent.type == EventType.MouseDrag)
            {
                scrollPosition = -(currentEvent.mousePosition - scrollStartPos);
                Repaint();
            }
        }

        if(selectedNode != null && currentEvent.button == 1)
        {
            Handles.DrawBezier(currentEvent.mousePosition,
                selectedNode.UIposition - scrollPosition + incomingEdgVec,
                currentEvent.mousePosition + Vector2.left * 100,
                selectedNode.UIposition - scrollPosition + incomingEdgVec + Vector2.right * 100,
                Color.white,
                null,
                1.5f);
            Repaint();
        }

        // Move nodes with left mouse button
        if(UIEvent == EventType.MouseUp)
        {
            activeNode = null;
        }
        else if (UIEvent == EventType.MouseDrag)
        {
            if( activeNode != null)
                activeNode.UIposition = currentEvent.mousePosition + mouseSelectionOffset;
        }

        // Import new Tech
        if(currentEvent.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }
        else if (currentEvent.type == EventType.DragPerform)
        {
            for (int i = 0; i< DragAndDrop.objectReferences.Length; i++)
            {
                if( DragAndDrop.objectReferences[i] is Tech)
                {
                    targetTree.AddNode(DragAndDrop.objectReferences[i] as Tech, currentEvent.mousePosition + scrollPosition);
                }
            }
        }
        EditorGUILayout.EndScrollView();

        scrollPosition.x = GUILayout.HorizontalScrollbar(scrollPosition.x, 20f, 0f, minTreeWidth);
        scrollPosition.y = GUI.VerticalScrollbar(new Rect(0,0,20,720),scrollPosition.y, 20f, 0f, minTreeHeight);

        EditorGUILayout.BeginHorizontal();
        if(selectedNode == null || selectedNode.tech == null)
        {
            EditorGUILayout.LabelField("No tech selected");
        }
        else
        {
            EditorGUILayout.LabelField("Selected Tech: " + selectedNode.tech.name);
            if(GUILayout.Button("Delete Tech"))
            {
                targetTree.DeleteNode(selectedNode.tech);
                if(activeNode == selectedNode) activeNode = null;
                selectedNode = null;
            }
        }

        EditorGUILayout.EndHorizontal();
        EditorUtility.SetDirty(targetTree);
    }
}