using ICSharpCode.Decompiler.Ast;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSharpCode.Decompiler.Tests.Wk
{
    public class Test
    {
        public String A { set; get; }
    }

    public class SimpleDecompileSpec
    {
        [Test]
        public void ShouldDecompileDiffLibDll()
        {
            var pathToAssembly = "../../../ICSharpCode.Decompiler/bin/Debug/DiffLib.dll"; 
            //var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(pathToAssembly);
            var def = Mono.Cecil.AssemblyDefinition.ReadAssembly(pathToAssembly);

            AstBuilder decompiler = new AstBuilder(new DecompilerContext(def.MainModule));
            decompiler.AddAssembly(def);
            new Helpers.RemoveCompilerAttribute().Run(decompiler.SyntaxTree);

            StringWriter output = new StringWriter();
            decompiler.GenerateCode(new PlainTextOutput(output));
            var str = output.ToString();
        }
    }
}
