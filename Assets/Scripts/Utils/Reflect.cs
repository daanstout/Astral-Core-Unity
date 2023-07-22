using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace Core {
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

        private static readonly Dictionary<Type, Type[]> interfaceImplementations = new Dictionary<Type, Type[]>();
        private static readonly HashSet<Type> types = new HashSet<Type>();
        private static readonly Dictionary<TypeAttributeInfo, FieldInfo[]> typeAttributeCache = new Dictionary<TypeAttributeInfo, FieldInfo[]>();

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
                result = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(field => field.GetCustomAttribute(attribute) != null).ToArray();
                typeAttributeCache[(type, attribute)] = result;
            }

            return result;
        }
    }
}
