namespace Astral.Core {
    public interface ITargetResolver {
        void BindToInstance(object instance);
        object Resolve(IServiceLocator serviceLocator);
    }

    public interface ITargetResolver<T> : ITargetResolver
         where T : class {
        void BindToInstance(T instance);
        new T Resolve(IServiceLocator serviceLocator);
    }
}
