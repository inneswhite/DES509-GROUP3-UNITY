Shader "Custom/CellShading"
{
    HLSLINCLUDE

    #pragma target 4.5
    #pragma only_renderers d3d11 playstation xboxone vulkan metal switch

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/FXAA.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/RTUpscale.hlsl"

    struct Attributes
    {
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float2 texcoord   : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
        output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
        return output;
    }

    // List of properties to control your post process effect
    float _Steps;
    float _MinimumBrightness;
    float _MinimumSaturation;
    float _DarkValuePriority;
    TEXTURE2D_X(_InputTexture);

    float posterize(float In, float steps)
    {
        float nonLinearIn = pow(In, 1 / _DarkValuePriority);
        float stepped = floor(nonLinearIn / (1/steps)) * (1/steps);
        return clamp(stepped, _MinimumBrightness, 1);
    }


    

    float4 CustomPostProcess(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        uint2 positionSS = input.texcoord * _ScreenSize.xy;

        float3 outColor = LOAD_TEXTURE2D_X(_InputTexture, positionSS).xyz;
        
        /*
        float greyscale = max(outColor.r, max(outColor.g, outColor.b));
        float lower = floor((greyscale * _Steps)) / _Steps;
        float lowerDiff = abs(greyscale - lower);

        float upper = ceil(greyscale * _Steps) / _Steps;
        float upperDiff = abs(upper - greyscale);

        float level = lower <= upperDiff ? lower : upper;
        float adjustment = level / greyscale;
        */

        

        float3 hsvColor = RgbToHsv(outColor);

        hsvColor.g = clamp(hsvColor.g, _MinimumSaturation, 1);


        hsvColor.b = posterize((hsvColor.b), _Steps);

        outColor = HsvToRgb(hsvColor);

        outColor = saturate(outColor);

        

        return float4(outColor, 1); 
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "CellShading"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}