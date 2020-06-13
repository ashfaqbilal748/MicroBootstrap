using System;

namespace MicroBootstrap.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
    public interface IIdentifiable : IIdentifiable<Guid>
    {
    }

}