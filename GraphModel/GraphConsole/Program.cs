using System;
using System.Runtime.InteropServices;
using LLVMSharp;
using GraphModelLibrary;

namespace GraphConsole {
	class Program {
		static void Main(string[] args) {
			LLVMBool False = new LLVMBool(0);
			LLVMModuleRef module = LLVM.ModuleCreateWithName("LLVMSharpIntro");

			LLVMTypeRef[] param_types = { LLVM.Int32Type(), LLVM.Int32Type() };
			LLVMTypeRef func_type = LLVM.FunctionType(LLVM.Int32Type(), param_types, false);
			LLVMValueRef sum = LLVM.AddFunction(module, nameof(sum), func_type);

			LLVMBasicBlockRef block = LLVM.AppendBasicBlock(sum, nameof(block));

			LLVMBuilderRef builder = LLVM.CreateBuilder();
			LLVM.PositionBuilderAtEnd(builder, block);
			LLVMValueRef tmp = LLVM.BuildAdd(builder, 
				LLVM.GetParam(sum, 0), LLVM.GetParam(sum, 1), nameof(tmp));
			LLVM.BuildRet(builder, tmp);

			IntPtr error;
			LLVM.VerifyModule(module, LLVMVerifierFailureAction.LLVMAbortProcessAction, out error);
			LLVM.DisposeMessage(error);

			LLVMExecutionEngineRef engine;

			LLVM.LinkInMCJIT();
			LLVM.InitializeNativeTarget();
			LLVM.InitializeNativeAsmPrinter();

			var options = new LLVMMCJITCompilerOptions();
			var options_size = (4 * sizeof(int)) + IntPtr.Size;

			LLVM.InitializeMCJITCompilerOptions(out options, options_size);
			LLVM.CreateMCJITCompilerForModule(out engine, module, out options, options_size, out error);

			var global = LLVM.GetPointerToGlobal(engine, sum);
			var addMethod = (Add)Marshal.GetDelegateForFunctionPointer(global, typeof(Add));

			int a, b, result;
			string[] strs = Console.ReadLine().Split();
			if (int.TryParse(strs[0], out a) && int.TryParse(strs[1], out b)) {
				result = addMethod(a, b);
			}
			else {
				Console.WriteLine("Fail");
				return;
			}

			Console.WriteLine("{0} + {1} = {2}", a, b, result);

			LLVM.DumpModule(module);

			LLVM.DisposeBuilder(builder);
			LLVM.DisposeExecutionEngine(engine);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int Add(int a, int b);
	}
}
