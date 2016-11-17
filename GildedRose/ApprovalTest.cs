using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace GildedRose
{
    [TestFixture]
    [UseReporter(typeof(NUnitReporter))]
    public class ApprovalTest
    {
        [Test]
        public void ThirtyDays()
        {
            StringBuilder fakeOutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeOutput));
            Console.SetIn(new StringReader("a\n"));

            Program.Main();
            string output = fakeOutput.ToString();
            Approvals.Verify(output);
        }
    }
}