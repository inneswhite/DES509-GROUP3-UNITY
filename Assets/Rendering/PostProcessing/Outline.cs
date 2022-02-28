using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/Outline")]
public sealed class Outline : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter depthDistance = new ClampedFloatParameter(1f, 0f, 32f);
    public ClampedIntParameter scale = new ClampedIntParameter(1, 0, 100);
    public FloatParameter depthThreshold = new FloatParameter(0.2f);
    public FloatParameter normalThreshold = new FloatParameter(0.4f);
    public ColorParameter edgeColor = new ColorParameter(Color.cyan);


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
        Vector4 parameters = new Vector4(depthDistance.value, scale.value, depthThreshold.value, depthDistance.value);

        m_Material.SetTexture("_InputTexture", source);
        m_Material.SetMatrix("_ClipToView", clipToView);
        m_Material.SetFloat("_NormalThreshold", normalThreshold.value);
        m_Material.SetVector("_Params", parameters);
        m_Material.SetTexture("_InputTexture", source);
        m_Material.SetColor("_EdgeColor", edgeColor.value);
        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }


    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
