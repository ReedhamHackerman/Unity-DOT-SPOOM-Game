using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class PlayerSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Entities
            .WithAll<Player>()
            .ForEach((ref Moveable mov) => 
        {

            mov.direcation = new Vector3(y,0,-x);

        }).Schedule();
    }
}
