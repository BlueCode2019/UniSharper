Shader "UniSharper/Rim Light" {
	Properties {
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RimLightColor("Rim Light Color", Color) = (1, 1, 1, 1)
		_RimLightIntensity("Rim Light Intensity", Range(0.000001, 3.0)) = 0.1
	}

	SubShader {
		Pass {
			Tags = { "RenderType" = "Opaque" }

			CGPROGRAM
			#include "Lighting.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			fixed4 _RimLightColor;
			float _RimLightIntensity;

			struct v2f {
				float4 pos:
			}

			ENDCG
		}
	}

	FallBack "Diffuse"
}