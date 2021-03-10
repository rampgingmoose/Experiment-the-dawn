// Made with Amplify Shader Editor
Shader "SterlexDev/DissolveShader"
{Properties
	{
		_Fill("Fill", Float) = 1
		_ValueFill("", Int) = 1
		_Cutoff("Mask Clip Value", Float) = 0.5
		[HDR]_Borderwidth("Border width", Float) = 0
		[HDR]_Bordercolor("Border color", Color) = (0,0,0,0)
		[HDR]_Fillcolor("Fill color", Color) = (1,0.5514706,0.5514706,1)
		_Noisescale("Noise scale", Range(0 , 20)) = 0
		_Noisespeed("Noise speed", Vector) = (0,0,0,0)
		[Toggle]_Worldcoordinates("World coordinates", Float) = 0
		[Toggle]_Invertmask("Invert mask", Float) = 0	
		_Wave1amplitude("Wave1 amplitude", Range(0 , 1)) = 0
		_Wave1frequency("Wave1 frequency", Range(0 , 50)) = 0
		_Wave2amplitude("Wave2 amplitude", Range(0 , 1)) = 0
		_Wave2Frequency("Wave2 Frequency", Range(0 , 50)) = 0
		_Maintiling("Maintiling", Vector) = (1,1,0,0)
		_Albedo("Albedo", 2D) = "black" {}
		_Normal("Normal", 2D) = "bump" {}
		_Specular("Specular", 2D) = "black" {}
		_Smoothness("Smoothness", Range(0 , 1)) = 0
		_Occlusion("Occlusion", 2D) = "white" {}
		_Bordertexture("Border texture", 2D) = "white" {}
		[HideInInspector] _texcoord("", 2D) = "white" {}
		[HideInInspector] __dirty("", Int) = 1
}

SubShader
		{
			Tags{ "RenderType" = "Transparent"  "Queue" = "Background+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
			Cull Off
			CGINCLUDE
			#include "UnityShaderVariables.cginc"
			#include "UnityPBSLighting.cginc"
			#include "Lighting.cginc"
			#pragma target 3.0
			#ifdef UNITY_PASS_SHADOWCASTER
				#undef INTERNAL_DATA
				#undef WorldReflectionVector
				#undef WorldNormalVector
				#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
				#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
				#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
			#endif
			struct Input
			{
				float2 uv_texcoord;
				float3 worldPos;
				float3 worldNormal;
				INTERNAL_DATA
				half ASEVFace : VFACE;
			};

			uniform sampler2D _Normal;
			uniform float2 _Maintiling;
			uniform float2 _Mainoffset;
			uniform sampler2D _Albedo;
			uniform float _Enablerimlight;
			uniform float4 _Rimlightcolor;
			uniform float _Rimlightbias;
			uniform float _Rimlightscale;
			uniform float _Rimlightpower;
			uniform float _Borderwidth;
			uniform float _Wave1amplitude;
			uniform float _Noisescale;
			uniform float _Worldcoordinates;
			uniform float3 _Noisespeed;
			uniform float _Layernoise;
			uniform float _Wave1frequency;
			uniform float _Wave1offset;
			uniform float _Wave2Frequency;
			uniform float _Wave2offset;
			uniform float _Wave2amplitude;
			uniform float _Fill;
			uniform float _Invertmask;
			uniform float4 _Bordercolor;
			uniform sampler2D _Bordertexture;
			uniform float4 _Bordertexture_ST;
			uniform float _Activateemission;
			uniform float4 _Basecolor;
			uniform sampler2D _Emission;
			uniform float2 _Emissiontexspeed;
			uniform float2 _Emissiontextiling;
			uniform sampler2D _Secondaryemissionnoise;
			uniform float2 _Noisetexspeed;
			uniform float2 _Noisetextiling;
			uniform float _SecondaryEmissionNoiseDesaturation;
			uniform sampler2D _Secondaryemission;
			uniform float2 _Secondaryemissionspeed;
			uniform float2 _Secondaryemissiontiling;
			uniform float _SecondaryEmissionDesaturation;
			uniform float4 _Secondaryemissioncolor;
			uniform float _Noisetexopacity;
			uniform float _Activatesecondaryemission;
			uniform float _Tintinsidecolor;
			uniform float4 _Fillcolor;
			uniform sampler2D _Specular;
			uniform float _Smoothness;
			uniform sampler2D _Occlusion;
			uniform float _Cutoff = 0.5;


			float3 mod3D289(float3 x) { return x - floor(x / 289.0) * 289.0; }

			float4 mod3D289(float4 x) { return x - floor(x / 289.0) * 289.0; }

			float4 permute(float4 x) { return mod3D289((x * 34.0 + 1.0) * x); }

			float4 taylorInvSqrt(float4 r) { return 1.79284291400159 - r * 0.85373472095314; }

			float snoise(float3 v)
			{
				const float2 C = float2(1.0 / 6.0, 1.0 / 3.0);
				float3 i = floor(v + dot(v, C.yyy));
				float3 x0 = v - i + dot(i, C.xxx);
				float3 g = step(x0.yzx, x0.xyz);
				float3 l = 1.0 - g;
				float3 i1 = min(g.xyz, l.zxy);
				float3 i2 = max(g.xyz, l.zxy);
				float3 x1 = x0 - i1 + C.xxx;
				float3 x2 = x0 - i2 + C.yyy;
				float3 x3 = x0 - 0.5;
				i = mod3D289(i);
				float4 p = permute(permute(permute(i.z + float4(0.0, i1.z, i2.z, 1.0)) + i.y + float4(0.0, i1.y, i2.y, 1.0)) + i.x + float4(0.0, i1.x, i2.x, 1.0));
				float4 j = p - 49.0 * floor(p / 49.0);  // mod(p,7*7)
				float4 x_ = floor(j / 7.0);
				float4 y_ = floor(j - 7.0 * x_);  // mod(j,N)
				float4 x = (x_ * 2.0 + 0.5) / 7.0 - 1.0;
				float4 y = (y_ * 2.0 + 0.5) / 7.0 - 1.0;
				float4 h = 1.0 - abs(x) - abs(y);
				float4 b0 = float4(x.xy, y.xy);
				float4 b1 = float4(x.zw, y.zw);
				float4 s0 = floor(b0) * 2.0 + 1.0;
				float4 s1 = floor(b1) * 2.0 + 1.0;
				float4 sh = -step(h, 0.0);
				float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
				float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
				float3 g0 = float3(a0.xy, h.x);
				float3 g1 = float3(a0.zw, h.y);
				float3 g2 = float3(a1.xy, h.z);
				float3 g3 = float3(a1.zw, h.w);
				float4 norm = taylorInvSqrt(float4(dot(g0, g0), dot(g1, g1), dot(g2, g2), dot(g3, g3)));
				g0 *= norm.x;
				g1 *= norm.y;
				g2 *= norm.z;
				g3 *= norm.w;
				float4 m = max(0.6 - float4(dot(x0, x0), dot(x1, x1), dot(x2, x2), dot(x3, x3)), 0.0);
				m = m * m;
				m = m * m;
				float4 px = float4(dot(x0, g0), dot(x1, g1), dot(x2, g2), dot(x3, g3));
				return 42.0 * dot(m, px);
			}


			void surf(Input i , inout SurfaceOutputStandardSpecular o)
			{
				float2 uv_TexCoord288 = i.uv_texcoord * _Maintiling + _Mainoffset;
				float2 UVTilingOffset290 = uv_TexCoord288;
				float3 Normal265 = UnpackNormal(tex2D(_Normal, UVTilingOffset290));
				o.Normal = Normal265;
				float4 Albedo262 = tex2D(_Albedo, UVTilingOffset290);
				o.Albedo = Albedo262.rgb;
				float3 ase_worldPos = i.worldPos;
				float3 ase_worldViewDir = normalize(UnityWorldSpaceViewDir(ase_worldPos));
				float3 ase_worldNormal = WorldNormalVector(i, float3(0, 0, 1));
				float fresnelNdotV94 = dot(ase_worldNormal, ase_worldViewDir);
				float fresnelNode94 = (_Rimlightbias + _Rimlightscale * pow(1.0 - fresnelNdotV94, _Rimlightpower));
				float3 ase_vertex3Pos = mul(unity_WorldToObject, float4(i.worldPos , 1));
				float3 Components205 = lerp(ase_vertex3Pos,ase_worldPos,_Worldcoordinates);
				float simplePerlin3D36 = snoise((_Noisescale * (Components205 + (_Noisespeed * _Time.y))));
				float Noise210 = simplePerlin3D36;
				float temp_output_208_0 = (Noise210 * _Layernoise);
				float3 break213 = Components205;
				float temp_output_2_0 = ((_Wave1amplitude * sin((Noise210 + ((temp_output_208_0 + break213.x) * _Wave1frequency + _Wave1offset)))) + break213.y + (sin((((break213.z + temp_output_208_0) * _Wave2Frequency + _Wave2offset) + Noise210)) * _Wave2amplitude));
				float temp_output_10_0 = (-1.0 + (_Fill - 0.0) * (1.0 - -1.0) / (1.0 - 0.0));
				float temp_output_12_0 = step(((0.0 + (max((float)0 , _Borderwidth) - 0.0) * (0.1 - 0.0) / (1.0 - 0.0)) + temp_output_2_0) , temp_output_10_0);
				float temp_output_228_0 = (1.0 - _Invertmask);
				float temp_output_8_0 = step(temp_output_2_0 , temp_output_10_0);
				float ColorMask156 = ((temp_output_12_0 * temp_output_228_0) + (_Invertmask * (1.0 - temp_output_8_0)));
				float4 Rimlight167 = (_Enablerimlight * (_Rimlightcolor * fresnelNode94 * ColorMask156));
				float2 uv_Bordertexture = i.uv_texcoord * _Bordertexture_ST.xy + _Bordertexture_ST.zw;
				float Border120 = (temp_output_8_0 - temp_output_12_0);
				float4 ColoredBorder169 = (_Bordercolor * tex2D(_Bordertexture, uv_Bordertexture) * Border120);
				float2 uv_TexCoord85 = i.uv_texcoord * _Emissiontextiling;
				float2 panner82 = (1.0 * _Time.y * _Emissiontexspeed + uv_TexCoord85);
				float4 BaseTex132 = tex2D(_Emission, panner82);
				float2 uv_TexCoord88 = i.uv_texcoord * _Noisetextiling;
				float2 panner89 = (1.0 * _Time.y * _Noisetexspeed + uv_TexCoord88);
				float SecondaryEmissionNoiseDesaturation285 = _SecondaryEmissionNoiseDesaturation;
				float3 desaturateInitialColor81 = tex2D(_Secondaryemissionnoise, panner89).rgb;
				float desaturateDot81 = dot(desaturateInitialColor81, float3(0.299, 0.587, 0.114));
				float3 desaturateVar81 = lerp(desaturateInitialColor81, desaturateDot81.xxx, SecondaryEmissionNoiseDesaturation285);
				float3 NoiseTex130 = desaturateVar81;
				float2 uv_TexCoord57 = i.uv_texcoord * _Secondaryemissiontiling;
				float2 panner60 = (1.0 * _Time.y * _Secondaryemissionspeed + uv_TexCoord57);
				float SecondaryEmissionDesaturation249 = _SecondaryEmissionDesaturation;
				float3 desaturateInitialColor64 = tex2D(_Secondaryemission, panner60).rgb;
				float desaturateDot64 = dot(desaturateInitialColor64, float3(0.299, 0.587, 0.114));
				float3 desaturateVar64 = lerp(desaturateInitialColor64, desaturateDot64.xxx, SecondaryEmissionDesaturation249);
				float3 SecondaryEmissionTex128 = desaturateVar64;
				float Activatesecondaryemission278 = _Activatesecondaryemission;
				float4 Textures126 = ((_Activateemission * _Basecolor * BaseTex132) + (float4(NoiseTex130 , 0.0) * float4(SecondaryEmissionTex128 , 0.0) * _Secondaryemissioncolor * _Noisetexopacity * Activatesecondaryemission278));
				float4 switchResult157 = (((i.ASEVFace > 0) ? ((Rimlight167 + ColoredBorder169 + (ColorMask156 * Textures126))) : ((ColoredBorder169 + (_Tintinsidecolor * _Fillcolor * ColorMask156)))));
				float4 Emission267 = switchResult157;
				o.Emission = Emission267.rgb;
				float4 Specular270 = tex2D(_Specular, UVTilingOffset290);
				float4 temp_output_271_0 = Specular270;
				o.Specular = temp_output_271_0.rgb;
				float temp_output_259_0 = _Smoothness;
				o.Smoothness = temp_output_259_0;
				float4 Occlusion274 = tex2D(_Occlusion, UVTilingOffset290);
				o.Occlusion = Occlusion274.r;
				o.Alpha = 1;
				float OpacityMask121 = ((temp_output_8_0 * temp_output_228_0) + ((1.0 - temp_output_12_0) * _Invertmask));
				clip(OpacityMask121 - _Cutoff);
			}

			ENDCG
			CGPROGRAM
			#pragma surface surf StandardSpecular keepalpha fullforwardshadows 

			ENDCG
			Pass
			{
				Name "ShadowCaster"
				Tags{ "LightMode" = "ShadowCaster" }
				ZWrite On
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_shadowcaster
				#pragma multi_compile UNITY_PASS_SHADOWCASTER
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#include "HLSLSupport.cginc"
				#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
					#define CAN_SKIP_VPOS
				#endif
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				struct v2f
				{
					V2F_SHADOW_CASTER;
					float2 customPack1 : TEXCOORD1;
					float4 tSpace0 : TEXCOORD2;
					float4 tSpace1 : TEXCOORD3;
					float4 tSpace2 : TEXCOORD4;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};
				v2f vert(appdata_full v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					Input customInputData;
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					half3 worldNormal = UnityObjectToWorldNormal(v.normal);
					half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
					half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
					half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
					o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
					o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
					o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
					o.customPack1.xy = customInputData.uv_texcoord;
					o.customPack1.xy = v.texcoord;
					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					return o;
				}
				half4 frag(v2f IN
				#if !defined( CAN_SKIP_VPOS )
				, UNITY_VPOS_TYPE vpos : VPOS
				#endif
				) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);
					Input surfIN;
					UNITY_INITIALIZE_OUTPUT(Input, surfIN);
					surfIN.uv_texcoord = IN.customPack1.xy;
					float3 worldPos = float3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
					half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
					surfIN.worldPos = worldPos;
					surfIN.worldNormal = float3(IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z);
					surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
					surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
					surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
					SurfaceOutputStandardSpecular o;
					UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandardSpecular, o)
					surf(surfIN, o);
					#if defined( CAN_SKIP_VPOS )
					float2 vpos = IN.pos;
					#endif
					SHADOW_CASTER_FRAGMENT(IN)
				}
				ENDCG
			}
		}
		Fallback "Diffuse"
	    CustomEditor "SterlexDev.DissolveShaderEditor"
}
