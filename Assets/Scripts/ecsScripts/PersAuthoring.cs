using Unity.Entities;
using UnityEngine;


namespace collegeGame
{

    public class PersAuthoring : MonoBehaviour
    {
        public float speed;
//        public Collider collider;
    }

    public class PersAuthoringBaker : Baker<PersAuthoring>
    {
        public override void Bake(PersAuthoring authoring)
        {
            var _player = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerMoveInput>(_player);
            AddComponent(_player, new PlayerMoveSpeed
            {
                Value = authoring.speed
            });
/*            AddComponent(_player, new PlayerCollider
            {
                collider = authoring.collider
            });*/

        }
    }
}