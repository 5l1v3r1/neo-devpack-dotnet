using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.Compiler.MSIL.UnitTests.Utils;
using Neo.VM.Types;
using System.Linq;

namespace Neo.Compiler.MSIL.UnitTests
{
    [TestClass]
    public class UnitTest_Array
    {
        [TestMethod]
        public void Test_IntArray()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testIntArray");

            //test 0,1,2
            Assert.IsTrue(result.TryPop(out Array arr));
            CollectionAssert.AreEqual(new int[] { 0, 1, 2 }, arr.Cast<PrimitiveType>().Select(u => (int)u.ToBigInteger()).ToArray());
        }

        [TestMethod]
        public void Test_IntArrayInit()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testIntArrayInit");

            //test 1,4,5
            Assert.IsTrue(result.TryPop(out Array arr));
            CollectionAssert.AreEqual(new int[] { 1, 4, 5 }, arr.Cast<Integer>().Select(u => (int)u.ToBigInteger()).ToArray());
        }

        /* TODO: We should uncomment this when NEWARRAY_T was done
        [TestMethod]
        public void Test_DefaultArray()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("TestDefaultArray");

            //test true
            Assert.IsTrue(result.TryPop(out Boolean b) && b.ToBoolean());
        }
        */

        [TestMethod]
        public void Test_StructArray()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testStructArray");

            //test (1+5)*7 == 42
            var bequal = result.Pop() as Struct != null;
            Assert.IsTrue(bequal);
        }

        [TestMethod]
        public void Test_StructArrayInit()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testStructArrayInit");

            //test (1+5)*7 == 42
            var bequal = result.Pop() as Struct != null;
            Assert.IsTrue(bequal);
        }

        [TestMethod]
        public void Test_ByteArrayOwner()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testByteArrayOwner");

            var bts = result.Pop() as ByteArray;

            ByteArray test = new byte[] { 0xf6, 0x64, 0x43, 0x49, 0x8d, 0x38, 0x78, 0xd3, 0x2b, 0x99, 0x4e, 0x4e, 0x12, 0x83, 0xc6, 0x93, 0x44, 0x21, 0xda, 0xfe };
            Assert.IsTrue(ByteArray.Equals(bts, test));
        }

        [TestMethod]
        public void Test_ByteArrayOwnerCall()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testByteArrayOwnerCall");

            var bts = result.Pop().ConvertTo(StackItemType.ByteArray);

            ByteArray test = new byte[] { 0xf6, 0x64, 0x43, 0x49, 0x8d, 0x38, 0x78, 0xd3, 0x2b, 0x99, 0x4e, 0x4e, 0x12, 0x83, 0xc6, 0x93, 0x44, 0x21, 0xda, 0xfe };
            Assert.IsTrue(ByteArray.Equals(bts, test));
        }

        [TestMethod]
        public void Test_StringArray()
        {
            var testengine = new TestEngine();
            testengine.AddEntryScript("./TestClasses/Contract_Array.cs");
            var result = testengine.ExecuteTestCaseStandard("testSupportedStandards");

            var bts = result.Pop().ConvertTo(StackItemType.Array);
            var items = bts as VM.Types.Array;

            Assert.AreEqual((ByteArray)"NEP-5", items[0]);
            Assert.AreEqual((ByteArray)"NEP-10", items[1]);
        }
    }
}
