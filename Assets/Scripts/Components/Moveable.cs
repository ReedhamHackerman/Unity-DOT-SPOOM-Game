using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Moveable : IComponentData
{
    public float speed;
    public float3 direcation;
}
