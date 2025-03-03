using UnityEngine;

public class SimulationControl : MonoBehaviour
{
    [SerializeField]
    Renderer waterRenderer;
    [SerializeField]

    Material flowMaterial;
    [SerializeField]

    Material noFlowMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void controlFlowMaps(bool state)
    {
        if (state)
            waterRenderer.material = flowMaterial;
        else
            waterRenderer.material = noFlowMaterial;
    }
}
