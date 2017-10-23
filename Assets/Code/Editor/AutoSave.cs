using System;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave {

    static AutoSave()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (EditorApplication.isPlaying == false)
        {
            EditorSceneManager.SaveOpenScenes();
        }
    }

}
