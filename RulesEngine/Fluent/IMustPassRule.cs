using System;
using System.Linq.Expressions;

namespace SpearSoft.RulesEngine.Fluent
{
    public interface IMustPassRule<M, T, R> : IFluentNode
    {
        M MustPassRule(IRule<R> rule);
        M MustPassRule(IRule<T> rule);
    }
}
