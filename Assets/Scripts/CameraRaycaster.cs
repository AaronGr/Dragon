using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    private bool layerChanged = false;

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    public delegate void OnLayerChange(Layer layer); // declare new delegate type
    public event OnLayerChange layerChangeObservers; // instantiate an observer set

    void Start() 
    {
        viewCamera = Camera.main;

    }

    void Update()
    {
   
 
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {

            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                //If a new layer is hit call the delegates
                if (m_layerHit != layer)
                {
                    m_layerHit = layer;
                    layerChangeObservers(m_layerHit); // call the delegates                  
                }
                
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
        layerChangeObservers(m_layerHit); // call the delegates 
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
