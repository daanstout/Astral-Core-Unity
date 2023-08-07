using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Astral.Core {
    public static class Reflect {
        private struct TypeAttributeInfo : IEquatable<TypeAttributeInfo> {
            public Type Type { get; set; }

            public Type AttributeType { get; set; }

            public TypeAttributeInfo(Type type, Type attribute) {
                Type = type;
                AttributeType = attribute;
            }

            public static implicit operator TypeAttributeInfo((Type type, Type attributeType) tuple) => new TypeAttributeInfo(tuple.type, tuple.attributeType);

            public override readonly bool Equals(object obj) => obj is TypeAttributeInfo info && Equals(info);
            public readonly bool Equals(TypeAttributeInfo other) => EqualityComparer<Type>.Default.Equals(Type, other.Type) && EqualityComparer<Type>.Default.Equals(AttributeType, other.AttributeType);
            public override readonly int GetHashCode() => HashCode.Combine(Type, AttributeType);

            public static bool operator ==(TypeAttributeInfo left, TypeAttributeInfo right) => left.Equals(right);
            public static bool operator !=(TypeAttributeInfo left, TypeAttributeInfo right) => !(left == right);
        }

        private struct TypeLookupInfo : IEquatable<TypeLookupInfo> {
            public Type Type { get; set; }

            public TypeFilter TypeFilter { get; set; }

            public TypeLookupInfo(Type type, TypeFilter typeFilter) {
                Type = type;
                TypeFilter = typeFilter;
            }

            public static implicit operator TypeLookupInfo((Type type, TypeFilter TypeFilter) tuple) => new TypeLookupInfo(tuple.type, tuple.TypeFilter);

            public override readonly bool Equals(object obj) => obj is TypeLookupInfo info && Equals(info);
            public readonly bool Equals(TypeLookupInfo other) => EqualityComparer<Type>.Default.Equals(Type, other.Type) && TypeFilter == other.TypeFilter;
            public override readonly int GetHashCode() => HashCode.Combine(Type, TypeFilter);

            public static bool operator ==(TypeLookupInfo left, TypeLookupInfo right) => left.Equals(right);
            public static bool operator !=(TypeLookupInfo left, TypeLookupInfo right) => !(left == right);
        }

        private static readonly Dictionary<Type, Type[]> interfaceImplementations = new Dictionary<Type, Type[]>();
        private static readonly HashSet<Type> types = new HashSet<Type>();
        private static readonly Dictionary<TypeAttributeInfo, FieldInfo[]> typeAttributeCache = new Dictionary<TypeAttributeInfo, FieldInfo[]>();
        private static readonly Dictionary<TypeLookupInfo, Type[]> ChildTypeCache = new Dictionary<TypeLookupInfo, Type[]>();

        static Reflect() {
            var assembly = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assemb in assembly) {
                foreach (var type in assemb.GetTypes()) {
                    types.Add(type);
                }
            }

            Dictionary<Type, List<Type>> interfaceImplementations = new Dictionary<Type, List<Type>>();

            foreach (var type in types) {
                if (type.IsInterface)
                    continue;

                foreach (var implementedInterface in type.GetInterfaces()) {
                    if (!interfaceImplementations.TryGetValue(implementedInterface, out List<Type> types)) {
                        types = new List<Type>();
                        interfaceImplementations.Add(implementedInterface, types);
                    }

                    types.Add(type);
                }
            }

            Reflect.interfaceImplementations = interfaceImplementations.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
        }

        public static Type[] GetImplementingTypes(Type type) => interfaceImplementations[type];

        public static FieldInfo[] GetFieldsWithAttribute<TType, TAttribute>() => GetFieldsWithAttribute(typeof(TType), typeof(TAttribute));

        public static FieldInfo[] GetFieldsWithAttribute(Type type, Type attribute) {
            if (!typeAttributeCache.TryGetValue((type, attribute), out FieldInfo[] result)) {
                List<FieldInfo> fields = new List<FieldInfo>();

                while (type != null) {
                    fields.AddRange(type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(field => field.GetCustomAttribute(attribute) != null).ToArray());

                    type = type.BaseType;
                }

                result = fields.ToArray();
                typeAttributeCache[(type, attribute)] = result;
            }

            return result;
        }

        public static Type[] GetTypesWithParentType<T>() => GetTypesWithParentType(typeof(T));

        public static Type[] GetTypesWithParentType(Type type, TypeFilter typeFilter = TypeFilter.All) {
            if (!ChildTypeCache.TryGetValue((type, typeFilter), out Type[] result)) {
                List<Type> foundTypes = new List<Type>();

                foreach (var t in types) {
                    if (!type.IsAssignableFrom(t))
                        continue;

                    if (t == type)
                        continue;

                    if (!typeFilter.HasFlag(TypeFilter.Interface) && t.IsInterface)
                        continue;

                    if (!typeFilter.HasFlag(TypeFilter.Abstract) && t.IsAbstract)
                        continue;

                    if (!typeFilter.HasFlag(TypeFilter.Instantiatable) && !(t.IsInterface || t.IsAbstract))
                        continue;

                    foundTypes.Add(t);
                }

                result = foundTypes.ToArray();
                ChildTypeCache[(type, typeFilter)] = result;
            }

            return result;
        }
    }
}
