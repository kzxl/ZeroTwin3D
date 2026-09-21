# ZeroTwin3D: Pure C# 3D Digital Twin & Robot Kinematics for .NET

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET Multi-Targeting](https://img.shields.io/badge/.NET-8.0%20%7C%204.6.2%20%7C%20Standard%202.0-purple.svg)](https://dotnet.microsoft.com/)

**ZeroTwin3D** is a lightweight, pure C# 3D digital twin engine, robotic kinematics solver, and spatial safety boundary supervisor for .NET.

## Key Features

- **3D Spatial Camera & View Frustum Culling**: Full Perspective & Orthographic projection support (`Camera3D`), 6-plane frustum extraction, and high-performance AABB view-frustum culling.
- **Orbit / Arcball Camera Controller**: Intuitive CAD model inspection (`OrbitCameraController`) with spherical yaw/pitch orbiting, boundary clamping, distance zooming, and screen-aligned panning.
- **Inverse Kinematics (CCD-IK)**: Cyclic Coordinate Descent solver (`NumericalInverseKinematics`) for $N$-DOF articulated robots and SCARA manipulators with joint limit constraints.
- **Multi-Joint Trajectory Planner**: Smooth kinematic path generation (`TrajectoryPlanner`) supporting Cubic Hermite Spline ($C^1$ continuity), smooth S-curve Trapezoidal, and Linear velocity profiles.
- **PBR & Blinn-Phong Lighting**: Directional, Point, Spot, and Ambient light sources (`Light3D`) with surface shading (`Material3D`) and standard industrial presets (IndustrialSteel, RobotOrange, SafetyYellow, SafetyRed, Glass).
- **Scene Graph Rendering Engine**: Hierarchical spatial tree traversal (`TwinSceneRenderer`), opaque front-to-back and transparent back-to-front queue sorting, and draw command batching (`RenderCommand`).
- **Robotic Kinematics Engine**: Analytical Denavit-Hartenberg (DH) forward kinematics solver (`RobotKinematics`) supporting 6-DOF articulated arms and 4-DOF SCARA robots.
- **Pure C# 3D Mathematics**: Fast vector, matrix, quaternion, and spatial transformation pipeline (`Math3D`) with zero unmanaged dependencies.
- **CAD Mesh Parsers**: Pure C# streaming parsers for binary/ASCII STL (`StlParser`) and Wavefront OBJ (`ObjParser`) meshes.
- **Spatial Safety Boundaries**: Real-time PLC telemetry synchronization and automated workcell exclusion zone breach detection (`TwinScene`, `TwinTelemetryBridge`).

## Multi-Targeting

- `.NET 8.0+`
- `.NET Framework 4.6.2+`
- `.NET Standard 2.0`

## License

MIT License. Copyright © 2026 Phong Võ (`kzxl`).
