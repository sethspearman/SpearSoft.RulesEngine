using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.Specialized;
using SpearSoft.RulesEngine.Fluent;

namespace SpearSoft.RulesEngine
{
    public interface IEngineBuilder
    {
        IEngine Build();
    }
}
