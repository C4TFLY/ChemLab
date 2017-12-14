//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(FloatingController))]
//[CanEditMultipleObjects]
//public class FloatingControllerEditor : Editor
//{

//    float minMoveSpeed;
//    float maxMoveSpeed;
//    FloatingController floatingController;

//    private void OnEnable()
//    {
//        minMoveSpeed = serializedObject.FindProperty("minMoveSpeed").floatValue;
//        maxMoveSpeed = serializedObject.FindProperty("maxMoveSpeed").floatValue;
//        floatingController = (FloatingController)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        serializedObject.Update();

//        EditorGUILayout.MinMaxSlider(ref minMoveSpeed, ref maxMoveSpeed, 0.1f, 1f);

//        floatingController.minMoveSpeed = minMoveSpeed;
//        floatingController.maxMoveSpeed = maxMoveSpeed;
//    }

//}
