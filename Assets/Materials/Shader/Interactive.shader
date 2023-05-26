Shader "3D Shaders/Outline Shader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RampTex("Ramp", 2D) = "white" {}
		_Brightness("Brightness", Range(-.5,.5)) = .5
		_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim Power", Range(0.5, 8.0)) = 3.0

			// Value to determine if outlining is enabled and outline color + size.
			_OutlineColor("Outline Color", Color) = (1,1,1,1)
			_OutlineSize("Outline Size", float) = 0
	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			 Pass
			{
				Cull Front
				ZWrite On
				CGPROGRAM
				#include "UnityCG.cginc"
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma glsl_no_auto_normalization
				#pragma vertex vert
				#pragma fragment frag

				struct appdata_t
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
				};

				fixed _OutlineSize;
				fixed4 _OutlineColor;


				v2f vert(appdata_t v)
				{
					v2f o;
					o.pos = v.vertex;
					o.pos.xyz += normalize(v.normal.xyz) * _OutlineSize * 0.01;
					o.pos = UnityObjectToClipPos(o.pos);
					return o;
				}

				fixed4 frag(v2f i) :COLOR
				{
					return _OutlineColor;
				}

				ENDCG
			}

			CGPROGRAM
					// Physically based Standard lighting model, and enable shadows on all light types
					#pragma surface surf Toon

					// Use shader model 3.0 target, to get nicer looking lighting
					#pragma target 3.0

					sampler2D _MainTex;
					sampler2D _RampTex;
					fixed4 _Color;
					fixed _Brightness;
					fixed4 _RimColor;
					fixed _RimPower;

					half4 LightingToon(SurfaceOutput s, half3 lightDir, half atten)
					{
						half NdotL = dot(s.Normal, lightDir);
						NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));

						fixed4 c;
						c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten;
						c.a = s.Alpha;
						return c;
					}

					struct Input {
						float2 uv_MainTex;
						float3 viewDir;
					};

					void surf(Input IN, inout SurfaceOutput o) {
						fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
						o.Albedo = c.rgb;

						o.Emission = _Brightness;

						half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
						o.Emission += _RimColor.rgb * pow(rim, _RimPower);

						o.Alpha = c.a;
					}
					ENDCG
		}
}