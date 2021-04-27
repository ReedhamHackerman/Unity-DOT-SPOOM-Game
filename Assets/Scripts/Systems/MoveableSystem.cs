using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
public class MoveableSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        //in means readonly and ALways Ref come first After In can be Declared 
        float dt = Time.DeltaTime;
        Entities.ForEach((ref PhysicsVelocity phyVelo, in Moveable mov) =>
        {
            var step = mov.direcation * mov.speed;
            phyVelo.Linear = step;

        }).Schedule();
    }
}
