using System;

namespace Common.Types
{
    public interface IIdentifiable
    {
         Guid Id { get; }
    }
}