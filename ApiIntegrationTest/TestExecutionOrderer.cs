using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ApiIntegrationTest;

    public class TestExecutionOrderer: ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
      where TTestCase : ITestCase
        {
            var sorted = testCases.OrderBy(tc =>
            {
                var attr = tc.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName!)
                    .FirstOrDefault();

                return attr == null ? 0 : attr.GetNamedArgument<int>("Priority");
            });

            return sorted;
        }
    }

