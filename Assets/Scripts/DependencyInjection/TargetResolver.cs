using System;
using System.Reflection;

namespace Astral.Core {
    public class TargetResolver<T> : ITargetResolver<T>
        where T : class, new() {
        public bool IsSingleton { get; set; } = true;

        public Func<IServiceLocator, object[]> DependencyResolverFunc { get; set; }

        public object Instance {
            get => instance;
            set {
                instance = (T)value;
                IsSingleton = true;
            }
        }

        private T instance;
        private bool isResolving;
        private bool isResolved;

        public TargetResolver() {
            if (typeof(T).IsInterface || typeof(T).IsAbstract) {
                throw new InvalidOperationException($"Cannot resolve for objects of type {typeof(T).FullName}!");
            }
        }

        public void BindToInstance(object instance) {
            Reset();

            this.instance = (T)instance;
            isResolved = true;
            isResolving = false;
            IsSingleton = true;
        }

        public void BindToInstance(T instance) {
            Reset();

            this.instance = instance;
            isResolved = true;
            isResolving = false;
            IsSingleton = true;
        }

        public T Resolve(IServiceLocator serviceLocator) {
            if (IsSingleton && isResolved) {
                return this.instance;
            }

            if (isResolving) {
                throw new InvalidOperationException($" Circular dependency found! Already resolving for instance of type {typeof(T).FullName}!");
            }

            isResolving = true;

            var instance = new T();

            var fieldsToInject = Reflect.GetFieldsWithAttribute<T, DependencyAttribute>();

            var dependencies = ObtainDependencies(fieldsToInject, serviceLocator);

            SetDependencies(instance, fieldsToInject, dependencies);

            if (IsSingleton) {
                isResolved = true;
                this.instance = instance;
            }

            return instance;
        }

        object ITargetResolver.Resolve(IServiceLocator serviceLocator) => Resolve(serviceLocator);

        private void Reset() {
            IsSingleton = true;
            instance = null;
            isResolved = false;
            isResolving = false;
        }

        private object[] ObtainDependencies(FieldInfo[] fields, IServiceLocator serviceLocator) {
            if (DependencyResolverFunc != null) {
                return DependencyResolverFunc(serviceLocator);
            } else {
                object[] result = new object[fields.Length];

                for(int i = 0; i < fields.Length; i++) {
                    result[i] = serviceLocator.Get(fields[i].FieldType);
                }

                return result;
            }
        }

        private void SetDependencies(T instance, FieldInfo[] fields, object[] objects) {
            for(int i = 0; i < fields.Length; i++) {
                fields[i].SetValue(instance, objects[i]);
            }
        }
    }
}
