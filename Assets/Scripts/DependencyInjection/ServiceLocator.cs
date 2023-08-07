using System;
using System.Collections.Generic;

namespace Astral.Core {
    public class ServiceLocator : IServiceLocator {
        private readonly Dictionary<Type, ITargetResolver> resolvers = new Dictionary<Type, ITargetResolver>();

        public ServiceLocator() {
            Bind<IServiceLocator>().BindToInstance(this);
            Bind<ServiceLocator>().BindToInstance(this);
        }

        public void Inject(object obj) {

        }

        public T Get<T>() where T : class => Bind<T>().Resolve(this);

        public object Get(Type type) => Bind(type).Resolve(this);

        public ITargetResolver Bind(Type type) {
            if (!resolvers.TryGetValue(type, out ITargetResolver resolver)) {
                resolver = CreateResolver(type);
                resolvers[type] = resolver;
            }

            return resolver;
        }

        public ITargetResolver<T> Bind<T>() where T : class => (ITargetResolver<T>)Bind(typeof(T));

        public bool Contains(Type type) => resolvers.ContainsKey(type);

        private ITargetResolver CreateResolver(Type type) {
            if (type.IsInterface) {
                var implementations = Reflect.GetImplementingTypes(type);
                if (implementations.Length != 1)
                    throw new InvalidOperationException("Cannot create resolver for type with multiple implementations");

                type = implementations[0];
            }

            var resolverType = typeof(TargetResolver<>).MakeGenericType(type);
            return (ITargetResolver)Activator.CreateInstance(resolverType);
        }
    }
}
