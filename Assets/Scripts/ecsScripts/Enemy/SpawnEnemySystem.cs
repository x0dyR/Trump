using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace collegeGame
{
    public partial struct SpawnEnemySystem : ISystem
    {
        uint updateCounter;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyEntity>();    
            state.RequireForUpdate<Prefabs>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var enemyQuery = SystemAPI.QueryBuilder().WithAll<EnemyHealth>().Build();
            if(enemyQuery.IsEmpty)
            {
                var prefab = SystemAPI.GetSingleton<EnemyEntity>().Prefab;
                var instance = state.EntityManager.Instantiate(prefab, 10, Allocator.Temp);
                var random = Random.CreateFromIndex(updateCounter++);

                foreach(var entity in instance)
                {
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                    transform.ValueRW.Position = random.NextFloat3(12) - new float3(40, 0, -10);
                }
            }
        }

    }
}
