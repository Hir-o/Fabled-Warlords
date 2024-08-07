using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "CameraData", menuName = "CameraData/CameraSettings")]

public class CameraData : ScriptableObject
{
   [Header("Drag")]
   public bool dragEnabled;
   public float dragSpeed = -0.06f;
   public TwoDCameraDrag.MouseButton mouseButton;
   [Header("Edge Scroll")] 
   public bool enableEdgeScroll;
   public int edgeBoundary = 20;
   [Range(0, 10)]
   [Tooltip("Speed the camera moves Mouse enters screen edge.")]
   public float edgeSpeed = 1f;
   [Header("Keyboard Input")]
   public bool keyboardInput;
   public bool inverseKeyboard;
   [Header("Zoom")]
   public bool zoomEnabled;
   public float maxZoom;
   public float minZoom;
   public float zoomStepSize; 
   public bool linkedZoomDrag;
   public bool zoomToMouse;
   [Header("Zoom")] 
   public bool clampCamera;



}
