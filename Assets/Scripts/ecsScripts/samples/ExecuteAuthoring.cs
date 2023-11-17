using Unity.Entities;
using UnityEngine;

namespace collegeGame
{
    public class ExecuteAuthoring : MonoBehaviour
    {
        public bool MainThread;
        public bool IJobEntity;
        public bool Aspects;
        public bool Prefabs;
        public bool IJobChunk;
        public bool GameObjectSync;
        public bool Reparenting;
        public bool EnableableComponents;
        class Baker : Baker<ExecuteAuthoring>
        {
            public override void Bake(ExecuteAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent<Prefabs>(entity);
                AddComponent<IJobChunkasd>(entity);
            }
        }
    }
    public struct MainThread : IComponentData { }
    public struct IJobEntityasd : IComponentData { }
    public struct Aspectsasd : IComponentData { }
    public struct Prefabs : IComponentData { }
    public struct IJobChunkasd : IComponentData { }
    public struct GameObjectSync : IComponentData { }
    public struct Reparenting : IComponentData { }
    public struct EnableableComponents : IComponentData { }
}
