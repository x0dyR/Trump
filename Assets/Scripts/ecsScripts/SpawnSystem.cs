using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

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
            var enemyComponents = SystemAPI.GetSingleton<EnemyEntity>();

            var enemyEntity = state.EntityManager.Instantiate(enemyComponents.prefab);
        }
    }
    
}
