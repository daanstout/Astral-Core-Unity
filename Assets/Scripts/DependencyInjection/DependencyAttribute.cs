using System;

namespace Core {
    [AttributeUsage(AttributeTargets.Field)]
    public class DependencyAttribute : Attribute {
        public string Identifier { get; } = null;

        public DependencyAttribute(string identifier = null) {
            Identifier = identifier;
        }
    }
}
