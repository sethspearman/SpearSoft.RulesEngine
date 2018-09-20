using NUnit.Framework;
using RulesEngine.Fluent;

namespace RulesEngine.Tests
{
    [TestFixture]
    public class FluentBuilderTests
    {
        [Test]
        public void ShouldCreateEngine()
        {
            var builder = new FluentBuilder();
            builder.For<Foo>()
                    .Setup(m => m.Value)
                        .MustBeGreaterThan(10);

            var engine = builder.Build();
            Assert.IsTrue(engine.Validate(new Foo(11)));
            Assert.IsFalse(engine.Validate(new Foo(9)));

        }

        [Test]
        public void NodeTests()
        {
            var builder = new FluentBuilder();
            builder.For<Foo>()
                .Setup(m => m.Value)
                .Setup(m => m.Value)
                .EndSetup();
        }
    }
}
