using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;

public class CollisionSystem : SystemBase
{
    private struct CollisionSystemJob : ICollisionEventsJob
    {
        public BufferFromEntity<CollisionBuffer> collisions;
        public void Execute(CollisionEvent collisionEvent)
        {
            if (collisions.Exists(collisionEvent.EntityA))
            {
                collisions[collisionEvent.EntityA].Add(new CollisionBuffer() { entity = collisionEvent.EntityB });
            }
            if (collisions.Exists(collisionEvent.EntityB))
            {
                collisions[collisionEvent.EntityB].Add(new CollisionBuffer() { entity = collisionEvent.EntityA });
            }
        }
    }

    //private struct TriggerSystemJob : ITriggerEventsJob
    //{
    //    public void Execute(TriggerEvent triggerEvent)
    //    {
           
    //    }
    //}


    protected override void OnUpdate()
    {
         var pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
         var sim = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

        Entities.ForEach((DynamicBuffer<CollisionBuffer> collisions) => {

            collisions.Clear();
        }
        ).Run();

         var colJobHandle =  new CollisionSystemJob()
         { 
             collisions = GetBufferFromEntity<CollisionBuffer>()
         }.Schedule(sim,ref pw,this.Dependency);
         colJobHandle.Complete();

    }
   
}
