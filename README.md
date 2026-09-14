# ZeroTwin3D: Pure C# 3D Digital Twin & Robot Kinematics for .NET

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET Multi-Targeting](https://img.shields.io/badge/.NET-8.0%20%7C%204.6.2%20%7C%20Standard%202.0-purple.svg)](https://dotnet.microsoft.com/)

**ZeroTwin3D** is a lightweight, pure C# 3D digital twin engine, robotic kinematics solver, and spatial safety boundary supervisor for .NET.

## Key Features

- **Robotic Kinematics Engine**: Analytical Denavit-Hartenberg (DH) forward kinematics solver (`KinematicsSolver`) supporting 6-DOF articulated arms and 4-DOF SCARA robots.
- **Pure C# 3D Mathematics**: Fast vector, matrix, quaternion, and spatial transformation pipeline (`Math3D`) with zero unmanaged dependencies.
- **CAD Mesh Parsers**: Pure C# streaming parsers for binary/ASCII STL (`StlParser`) and Wavefront OBJ (`ObjParser`) meshes.
- **Spatial Safety Boundaries**: Workcell collision avoidance and keep-out zone enforcement (`TwinScene`, `SafetyZone`).

## Multi-Targeting

- `.NET 8.0+`
- `.NET Framework 4.6.2+`
- `.NET Standard 2.0`

## License

MIT License. Copyright © 2026 Phong Võ (`kzxl`).
