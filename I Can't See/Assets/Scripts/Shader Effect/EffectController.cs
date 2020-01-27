using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EffectController : MonoBehaviour
{

    [SerializeField]
    private Material effectMaterial;
    private Camera mainCam;

    //Effect Points
    public static List<EffectPoint> ePoints;
    

    [SerializeField]
    private 

    // Start is called before the first frame update
    void OnEnable()
    {
        mainCam = GetComponent<Camera>();
        mainCam.depthTextureMode = DepthTextureMode.Depth;

        ePoints = new List<EffectPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //effectMaterial.SetVectorArray
    }
}
