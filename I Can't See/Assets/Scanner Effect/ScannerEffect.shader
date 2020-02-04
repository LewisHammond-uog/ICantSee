
Shader "Hidden/ScannerEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DetailTex("Texture", 2D) = "white" {}
		//_ScanDistance("Scan Distance", float) = 0
		//_ScanWidth("Scan Width", float) = 20
		_LeadSharp("Leading Edge Sharpness", float) = 10
		_LeadColor("Leading Edge Color", Color) = (1, 1, 1, 0)
		_MidColor("Mid Color", Color) = (1, 1, 1, 0)
		_TrailColor("Trail Color", Color) = (1, 1, 1, 0)
		_HBarColor("Horizontal Bar Color", Color) = (0.5, 0.5, 0.5, 0)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct VertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 ray : TEXCOORD1;
			};

			struct VertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_depth : TEXCOORD1;
				float4 interpolatedRay : TEXCOORD2;
			};

			float4 _MainTex_TexelSize;
			float4 _CameraWS;

			VertOut vert(VertIn v)
			{
				VertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;
				o.uv_depth = v.uv.xy;

				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1 - o.uv.y;
				#endif				

				o.interpolatedRay = v.ray;

				return o;
			}

			sampler2D _MainTex;
			sampler2D _DetailTex;
			sampler2D_float _CameraDepthTexture;
			float _NumOfEffectUpdates;
			float4 _WorldSpaceScannerPos[25];
			float _ScanDistance[25];
			float _ScanWidth[25];
			float _LeadSharp;
			float4 _LeadColor;
			float4 _MidColor;
			float4 _TrailColor;
			float4 _HBarColor;

			
			float4 horizBars(float2 p)
			{
				return 1 - saturate(round(abs(frac(p.x * 100) * 2)));
			}

			float4 horizTex(float2 p)
			{
				return tex2D(_DetailTex, float2(p.x * 30, p.y * 40));
			}

			float test() {
				for (int i = 0; i < 100;i++) {

				}
			}

			half4 calcScannerCol(float a_rawDepth, float a_linearDepth, float4 a_wsDir, float3 a_wsPos, half4 a_scannerCol, VertOut i) {

				for (int j = 0; j < _NumOfEffectUpdates; j++) {

					float dist = distance(a_wsPos, _WorldSpaceScannerPos[j]);

					if (dist < _ScanDistance[j] && dist > _ScanDistance[j] - _ScanWidth[j] && a_linearDepth < 1)
					{
						float diff = 1 - (_ScanDistance[j] - dist) / (_ScanWidth[j]);
						half4 edge = lerp(_MidColor, _LeadColor, pow(diff, _LeadSharp));
						a_scannerCol = lerp(_TrailColor, edge, diff) + horizBars(i.uv) * _HBarColor;
						a_scannerCol *= diff;
					}
				}

				return a_scannerCol;

			}

			half4 frag (VertOut i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.uv);

				float rawDepth = DecodeFloatRG(tex2D(_CameraDepthTexture, i.uv_depth));
				float linearDepth = Linear01Depth(rawDepth);
				float4 wsDir = linearDepth * i.interpolatedRay;
				float3 wsPos = _WorldSpaceCameraPos + wsDir;
				half4 scannerCol = half4(0, 0, 0, 0);

				scannerCol = calcScannerCol(rawDepth, linearDepth, wsDir, wsPos, scannerCol, i);

				return col + scannerCol;
			}
			ENDCG
		}
	}
}

//Lewis Hammond - Modified from "Makin' Stuff Look Good" (https://www.youtube.com/watch?v=OKoNp2RqE9A)