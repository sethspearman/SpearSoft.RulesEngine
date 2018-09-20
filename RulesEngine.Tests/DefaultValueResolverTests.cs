using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace SpearSoft.RulesEngine.Tests
{
    [TestFixture]
    public class DefaultValueResolverTests
    {
        private class Foo
        {
            public Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            public object[] Array;
            public Foo Composition;
        }

        [Test]
        public void ShouldResolveValueFromDictionary()
        {
            var foo = new Foo();
            var obj = new object();
            foo.Dictionary["test1"] = obj;

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Dictionary["test1"]);
            object result;
            Assert.IsTrue(resolver.TryGetValue(foo, out result));
            Assert.AreSame(obj, result);
        }

        [Test]
        public void ShouldIgnoreKeyNotFoundExceptions()
        {
            var foo = new Foo();
            var obj = new object();
            foo.Dictionary["test1"] = obj;

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Dictionary["test2"]);
            object result;
            Assert.IsFalse(resolver.TryGetValue(foo, out result));
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldResolveValueFromArray()
        {
            var foo = new Foo();
            var obj = new object();
            foo.Array = new[] { obj };

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Array[0]);
            object result;
            Assert.IsTrue(resolver.TryGetValue(foo, out result));
            Assert.AreSame(obj, result);
        }

        [Test]
        public void ShouldIgnoreIndexOutOfRange()
        {
            var foo = new Foo();
            var obj = new object();
            foo.Array = new[] { obj };

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Array[1]);

            object result;
            Assert.IsFalse(resolver.TryGetValue(foo, out result));
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldResolveValueFromComposedExpression()
        {
            var foo = new Foo();
            var obj = new Foo();
            foo.Composition = obj;

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Composition.Dictionary);
            object result;
            Assert.IsTrue(resolver.TryGetValue(foo, out result));
            Assert.AreSame(obj.Dictionary, result);
        }

        [Test]
        public void ShouldIgnoreNullReferenceExceptions()
        {
            var foo = new Foo();
            foo.Composition = null;

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Composition.Dictionary);

            object result;
            Assert.IsFalse(resolver.TryGetValue(foo, out result));
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldImplementIValueResolver()
        {
            var foo = new Foo();
            var obj = new Foo();
            foo.Composition = obj;

            var resolver = new DefaultValueResolver<Foo, object>(f => f.Composition.Dictionary);
            object result;
            Assert.IsTrue(((IValueResolver)resolver).TryGetValue(foo, out result));
            Assert.AreSame(obj.Dictionary, result);
        }

    }
}
