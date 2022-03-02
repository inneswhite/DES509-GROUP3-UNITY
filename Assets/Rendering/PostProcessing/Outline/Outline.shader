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


		
			float4 _EdgeColor;

			float _Scale;
			float _DepthThreshold;
			float _DepthNormalThreshold;
			float _DepthNormalThresholdScale;

			float _NormalThreshold;

			float4x4 _ClipToView;

			TEXTURE2D_X(_InputTexture);
			TEXTURE2D(_DepthTexture);

			float4 alphaBlend(float4 top, float4 bottom)
			{
				float3 color = (top.rgb * top.a) + (bottom.rgb * (1 - top.a));
				float alpha = top.a + bottom.a * (1 - top.a);

				return float4(color, alpha);
			}

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



			float3 GetNormalWS(float2 positionSS, float depth)
			{
				float3 normalWS = 0;
				if (depth > 0.0f)
				{
					NormalData normalData;
					DecodeFromNormalBuffer(positionSS, normalData);
					normalWS = normalData.normalWS;
				}
				return normalWS;
			}

			float4 CustomPostProcess(Varyings input) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				uint2 positionSS = input.texcoord * _ScreenSize.xy;

				float4 inputColor = float4(LOAD_TEXTURE2D_X(_InputTexture, positionSS).xyz,1);

				float4 texelSize = float4(1/ _ScreenSize.x, 1/ _ScreenSize.y, _ScreenSize.x, _ScreenSize.y);

				float halfScaleFloor = floor(_Scale * 0.5);
				float halfScaleCeil = ceil(_Scale * 0.5);


				float2 bottomLeftUV = input.texcoord - float2(texelSize.x, texelSize.y) * halfScaleFloor;
				float2 topRightUV = input.texcoord + float2(texelSize.x, texelSize.y) * halfScaleCeil;  
				float2 bottomRightUV = input.texcoord + float2(texelSize.x * halfScaleCeil, -texelSize.y * halfScaleFloor);
				float2 topLeftUV = input.texcoord + float2(-texelSize.x * halfScaleFloor, texelSize.y * halfScaleCeil);

				float2 bottomLeftSS = float2(bottomLeftUV.x * _ScreenSize.x, bottomLeftUV.y * _ScreenSize.y);
				float2 topRightSS = float2(topRightUV.x * _ScreenSize.x, topRightUV.y * _ScreenSize.y);
				float2 bottomRightSS = float2(bottomRightUV.x * _ScreenSize.x, bottomRightUV.y * _ScreenSize.y);
				float2 topLeftSS = float2(topLeftUV.x * _ScreenSize.x, topLeftUV.y * _ScreenSize.y);
				
				float depth0 = SampleCameraDepth(bottomLeftUV);
				float depth1 = SampleCameraDepth(topRightUV);
				float depth2 = SampleCameraDepth(bottomRightUV);
				float depth3 = SampleCameraDepth(topLeftUV);

				float3 normalWS0 = GetNormalWS(bottomLeftSS, depth0);
				float3 normalWS1 = GetNormalWS(topRightSS, depth1);
				float3 normalWS2 = GetNormalWS(bottomRightSS, depth2);
				float3 normalWS3 = GetNormalWS(topLeftSS, depth3);

				float3 viewNormal = normalWS0 * 2 - 1;
				float NdotV = 1 - dot(viewNormal, -input.viewSpaceDir);

				float normalThreshold01 = saturate((NdotV - _DepthNormalThreshold) / (1- _DepthNormalThreshold));
				float normalThreshold = normalThreshold01 * _DepthNormalThresholdScale + 1;
			
				float depthThreshold = _DepthThreshold * depth0 * normalThreshold;

				float depthFiniteDifference0 = depth1 - depth0;
				float depthFiniteDifference1 = depth3 - depth2;

				float edgeDepth = sqrt(pow(depthFiniteDifference0, 2) + pow(depthFiniteDifference1, 2)) * 100;
				edgeDepth = edgeDepth > depthThreshold ? 1 : 0;

				
				float3 normalFiniteDifference0 = normalWS1 - normalWS0;
				float3 normalFiniteDifference1 = normalWS3 - normalWS2;

				float edgeNormal = sqrt(dot(normalFiniteDifference0, normalFiniteDifference0) + dot(normalFiniteDifference1, normalFiniteDifference1));
				edgeNormal = edgeNormal > _NormalThreshold ? 1 : 0;

				float edge = max(edgeDepth, edgeNormal);
				float4 edgeColor = float4(_EdgeColor.rgb, _EdgeColor.a * edge);

				return alphaBlend(edgeColor, inputColor);

			}

			ENDHLSL
		}
		Pass
        {
            Stencil
            {
                WriteMask [_StencilMask]
                Ref [_StencilRef]
                Comp Always
                Pass Replace
            }

            HLSLPROGRAM

                #pragma vertex VertEdge
                #pragma fragment FragEdge
                #include "SubpixelMorphologicalAntialiasingBridgeOutline.hlsl"

            ENDHLSL
        }
	}

	Fallback Off
}