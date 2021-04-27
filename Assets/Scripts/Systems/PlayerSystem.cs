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
        Entities.ForEach((ref Moveable mov,in Player player) => {

            mov.direcation = new Vector3(x,0,y);

        }).Schedule();
    }
}
