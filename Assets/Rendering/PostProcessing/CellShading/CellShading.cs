using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/CellShading")]
public sealed class CellShading : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    [Tooltip("Controls the intensity of the effect.")]
    public ClampedIntParameter steps = new ClampedIntParameter(1, 1, 10);
    public ClampedFloatParameter minimumBrightness = new ClampedFloatParameter(0.1f, 0f, 1f);
    public ClampedFloatParameter minimumSaturation = new ClampedFloatParameter(0.1f, 0f, 1f);
    public ClampedIntParameter darkValuePriority = new ClampedIntParameter(1, 1, 10);

    Material m_Material;

    public bool IsActive() => m_Material != null;

    // Do not forget to add this post process in the Custom Post Process Orders list (Project Settings > HDRP Default Settings).
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    const string kShaderName = "Custom/CellShading";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
            Debug.LogError($"Unable to find shader '{kShaderName}'. Post Process Volume CellShading is unable to load.");

    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        m_Material.SetTexture("_InputTexture", source);
        m_Material.SetFloat("_Steps", steps.value);
        m_Material.SetFloat("_MinimumBrightness", minimumBrightness.value);
        m_Material.SetFloat("_MinimumSaturation", minimumSaturation.value);
        m_Material.SetFloat("_DarkValuePriority", darkValuePriority.value);
        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }

    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
