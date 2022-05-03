using SharpDX;
using SharpDX.Direct3D10;
using SharpDX.DXGI;

namespace DirectxWpf.Models
{
    public struct VertexPos
    {
        public Vector3 Position;

        public VertexPos(Vector3 pos)
        {
            Position = pos;
        }
    }

    public struct GS4
    {
        public Vector4 Position;
        public Vector3 WorldPos;
        public Vector3 Normal;

        public GS4(Vector4 pos, Vector3 worldPos, Vector3 normal)
        {
            Position = pos;
            WorldPos = worldPos;
            Normal = normal;
        }
    }

    public static class InputLayouts
    {
        public static readonly InputElement[] Pos = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0)
        };
    }

    public static class StreamOutputLayouts
    {
        public static readonly StreamOutputElement[] GS4 =
        {
            new StreamOutputElement("SV_POSITION", 0, 0, 4, 0),
            new StreamOutputElement("POSITION0", 0, 3, 0, 0),
            new StreamOutputElement("NORMAL", 0, 0, 3, 0)
        };
    }

}
