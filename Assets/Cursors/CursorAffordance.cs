using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D defaultCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    private CameraRaycaster cameraRayCaster;

	// Use this for initialization
	void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
        cameraRayCaster.notifyLayerChangeObservers += OnLayerChange;
	}

    void OnLayerChange(int newLayer)
    {
        switch(newLayer)
        {
            case (int)CameraRaycaster.Layers.EnemyLayer:
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                break;

            case (int)CameraRaycaster.Layers.WalkableLayer:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;

            case (int)CameraRaycaster.Layers.UILayer:
                Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
                break;

            case (int)CameraRaycaster.Layers.DefaultLayer:
                Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
                break;

            default:
                Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
                return;
        }
    }
	
}
