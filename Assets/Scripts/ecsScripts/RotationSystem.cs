using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Assertions;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace collegeGame
{
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //state.RequireForUpdate<MainThread>(); mainThread example
            //state.RequireForUpdate<IJobEntityasd>(); IJobEntity example
            //state.RequireForUpdate<Aspectsasd>();  Aspects example
            state.RequireForUpdate<IJobChunkasd>(); // IJobChunk example
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            /* MainThread example
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                transform.ValueRW = transform.ValueRO.RotateY(
                    speed.ValueRO.Radians * deltaTime);
            }*/
            /*IJobEntity example
             var job = new RotationJob { deltaTime = SystemAPI.Time.DeltaTime };
            state.Dependency = job.Schedule(state.Dependency);*/
            /* Prefab example require Aspect
            float deltaTime = SystemAPI.Time.DeltaTime;
            double elapsedTime = SystemAPI.Time.ElapsedTime;
            foreach(var (transform,speed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<RotationSpeed>>())
            {
                transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.Radians * deltaTime);
            }
            foreach(var movement in SystemAPI.Query<VerticalMovementAspect>())
            {
                movement.Move(elapsedTime);
            }*/
            var spinningCubeQuery = SystemAPI.QueryBuilder().WithAll<RotationSpeed, LocalTransform>().Build();

            var job = new RotationJob
            {
                TransformTypeHandle = SystemAPI.GetComponentTypeHandle<LocalTransform>(),
                RotationSpeedTypeHandle = SystemAPI.GetComponentTypeHandle<RotationSpeed>(),
                deltaTime = SystemAPI.Time.DeltaTime
            };

            state.Dependency = job.ScheduleParallel(spinningCubeQuery, state.Dependency);

        }
    }
    /*IJobEntity example
     public partial struct RotationJob : IJobEntity
    {
        public float deltaTime;
        void Execute(ref LocalTransform transform, in RotationSpeed speed)
        {
            transform = transform.RotateY(speed.Radians * deltaTime);
        }
    }*/
    /* IAspect + Prefab example
    readonly partial struct VerticalMovementAspect : IAspect
    {
        readonly RefRW<LocalTransform> m_Transform;
        readonly RefRO<RotationSpeed> m_Speed;
        public void Move(double elapsedTime)
        {
            m_Transform.ValueRW.Position.y = (float)math.sin(elapsedTime * m_Speed.ValueRO.Radians);
        }
    }*/
    [BurstCompile]
    struct RotationJob : IJobChunk
    {
        public ComponentTypeHandle<LocalTransform> TransformTypeHandle;
        public ComponentTypeHandle<RotationSpeed> RotationSpeedTypeHandle;
        public float deltaTime;
        public void Execute(
            in ArchetypeChunk chunk,
            int unfileteredChunkIndex,
            bool useEnabledMask,
            in v128 chunkEnabledMask)
        {
            Assert.IsFalse(useEnabledMask);
            var transforms = chunk.GetNativeArray(ref TransformTypeHandle);
            var rotationSpeeds = chunk.GetNativeArray(ref RotationSpeedTypeHandle);

            for(int i = 0,chunkEntityCount = chunk.Count;i < chunkEntityCount;i++)
            {
                transforms[i] = transforms[i].RotateY(rotationSpeeds[i].Radians * deltaTime);
            }
        }
    }
}
