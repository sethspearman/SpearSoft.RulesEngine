using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SpearSoft.RulesEngine
{
    public interface IRuleInvoker
    {
        void Invoke(object value, IValidationReport report, ValidationReportDepth depth);
        Type ParameterType { get; }

    }
}
