using DirectxWpf.Model;
using DirectxWpf.Models;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D10;

namespace DirectxWpf.Effects
{
    public class VoxelGeneratorEffectLight : IEffect
    {
        public EffectTechnique Technique { get; set; } = null;
        public Effect Effect { get; set; } = null;
        public InputLayout InputLayout { get; set; } = null;

        public void Create(Device1 device)
        {
            // Create effect
            const string loc = "Resources/TerrainShader.fx";

            CompilationResult result = ShaderBytecode.CompileFromFile(loc, "fx_4_0",
                ShaderFlags.SkipOptimization 
                | ShaderFlags.AvoidFlowControl
                | ShaderFlags.NoPreshader
                | ShaderFlags.EnableStrictness
                | ShaderFlags.SkipValidation
                , EffectFlags.AllowSlowOperations);
            
            Effect = new Effect(device, result.Bytecode.Data);

            // Retrieve technique
            Technique = Effect.GetTechniqueByName("DefaultTechnique");

            // Define inputlayout
            var sign = ShaderSignature.GetInputSignature(Technique.GetPassByIndex(0).Description.Signature.Data);
            InputLayout = new InputLayout(device, sign.Data, InputLayouts.Pos);

            result.Dispose();
        }

        public void SetVariables(SettingsModel settings)
        {
            if (settings != null)
            {
                SetLightDirection(settings.LightDirection);

                Effect.GetVariableByName("CellSize").AsScalar().Set(settings.CellSize);

                Effect.GetVariableByName("m_Color").AsVector().Set(settings.Color);

                //if (settings.NoiseMap01 != null)
                    Effect.GetVariableByName("m_NoiseMap01").AsShaderResource().SetResource(settings.NoiseMap01);
                //if (settings.NoiseMap02 != null)
                    Effect.GetVariableByName("m_NoiseMap02").AsShaderResource().SetResource(settings.NoiseMap02);
                //if (settings.NoiseMap03 != null)
                    Effect.GetVariableByName("m_NoiseMap03").AsShaderResource().SetResource(settings.NoiseMap03);

                //if (settings.DensityVolume != null)
                //    Effect.GetVariableByName("m_DensityVol").AsShaderResource().SetResource(settings.DensityVolume);
                Effect.GetVariableByName("m_DensityNormalDiv").AsScalar().Set(settings.DensityNormalDiv);

                Effect.GetVariableByName("m_UseTexture").AsScalar().Set(settings.UseTexture);
                //if (settings.TextureNoise != null)
                    Effect.GetVariableByName("m_TextureNoise").AsShaderResource().SetResource(settings.TextureNoise);
                //if (settings.TextureMap != null)
                    Effect.GetVariableByName("m_TextureMap").AsShaderResource().SetResource(settings.TextureMap);
                Effect.GetVariableByName("m_UseNormalMap").AsScalar().Set(settings.UseNormalMap);
                //if (settings.NormalMap != null)
                    Effect.GetVariableByName("m_NormalMap").AsShaderResource().SetResource(settings.NormalMap);
                Effect.GetVariableByName("m_ModU").AsScalar().Set(settings.ModU);
                Effect.GetVariableByName("m_ModUOffset").AsScalar().Set(settings.ModUOffset);
                Effect.GetVariableByName("m_ModV").AsScalar().Set(settings.ModV);
                Effect.GetVariableByName("m_Clamp").AsScalar().Set(settings.Clamp);

                Effect.GetVariableByName("m_UseSpecular").AsScalar().Set(settings.UseSpecular);
                Effect.GetVariableByName("m_Shininess").AsScalar().Set(settings.Shininess);
                Effect.GetVariableByName("m_ColorSpecular").AsVector().Set(settings.ColorSpecular);

                Effect.GetVariableByName("m_AmbientIntensity").AsScalar().Set(settings.AmbientIntensity);
                Effect.GetVariableByName("m_AmbientColor").AsVector().Set(settings.AmbientColor);

                Effect.GetVariableByName("m_Frequency01").AsScalar().Set(settings.Frequency01);
                Effect.GetVariableByName("m_Frequency02").AsScalar().Set(settings.Frequency02);
                Effect.GetVariableByName("m_Frequency03").AsScalar().Set(settings.Frequency03);
                Effect.GetVariableByName("m_Amplitude01").AsScalar().Set(settings.Amplitude01);
                Effect.GetVariableByName("m_Amplitude02").AsScalar().Set(settings.Amplitude02);
                Effect.GetVariableByName("m_Amplitude03").AsScalar().Set(settings.Amplitude03);

                Effect.GetVariableByName("m_GroundLevel").AsScalar().Set(settings.GroundLevel);
                Effect.GetVariableByName("m_ForcedGroundLevel").AsScalar().Set(settings.ForcedGroundLevel);
                Effect.GetVariableByName("m_ForceGroundLevel").AsScalar().Set(settings.ForceGroundLevel);

                Effect.GetVariableByName("m_Sphere").AsScalar().Set(settings.Sphere);
                Effect.GetVariableByName("m_Spherical").AsScalar().Set(settings.Spherical);
            }
        }

        public void SetWorld(Matrix world)
        {
            // Send world to shader
            Effect.GetVariableByName("m_MatrixWorld").AsMatrix().SetMatrix<Matrix>(world);
        }

        public void SetWorldViewProjection(Matrix wvp)
        {
            // Send wvp to shader
            Effect.GetVariableByName("m_MatrixWorldViewProj").AsMatrix().SetMatrix<Matrix>(wvp);
        }

        public void SetViewProjection(Matrix wvp)
        {
            // Send viewprojection to shader
            Effect.GetVariableByName("m_MatrixViewProj").AsMatrix().SetMatrix<Matrix>(wvp);
        }

        public void SetViewInverse(Matrix wvp)
        {
            // Send viewinverse to shader
            Effect.GetVariableByName("m_MatrixViewInv").AsMatrix().SetMatrix<Matrix>(wvp);
        }

        public void SetLightDirection(Vector3 dir)
        {
            // Send light dir to shader
            Effect.GetVariableByName("m_LightDirection").AsVector().Set<Vector3>(dir);
        }
    }
}