Shader "Custom/Outline"
{
	SubShader
	{
		Pass
		{
			Name "Outline"

			ZWrite Off
			ZTest Always
			Blend Off
			Cull Off

			HLSLPROGRAM

			#pragma fragment CustomPostProcess
			#pragma vertex Vert

			#pragma target 4.5
			#pragma only_renderers d3d11 playstation xboxone vulkan metal switch

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/NormalBuffer.hlsl"


			float4 _Params;

			#define _Distance _Params.x
			#define _Scale _Params.y
			#define _DepthThreshold _Params.z;

			float _NormalThreshold;

			float4x4 _ClipToView;

			TEXTURE2D(_InputTexture);
			TEXTURE2D(_DepthTexture);


			struct Attributes
			{
				float3 vertex : POSITION;
				uint vertexID : SV_VertexID;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float3 viewSpaceDir : TEXCOORD2;

				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vert(Attributes input)
			{
				Varyings output;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);



				output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);

				output.texcoord =  GetFullScreenTriangleTexCoord(input.vertexID);

				output.viewSpaceDir = mul(_ClipToView, float4(input.vertex.xy, 0.0, 1.0)).xyz;
				return output;
			}


			

			float4 CustomPostProcess(Varyings input) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				uint2 positionSS = input.texcoord * _ScreenSize.xy;

				float4 texelSize = float4(1/ _ScreenSize.x, 1/ _ScreenSize.y, _ScreenSize.x, _ScreenSize.y);

				float halfScaleFloor = floor(_Scale * 0.5);
				float halfScaleCeil = ceil(_Scale * 0.5);


				float2 bottomLeftUV = input.texcoord - float2(texelSize.x, texelSize.y) * halfScaleFloor;
				float2 topRightUV = input.texcoord + float2(texelSize.x, texelSize.y) * halfScaleCeil;  

				float2 bottomRightUV = input.texcoord + float2(texelSize.x * halfScaleCeil, -texelSize.y * halfScaleFloor);
				float2 topLeftUV = input.texcoord + float2(-texelSize.x * halfScaleFloor, texelSize.y * halfScaleCeil);

				
				
				float depth0 = _Distance/ LinearEyeDepth(SampleCameraDepth(bottomLeftUV), _ZBufferParams);
				float depth1 = _Distance/LinearEyeDepth(SampleCameraDepth(topRightUV), _ZBufferParams);
				float depth2 =_Distance/ LinearEyeDepth(SampleCameraDepth(bottomRightUV), _ZBufferParams);
				float depth3 = _Distance/LinearEyeDepth(SampleCameraDepth(topLeftUV), _ZBufferParams);
			
				float depthThreshold = _DepthThreshold;
				depthThreshold *= depth0;

				float depthFiniteDifference0 = depth1 - depth0;
				float depthFiniteDifference1 = depth3 - depth2;

				float edgeDepth = sqrt(pow(depthFiniteDifference0, 2) + pow(depthFiniteDifference1, 2)) * 100;
				edgeDepth = edgeDepth > depthThreshold ? 1 : 0;

				float2 bottomLeftSS = float2(bottomLeftUV.x * _ScreenSize.x, bottomLeftUV.y * _ScreenSize.y);
				float2 topRightSS = float2(topRightUV.x * _ScreenSize.x, topRightUV.y * _ScreenSize.y);
				float2 bottomRightSS = float2(bottomRightUV.x * _ScreenSize.x, bottomRightUV.y * _ScreenSize.y);
				float2 topLeftSS = float2(topLeftUV.x * _ScreenSize.x, topLeftUV.y * _ScreenSize.y);

				NormalData normalData;
				DecodeFromNormalBuffer(bottomRightSS, normalData);
				float3 normal0 = normalData.normalWS;
				DecodeFromNormalBuffer(topRightSS, normalData);
				float3 normal1 = normalData.normalWS;
				DecodeFromNormalBuffer(bottomRightSS, normalData);
				float3 normal2 = normalData.normalWS;
				DecodeFromNormalBuffer(topLeftSS, normalData);
				float3 normal3 = normalData.normalWS;
				
				float3 normalFiniteDifference0 = normal1 - normal0;
				float3 normalFiniteDifference1 = normal3 - normal2;

				float edgeNormal = sqrt(dot(normalFiniteDifference0, normalFiniteDifference0) + dot(normalFiniteDifference1, normalFiniteDifference1));
				edgeNormal = edgeNormal > _NormalThreshold ? 1 : 0;

				float edge = max(edgeDepth, edgeNormal);

				//return float4(input.viewSpaceDir,1);
				return edge;

			}

			ENDHLSL
		}
	}

	Fallback Off
}