using Svelto.ECS;
using EtAlii.Them.Game.Physics.EntityComponents;

namespace EtAlii.Them.Game.Physics.Descriptors
{
    /// <summary>
    /// RigidBody EntityDescriptor, designed to be extended
    /// </summary>
    public class RigidBodyDescriptor : GenericEntityDescriptor<TransformEntityComponent, RigidbodyEntityComponent> { }
}