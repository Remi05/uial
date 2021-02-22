using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Modules;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class ModuleDeclarationsParsingTests
    {
        private const string ModuleIdentifier = "module";
        private const string ModuleName = "TestModule";
        private const string AbsolutePath = @"C:\users\testuser\Desktop\somefolder\somefile.dll";
        private const string RelativePath_SameParent = @"..\somefolder\somefile.dll";
        private const string RelativePath_SameFolder = @"somefile.dll";
        private const string RelativePath_ForwardSlash = @"../somefolder/somefile.dll";

        private class ValidModules
        {
            public const string BinaryWithAbsolutePath   = ModuleIdentifier + " " + ModuleName + "(\"" + AbsolutePath + "\")";
            public const string BinaryInSameParentFolder = ModuleIdentifier + " " + ModuleName + "(\"" + RelativePath_SameParent + "\")";
            public const string BinaryInSameFolder       = ModuleIdentifier + " " + ModuleName + "(\"" + RelativePath_SameFolder + "\")";
            public const string BinaryPathForwardSlash   = ModuleIdentifier + " " + ModuleName + "(\"" + RelativePath_ForwardSlash + "\")";
        }

        private class InvalidModules
        {
            public const string MissingName       = ModuleIdentifier + " (\"" + AbsolutePath + "\")";
            public const string MissingBinaryPath = ModuleIdentifier + " " + ModuleName + "()";
            public const string EmptyBinaryPath   = ModuleIdentifier + " " + ModuleName + "(\"\")";
        }


        [TestMethod]
        public void ModuleNameIsParsed()
        {
            ScriptParser parser = new ScriptParser();
            ModuleDefinition moduleDefinition = parser.ParseModuleDefinition(ValidModules.BinaryInSameFolder);

            Assert.IsNotNull(moduleDefinition, "The parsed ModuleDefinition should not be null.");
            Assert.AreEqual(ModuleName, moduleDefinition.Name, "The parsed ModuleDefinition's Name should be the given name.");
        }

        [DataRow(ValidModules.BinaryWithAbsolutePath,   AbsolutePath,              DisplayName = "ValidModule_BinaryWithAbsolutePath")]
        [DataRow(ValidModules.BinaryInSameParentFolder, RelativePath_SameParent,   DisplayName = "ValidModule_BinaryInSameParentFolder")]
        [DataRow(ValidModules.BinaryInSameFolder,       RelativePath_SameFolder,   DisplayName = "ValidModule_BinaryInSameFolder")]
        [DataRow(ValidModules.BinaryPathForwardSlash,   RelativePath_ForwardSlash, DisplayName = "ValidModule_BinaryPathForwardSlash")]
        [DataTestMethod]
        public void ModuleBinaryPathIsParsed(string moduleDefinitionStr, string expectedBinaryPath)
        {
            ScriptParser parser = new ScriptParser();
            ModuleDefinition moduleDefinition = parser.ParseModuleDefinition(moduleDefinitionStr);

            Assert.IsNotNull(moduleDefinition, "The parsed ModuleDefinition should not be null.");
            Assert.AreEqual(expectedBinaryPath, moduleDefinition.BinaryPath, "The parsed ModuleDefinition's BinaryPath should be the given binary path.");
        }

        [DataRow(InvalidModules.MissingName,       DisplayName = "InvalidModules_MissingName")]
        [DataRow(InvalidModules.MissingBinaryPath, DisplayName = "InvalidModules_MissingBinaryPath")]
        [DataRow(InvalidModules.EmptyBinaryPath,   DisplayName = "InvalidModules_EmptyBinaryPath")]
        [DataTestMethod]
        public void InvalidModulesThrowException(string moduleDefinitionStr)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidModuleDeclarationException>(() => parser.ParseModuleDefinition(moduleDefinitionStr));
        }
    }
}
