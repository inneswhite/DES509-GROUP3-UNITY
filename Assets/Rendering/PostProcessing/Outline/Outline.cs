using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/Outline")]
public sealed class Outline : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    public ClampedIntParameter scale = new ClampedIntParameter(1, 0, 100);
    public ColorParameter edgeColor = new ColorParameter(Color.cyan);
    
    public ClampedFloatParameter depthThreshold = new ClampedFloatParameter(0.2f,0,100);
    public FloatParameter depthNormalThreshold = new FloatParameter(0.5f);
    public FloatParameter depthNormalThresholdScale = new FloatParameter(7);
    public FloatParameter normalThreshold = new FloatParameter(0.4f);
    


    Material m_Material;

    public bool IsActive() => m_Material != null;

    // Do not forget to add this post process in the Custom Post Process Orders list (Project Settings > HDRP Default Settings).
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    const string kShaderName = "Custom/Outline";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
            Debug.LogError($"Unable to find shader '{kShaderName}'. Post Process Volume Outline is unable to load.");
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        Matrix4x4 clipToView = GL.GetGPUProjectionMatrix(camera.camera.projectionMatrix, true).inverse;

        m_Material.SetTexture("_InputTexture", source);

        m_Material.SetMatrix("_ClipToView", clipToView);

        m_Material.SetFloat("_Scale", scale.value);
        m_Material.SetFloat("_DepthThreshold", depthThreshold.value);
        m_Material.SetFloat("_DepthNormalThreshold", depthNormalThreshold.value);
        m_Material.SetFloat("_DepthNormalThresholdScale", depthNormalThresholdScale.value);
        m_Material.SetFloat("_NormalThreshold", normalThreshold.value);

        
        m_Material.SetColor("_EdgeColor", edgeColor.value);
        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }


    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
