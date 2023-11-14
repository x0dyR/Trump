using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace collegeGame
{
    public partial struct SpawnSystem : ISystem
    {

        public void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<EnemyHealth>();
            state.RequireForUpdate<EnemyLevel>();
            state.RequireForUpdate<EnemyPosition>();
            state.RequireForUpdate<PlayerHealth>();
            state.RequireForUpdate<PlayerLevel>();
            state.RequireForUpdate<PlayerPosition>();
        }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var rand = new Random(123);
            var enemyComponents = SystemAPI.GetSingleton<EnemyPosition>();
            var enemyPrefab = SystemAPI.GetSingleton<EnemyEntity>();
            var enemyScale = SystemAPI.GetSingleton<EnemyScale>();
            

            var enemyEntity = state.EntityManager.Instantiate(enemyPrefab.prefab);
            state.EntityManager.SetComponentData(enemyEntity, new LocalTransform
            {
                Position = new float3
                {
                    x = rand.NextFloat(enemyComponents.offsetSpawn),
                    y=0,
                    z = rand.NextFloat(enemyComponents.offsetSpawn)
                },
                Scale = enemyScale.scale,
                Rotation = quaternion.identity
            });
        }
    }
    
}
