using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave {

    static AutoSave()
    {
        EditorApplication.playmodeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged()
    {
        if (EditorApplication.isPlaying == false)
        {
            EditorSceneManager.SaveOpenScenes();
        }
    }

}
