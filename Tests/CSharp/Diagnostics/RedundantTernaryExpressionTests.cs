using NUnit.Framework;
using RefactoringEssentials.CSharp.Diagnostics;

namespace RefactoringEssentials.Tests.CSharp.Diagnostics
{
    [TestFixture]
    public class RedundantTernaryExpressionTests : InspectionActionTestBase
    {
        [Test]
        public void TestTrueFalseCase()
        {
            Analyze<RedundantTernaryExpressionAnalyzer>(@"
class Foo
{
	void Bar ()
	{
		var a = 1 < 2 $? true : false$;
	}
}
", @"
class Foo
{
	void Bar ()
	{
		var a = 1 < 2;
	}
}
");
        }

        [Test]
        public void TestDisable()
        {
            Analyze<RedundantTernaryExpressionAnalyzer>(@"
class Foo
{
	void Bar ()
	{
#pragma warning disable " + DiagnosticIDs.RedundantTernaryExpressionAnalyzerID + @"
		var a = 1 < 2 ? true : false;
	}
}
");
        }

        [Test]
        public void TestExceptionCastNotValid()
        {
            Analyze<RedundantTernaryExpressionAnalyzer>(@"namespace Test
{
    class Foo
    {
        internal void Bar(string str)
        {
            var description = str.EndsWith(""a"") ? ""Add"" : ""Remove"";
        }
	}
}");
        }
    }
}
