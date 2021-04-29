﻿using System.Collections;
using System.Collections.Generic;

using Unity.Entities;

[GenerateAuthoringComponent]
public struct CollisionBuffer : IBufferElementData
{
    public Entity entity;
}
