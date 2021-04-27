using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveableSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        Entities.ForEach((ref Moveable mov,ref Translation translation,ref Rotation rot) =>
        {
            translation.Value += mov.speed * mov.direcation * dt;
            rot.Value = math.mul(rot.Value.value, quaternion.RotateY(mov.speed * dt));
        }).Schedule();
    }
}
