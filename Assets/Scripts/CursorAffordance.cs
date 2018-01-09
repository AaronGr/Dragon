using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D debugCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    private CameraRaycaster cameraRayCaster;

	// Use this for initialization
	void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
        cameraRayCaster.layerChangeObservers += OnLayerChange;
	}
	
	void OnLayerChange (Layer layer) {
        switch(layer) {
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
