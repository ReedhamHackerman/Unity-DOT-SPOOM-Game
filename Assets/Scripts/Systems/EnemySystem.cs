using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class EnemySystem : SystemBase
{

    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random(1234);
    protected override void OnUpdate()
    {


        var movementRayCast = new MovementRaycast() { pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld};
        rng.NextUInt();
        var rngTemp = rng;

        Entities.ForEach((ref Moveable mov, ref Enemy enemy, in Translation translation) =>
        {
            if (math.distance(translation.Value, enemy.previousCell) > 0.9f)
            {
                
                var validDir =new NativeList<float3>(Allocator.Temp);
                enemy.previousCell = math.round(translation.Value);
                if (!movementRayCast.CheckRay(translation.Value, new float3(0, 0, -1), mov.direcation))
                    validDir.Add(new float3(0, 0, -1));
                if (!movementRayCast.CheckRay(translation.Value, new float3(0, 0, 1), mov.direcation))
                    validDir.Add(new float3(0, 0, 1));
                if (!movementRayCast.CheckRay(translation.Value, new float3(-1, 0, 0), mov.direcation))
                    validDir.Add(new float3(-1, 0, 0));
                if (!movementRayCast.CheckRay(translation.Value, new float3(1, 0, 0), mov.direcation))
                    validDir.Add(new float3(1, 0, 0));
                if (validDir.Length==0)
                {
                    return;
                }
                mov.direcation = validDir[rngTemp.NextInt(validDir.Length)];
                validDir.Dispose();
            }

        }).Schedule();
    }



    private struct MovementRaycast
    {
        [ReadOnly]public PhysicsWorld pw;
        public bool CheckRay(float3 position,float3 direction,float3 correctDirection)
        {
            if (direction.Equals(-correctDirection))
            {
                return true;
            }

            var ray = new RaycastInput()
            {
                Start = position,
                End = position + (direction * 0.8f),
                Filter = new CollisionFilter()
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 2,
                    CollidesWith = 1u << 1

                }

            };
            return pw.CastRay(ray);
        }
    }
}
