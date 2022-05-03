using DirectxWpf.Model;

namespace DirectxWpf.Models
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using SharpDX;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D10;
    using Buffer = SharpDX.Direct3D10.Buffer;

    namespace DirectxWpf.Models
    {
        public class ChunkGenerator : IModel
        {
            public PrimitiveTopology PrimitiveTopology { get; set; }
            public int VertexStride { get; set; }
            public int IndexCount { get; set; }
            public int VertexCount { get; set; }
            public Buffer IndexBuffer { get; set; }
            public Buffer VertexBuffer { get; set; }
            public SettingsModel Settings { get; set; }

            public void Create(Device1 device)
            {
                // Settings
                var X = Settings.SizeX;
                var Y = Settings.SizeY;
                var Z = Settings.SizeZ;

                // Set PrimitiveTopolgy
                PrimitiveTopology = PrimitiveTopology.PointList;
                // Set VertexStride (size in bytes)
                VertexStride = Marshal.SizeOf<VertexPos>();
                // Set Vertexcount
                VertexCount = X*Y*Z;

                // Data
                var verts = new List<VertexPos>(VertexCount);
                // Generate vertices
                for (int x = -X / 2; x < X / 2; ++x)
                {
                    for (int y = -Y / 2; y < Y / 2; ++y)
                    {
                        for (int z = -Z / 2; z < Z / 2; ++z)
                        {
                            verts.Add(new VertexPos(new Vector3(x,y,z)));
                        }
                    }
                }

                // Create buffer
                VertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, verts.ToArray());
                // No need for indexbuffer
                IndexBuffer = null;
            }
        }
    }
}