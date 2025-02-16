﻿using UnityEngine;
using System.Collections;

/*
USE FOR LOOPS FOR POSTIONS USE OBJECT ATTACHED TO THE CAMERA AND PASS ARRAY OF POSISITIONS TO DO IT
*/
/*

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
	public Transform[] ScannerOrigin;
	public Material EffectMaterial;
	public float ScanDistance;

	private Camera _camera;

	// Demo Code
	bool _scanning;
	//Scannable[] _scannables;

	void Start()
	{
        //_scannables = FindObjectsOfType<Scannable>();
    }

	void Update()
	{
		if (_scanning)
		{
			ScanDistance += Time.deltaTime * 25;
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			_scanning = true;
			ScanDistance = 0;
		}

		if (Input.GetMouseButtonDown(0))
		{
			//Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			//RaycastHit hit;

			//if (Physics.Raycast(ray, out hit))
			//{
			
				_scanning = true;
				ScanDistance = 0;
				//ScannerOrigin.position = ScannerOrigin.position;
				
			//}
		}
	}
	// End Demo Code

	void OnEnable()
	{
        _camera = Camera.main;
        _camera.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Vector4[] list = new Vector4[9];
		for(int i = 0; i < 9; i++)
		{
			list[i] = ScannerOrigin[i].position;
		}

		float[] distList = new float[9];
		distList[0] = 6;
		distList[1] = 6;
		distList[2] = 6;
		distList[3] = 6;
		distList[4] = 6;
		distList[5] = 6;
		distList[6] = 6;
		distList[7] = 6;
		distList[8] = 6;

		EffectMaterial.SetVectorArray("_WorldSpaceScannerPos", list);
		EffectMaterial.SetFloatArray("_ScanDistance", distList);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
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
*/

	//Lewis Hammond