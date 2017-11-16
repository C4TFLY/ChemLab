using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStorage : MonoBehaviour {

    public Shader outlinedShader;
    public Shader defaultShader;

    public static Shader OutlinedShader { get; private set; }
    public static Shader DefaultShader { get; private set; }

    private void Awake()
    {
        OutlinedShader = outlinedShader;
        DefaultShader = defaultShader;
    }

}
