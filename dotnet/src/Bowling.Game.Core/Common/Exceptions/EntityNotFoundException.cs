namespace Bowling.Game.Core.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type type, params object[] ids)
        : base($"Could not find entity of type {type.Name} with ids {string.Join(",", ids)}")
    {
        
    }
}

public class EntityNotFoundException<T> : EntityNotFoundException
{
    public EntityNotFoundException(object id)
        : base(typeof(T), id)
    {
        
    }
}