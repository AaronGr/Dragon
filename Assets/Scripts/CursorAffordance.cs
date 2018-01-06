﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D debugCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    private CameraRaycaster cameraRayCaster;

	// Use this for initialization
	void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        switch(cameraRayCaster.layerHit) {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(debugCursor, cursorHotspot, CursorMode.Auto);
                return;
        }
	}
}
