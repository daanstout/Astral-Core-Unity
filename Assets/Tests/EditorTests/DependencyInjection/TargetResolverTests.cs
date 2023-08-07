using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using NSubstitute;
using Astral.Core;

namespace Astral.Core.Test.DependencyInjection {
    [TestFixture]
    public class TargetResolverTests {
        private class TestTarget {
            public ITestInterface1 TestInterface1 => testInterface1;
            public ITestInterface2 TestInterface2 => testInterface2;
            public ITestInterface2 TestInterface2_2 => testInterface2_2;

            [Dependency] private readonly ITestInterface1 testInterface1;
            [Dependency] private readonly ITestInterface2 testInterface2;
            [Dependency] private readonly ITestInterface2 testInterface2_2;
        }

        private interface ITestInterface1 { }

        private interface ITestInterface2 { }

        private class TestClass1 : ITestInterface1 { }

        private class TestClass2 : ITestInterface2 { }


        private IServiceLocator serviceLocator;
        private TestClass1 testClass1Instance;
        private TestClass2 testClass2Instance;

        private TargetResolver<TestTarget> instance;

        [SetUp]
        public void SetUp() {
            serviceLocator = Substitute.For<IServiceLocator>();

            testClass1Instance = new TestClass1();
            testClass2Instance = new TestClass2();

            serviceLocator.Get(typeof(ITestInterface1)).Returns(testClass1Instance);
            serviceLocator.Get(typeof(ITestInterface2)).Returns(testClass2Instance);

            instance = new TargetResolver<TestTarget>();
        }

        [Test]
        public void Resolve_WhenCalledForFirstTime_CreatesInstanceAndInjects() {
            // Arrange

            // Act
            var result = instance.Resolve(serviceLocator);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TestInterface1, Is.EqualTo(testClass1Instance));
            Assert.That(result.TestInterface2, Is.EqualTo(testClass2Instance));
            Assert.That(result.TestInterface2_2, Is.EqualTo(testClass2Instance));
        }
    }
}