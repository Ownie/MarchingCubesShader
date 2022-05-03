using System.IO;
using System.Linq;
using DirectxWpf.Models;
using SharpDX;

namespace DirectxWpf.Helpers
{
    public class ObjExporter
    {
        public static void SaveModel(GS4[] data, string location)
        {
            // Variables
            int numVertices = 0;
            StreamWriter f = new StreamWriter(location, false);

            //var query = from gs4 in data where (gs4.Position == Vector4.Zero) && (gs4.Normal == Vector3.Zero) && (gs4.WorldPos == Vector3.Zero) select gs4;
            var query =
                data.
                    Select((gs4, index) => new {GS4 = gs4, Index = index}).
                    Where(
                        x =>
                            (x.GS4.Position == Vector4.Zero) && (x.GS4.Normal == Vector3.Zero) &&
                            (x.GS4.WorldPos == Vector3.Zero)).
                                Select(x => x.Index).First();

            numVertices = query;

            // Write Positions
            int count = 0;
            foreach (GS4 pos in data)
            {
                if (count >= numVertices)
                {
                    break;
                }
                count++;

                f.Write($"v {pos.WorldPos.X.ToString("0.0000")} {pos.WorldPos.Y.ToString("0.0000")} {pos.WorldPos.Z.ToString("0.0000")}\n");
            }
            f.Write("\n");

            // Normals
            count = 0;
            foreach (GS4 norm in data)
            {
                if (count >= numVertices)
                {
                    break;
                }
                ++count;

                f.Write($"vn {norm.Normal.X.ToString("0.0000")} {norm.Normal.Y.ToString("0.0000")} {norm.Normal.Z.ToString("0.0000")}\n");
            }
            f.Write("\n");

            // Object Name
            f.Write("g Chunk\n");
            // Indices/Match Normals
            count = 0;
            for (int i = 1; i < (numVertices - 2); i+=3)
            {
                ++count;
                f.Write($"f {i}//{i} {i + 1}//{i+1} {i + 2}//{i+2}\n");
            }
            f.Write("\n");

            //Close
            f.Close();
        }
    }
}