namespace DirectxWpf.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using SharpDX;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D10;
    using Buffer = SharpDX.Direct3D10.Buffer;

    namespace DirectxWpf.Models
    {
        public class ChunkModel : IModel
        {
            public PrimitiveTopology PrimitiveTopology { get; set; }
            public int VertexStride { get; set; }
            public int IndexCount { get; set; }
            public int VertexCount { get; set; }
            public Buffer IndexBuffer { get; set; }
            public Buffer VertexBuffer { get; set; }

            public void Create(Device1 device)
            {
                const string fileLoc = "Resources/Chunk.ovm";

                // Set PrimitiveTopolgy
                PrimitiveTopology = PrimitiveTopology.PointList;

                // Set VertexStride (size in bytes)
                // NOT sizeof(VertexPosColNorm) // Marshal
                VertexStride = Marshal.SizeOf<VertexPos>();

                Color col = Color.Gray;
                var vertsPos = new List<Vector3>();
                var vertsNorm = new List<Vector3>();
                var vertsCol = new List<Vector4>();
                var verts = new List<VertexPos>();
                var indices = new List<UInt32>();

                //read file
                FileStream stream = new FileStream(fileLoc, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                string name;

                byte vMajor = reader.ReadByte();
                byte vMinor = reader.ReadByte();

                bool isDone = false;
                while (!isDone)
                {
                    byte blockId = reader.ReadByte();

                    if (blockId == 0)
                    {
                        isDone = true;
                        break;
                    }

                    UInt32 blockLength = reader.ReadUInt32();

                    switch (blockId)
                    {
                        case 1:
                            name = reader.ReadString();
                            verts.Capacity = (int)reader.ReadUInt32();
                            VertexCount = verts.Capacity;
                            vertsPos.Capacity = verts.Capacity;
                            vertsNorm.Capacity = verts.Capacity;
                            IndexCount = (int)reader.ReadUInt32();
                            indices.Capacity = IndexCount;
                            break;

                        case 2: // POS
                            for (int i = 0; i < verts.Capacity; ++i)
                            {
                                Vector3 vert = new Vector3();
                                vert.X = reader.ReadSingle();
                                vert.Y = reader.ReadSingle();
                                vert.Z = reader.ReadSingle();
                                vertsPos.Add(vert);
                            }
                            break;

                        case 3: // INDICES
                            for (int i = 0; i < IndexCount; ++i)
                            {
                                indices.Add(reader.ReadUInt32());
                            }
                            break;

                        case 4: // NORMAL
                            for (int i = 0; i < verts.Capacity; ++i)
                            {
                                Vector3 vert = new Vector3();
                                vert.X = reader.ReadSingle();
                                vert.Y = reader.ReadSingle();
                                vert.Z = reader.ReadSingle();
                                vertsNorm.Add(vert);
                            }
                            break;
                        case 7: // COLORS
                            for (int i = 0; i < verts.Capacity; ++i)
                            {
                                Vector4 vert = new Vector4();
                                vert.X = reader.ReadSingle();
                                vert.Y = reader.ReadSingle();
                                vert.Z = reader.ReadSingle();
                                vert.W = reader.ReadSingle();
                                vertsCol.Add(vert);
                            }
                            break;
                        default:
                            int b = (int)blockLength;
                            reader.ReadBytes(b);
                            break; ;
                    }
                }
                reader.Close();
                stream.Close();

                verts.Clear();
                vertsPos.ForEach(vert => verts.Add(new VertexPos(vert)));

                VertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, verts.ToArray());
                IndexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices.ToArray());
            }
        }
    }
}