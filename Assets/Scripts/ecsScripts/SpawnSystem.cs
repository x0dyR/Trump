using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace collegeGame
{
    public partial struct SpawnSystem : ISystem
    {
        uint updateCounter;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Spawner>();
            state.RequireForUpdate<Prefabs>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var spinningCubeQuery = SystemAPI.QueryBuilder().WithAll<RotationSpeed>().Build();

            if (spinningCubeQuery.IsEmpty)
            {
                var prefab = SystemAPI.GetSingleton<Spawner>().Prefab;
                var instance = state.EntityManager.Instantiate(prefab, 50000, Allocator.Temp);
                var random = Random.CreateFromIndex(updateCounter++);

                foreach (var entity in instance)
                {
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                    transform.ValueRW.Position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;
                }
            }
        }
    }
}
