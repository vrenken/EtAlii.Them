﻿// ReSharper disable All
using Tessera;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


// This script simply kicks off the generator.
// By default the generator doesn't do anything, which is not useful for a sample
public class GenerateOnStart : MonoBehaviour
{
    void Start()
    {
        Generate();
    }

    public void Clear()
    {
        var multipassGenerator = GetComponent<TesseraMultipassGenerator>();
        var animatedGenerator = GetComponent<AnimatedGenerator>();

        if (multipassGenerator != null)
        {
            multipassGenerator.Clear();
        }
        else if (animatedGenerator != null)
        {
            animatedGenerator.StopGeneration();
        }
        else
        {

            var generators = GetComponents<TesseraGenerator>();

            generators[0].Clear();
        }
    }

    public void Generate()
    { 
        var multipassGenerator = GetComponent<TesseraMultipassGenerator>();
        var animatedGenerator = GetComponent<AnimatedGenerator>();
        if (multipassGenerator != null)
        {
            multipassGenerator.Generate();
        }
        else if (animatedGenerator != null)
        {
            animatedGenerator.StartGeneration();
        }
        else
        {

            var generators = GetComponents<TesseraGenerator>();

            foreach (var generator in generators)
            {
                generator.Generate();
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GenerateOnStart))]
public class GenerateOnStartEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Regenerate"))
        {
            var t = target as GenerateOnStart;
            t.Clear();
            t.Generate();
        }
    }
}
#endif
