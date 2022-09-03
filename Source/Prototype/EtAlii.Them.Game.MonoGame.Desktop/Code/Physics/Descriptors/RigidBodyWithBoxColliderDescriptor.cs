using Svelto.ECS;
using EtAlii.Them.Game.Physics.EntityComponents;

namespace EtAlii.Them.Game.Physics.Descriptors
{
        public class RigidBodyWithBoxColliderDescriptor : ExtendibleEntityDescriptor<RigidBodyDescriptor>
        {
            public RigidBodyWithBoxColliderDescriptor() : base(new IComponentBuilder[]
            {
                new ComponentBuilder<BoxColliderEntityComponent>()
            }) { }
        }
}