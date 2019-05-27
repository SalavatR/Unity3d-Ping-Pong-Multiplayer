using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraView : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void OnValidate()
    {
        if (!cam) cam = GetComponent<Camera>();
    }

    //todo: process perspective camera
    public void SetCameraSize(Vector2 fieldSize)
    {
        if (!cam.orthographic)
        {
            throw new NotImplementedException("Not implemented for perspective projection");
        }

        if (Screen.width / (float) Screen.height <= fieldSize.x / fieldSize.y)
            cam.orthographicSize = (fieldSize.x * Screen.height) / (Screen.width * 2f);
        else
        {
            cam.orthographicSize = (fieldSize.y / 2f);
        }
    }
}
