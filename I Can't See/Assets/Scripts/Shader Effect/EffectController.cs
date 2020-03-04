using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EffectController : MonoBehaviour
{

    [SerializeField]
    private Material effectMaterial;
    private Camera mainCam;

    //Max number of effects to update at once - forced to <25,
    //as this is the size of the array within the shader which has
    //to be hard coded
    [Range(0,25)]
    [SerializeField]
    private volatile int maxEffectUpdates = 20;

    //Effect Points
    public static List<EffectPoint> ePoints;


    // Start is called before the first frame update
    void OnEnable()
    {
        mainCam = GetComponent<Camera>();
        mainCam.depthTextureMode = DepthTextureMode.Depth;

        ePoints = new List<EffectPoint>();
    }


    /// <summary>
    /// Gets the x number of closest effect points (unsorted), if there are less than the desired number
    /// the function will return all of the points in the scene in a smaller array
    /// </summary>
    /// <param name="numOfPoints">Number of points to get</param>
    /// <returns></returns>
    private EffectPoint[] GetClosestEffectPoints(int numOfPoints)
    {

        //List of all points and their distances to the camera / VR Rig
        Dictionary<EffectPoint, float> pointDistPairs = new Dictionary<EffectPoint, float>();

        //Loop and add distances with objects to list
        foreach(EffectPoint point in ePoints)
        {
            //Closest Point to the Camera
            float dist = Vector3.Distance(point.transform.position, mainCam.transform.position);

            //Add a new value to the dictonary if we haven't filled out the max num of values,
            //otherwise if the value is less than (i.e closer) than the current furthest object
            //replace it in the dictonary
            if(pointDistPairs.Count() < numOfPoints)
            {
                //Add new value to array
                pointDistPairs.Add(point, dist);

            }else if (dist < pointDistPairs.Values.Max())
            {
                //Remove old max from the dict and add this new one
                EffectPoint keyOfMaxVal = pointDistPairs.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                pointDistPairs.Remove(keyOfMaxVal);
                pointDistPairs.Add(point, dist);
            }
            
        }

        //Convert to an array
        EffectPoint[] closestPoints = pointDistPairs.Keys.ToArray();

        return closestPoints;
    } 

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        //Get Array of closest items and convert in to arrays to send to the shader
        EffectPoint[] closestPoints = GetClosestEffectPoints(maxEffectUpdates);

        //DO NOT CHANGE THE LENGTH OF THESE ARRAYS EVEN IF YOU THINK IT
        //WILL MAKE THE PROGRAM QUICKER. UNITY SHADERS ARE NOT HAPPY WHEN THE LENGTHS
        //OF ARRAYS ARE CHANGED AT RUN TIME
        //THIS SYSTEM IS OKAY WITH NULL VALUES IN THE ARRAY

        Vector4[] positions = new Vector4[maxEffectUpdates];
        float[] distances = new float[maxEffectUpdates];
        float[] widths = new float[maxEffectUpdates];
        for (int i = 0; i < closestPoints.Length; i++)
        {
            positions[i] = closestPoints[i].transform.position;
            distances[i] = closestPoints[i].ScanDistance;
            widths[i] = closestPoints[i].ScanWidth;
        }

        //Send info to the shader
        effectMaterial.SetFloat("_NumOfEffectUpdates", maxEffectUpdates);
        effectMaterial.SetVectorArray("_WorldSpaceScannerPos", positions);
        effectMaterial.SetFloatArray("_ScanDistance", distances);
        effectMaterial.SetFloatArray("_ScanWidth", widths);

        
        RaycastCornerBlit(src, dst, effectMaterial);
    }

    void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
    {
        // Compute Frustum Corners
        float camFar = mainCam.farClipPlane;
        float camFov = mainCam.fieldOfView;
        float camAspect = mainCam.aspect;

        float fovWHalf = camFov * 0.5f;

        Vector3 toRight = mainCam.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
        Vector3 toTop = mainCam.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        Vector3 topLeft = (mainCam.transform.forward - toRight + toTop);
        float camScale = topLeft.magnitude * camFar;

        topLeft.Normalize();
        topLeft *= camScale;

        Vector3 topRight = (mainCam.transform.forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        Vector3 bottomRight = (mainCam.transform.forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        Vector3 bottomLeft = (mainCam.transform.forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        // Custom Blit, encoding Frustum Corners as additional Texture Coordinates
        RenderTexture.active = dest;

        mat.SetTexture("_MainTex", source);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.MultiTexCoord2(0, 0.0f, 0.0f);
        GL.MultiTexCoord(1, bottomLeft);
        GL.Vertex3(0.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 0.0f);
        GL.MultiTexCoord(1, bottomRight);
        GL.Vertex3(1.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 1.0f);
        GL.MultiTexCoord(1, topRight);
        GL.Vertex3(1.0f, 1.0f, 0.0f);

        GL.MultiTexCoord2(0, 0.0f, 1.0f);
        GL.MultiTexCoord(1, topLeft);
        GL.Vertex3(0.0f, 1.0f, 0.0f);

        GL.End();
        GL.PopMatrix();
    }
}

//Lewis Hammond