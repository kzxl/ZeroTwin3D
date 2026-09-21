using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ZeroTwin3D.Engine;
using ZeroTwin3D.Sync;

namespace ZeroTwin3D.IO
{
    /// <summary>
    /// Pure C# Wavefront OBJ 3D model loader supporting multi-group meshes, normals, UVs, and polygon triangulation.
    /// </summary>
    public static class ObjLoader
    {
        /// <summary>
        /// Loads a Wavefront OBJ file from disk into a hierarchical <see cref="TwinNode"/>.
        /// </summary>
        public static TwinNode LoadFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"OBJ model file not found: {filePath}", filePath);

            string text = File.ReadAllText(filePath);
            string name = Path.GetFileNameWithoutExtension(filePath);
            return Load(text, name);
        }

        /// <summary>
        /// Loads Wavefront OBJ ASCII content into a hierarchical <see cref="TwinNode"/>.
        /// </summary>
        public static TwinNode Load(string objText, string nodeName = "ObjModel")
        {
            if (objText == null) throw new ArgumentNullException(nameof(objText));

            var rootNode = new TwinNode { Name = nodeName };
            var positions = new List<Vec3>();
            var normals = new List<Vec3>();
            var uvs = new List<Vec2>();

            // Grouping state
            string currentGroupName = "Default";
            var currentMesh = new Mesh3D();
            var namedMeshes = new Dictionary<string, Mesh3D>(StringComparer.OrdinalIgnoreCase);

            using var reader = new StringReader(objText);
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;

                string[] parts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                switch (parts[0])
                {
                    case "v" when parts.Length >= 4:
                        positions.Add(new Vec3(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture),
                            float.Parse(parts[3], CultureInfo.InvariantCulture)
                        ));
                        break;

                    case "vn" when parts.Length >= 4:
                        normals.Add(new Vec3(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture),
                            float.Parse(parts[3], CultureInfo.InvariantCulture)
                        ));
                        break;

                    case "vt" when parts.Length >= 3:
                        uvs.Add(new Vec2(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture)
                        ));
                        break;

                    case "o":
                    case "g":
                        string newName = parts.Length > 1 ? parts[1].Trim() : "Group";
                        if (currentMesh.Vertices.Count > 0)
                        {
                            currentMesh.ComputeBounds();
                            namedMeshes[currentGroupName] = currentMesh;
                            currentMesh = new Mesh3D();
                        }
                        currentGroupName = newName;
                        break;

                    case "f" when parts.Length >= 4:
                        // Triangle or quad / n-gon fan-triangulation
                        int firstIdx = AddFaceVertex(currentMesh, parts[1], positions, normals, uvs);
                        int prevIdx = AddFaceVertex(currentMesh, parts[2], positions, normals, uvs);

                        for (int i = 3; i < parts.Length; i++)
                        {
                            int currIdx = AddFaceVertex(currentMesh, parts[i], positions, normals, uvs);
                            currentMesh.Indices.Add(firstIdx);
                            currentMesh.Indices.Add(prevIdx);
                            currentMesh.Indices.Add(currIdx);
                            prevIdx = currIdx;
                        }
                        break;
                }
            }

            if (currentMesh.Vertices.Count > 0)
            {
                currentMesh.ComputeBounds();
                namedMeshes[currentGroupName] = currentMesh;
            }

            if (namedMeshes.Count == 1 && namedMeshes.ContainsKey("Default"))
            {
                rootNode.Mesh = namedMeshes["Default"];
            }
            else
            {
                foreach (var kvp in namedMeshes)
                {
                    var child = new TwinNode
                    {
                        Name = kvp.Key,
                        Mesh = kvp.Value
                    };
                    rootNode.AddChild(child);
                }
            }

            return rootNode;
        }

        private static int AddFaceVertex(Mesh3D mesh, string faceToken, List<Vec3> positions, List<Vec3> normals, List<Vec2> uvs)
        {
            string[] sub = faceToken.Split('/');
            int pIdx = int.Parse(sub[0]) - 1;
            Vec3 pos = (pIdx >= 0 && pIdx < positions.Count) ? positions[pIdx] : Vec3.Zero;

            Vec2 uv = Vec2.Zero;
            if (sub.Length > 1 && !string.IsNullOrEmpty(sub[1]))
            {
                int uvIdx = int.Parse(sub[1]) - 1;
                if (uvIdx >= 0 && uvIdx < uvs.Count) uv = uvs[uvIdx];
            }

            Vec3 norm = Vec3.UnitY;
            if (sub.Length > 2 && !string.IsNullOrEmpty(sub[2]))
            {
                int nIdx = int.Parse(sub[2]) - 1;
                if (nIdx >= 0 && nIdx < normals.Count) norm = normals[nIdx];
            }

            mesh.Vertices.Add(new Vertex3D(pos, norm, uv));
            return mesh.Vertices.Count - 1;
        }
    }
}
