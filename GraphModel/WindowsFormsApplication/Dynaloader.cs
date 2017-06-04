using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication {
	class Dynaloader {
		private IntPtr m_dll = IntPtr.Zero;

        public Dynaloader()
        {
        }

        public Dynaloader(Form1 form) {
		}

		public Dynaloader(string dll_name) {
			load(dll_name);
		}

		~Dynaloader() {
			if (loaded())
				unload();
		}

		public bool loaded() {
			return m_dll != IntPtr.Zero;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllToLoad);

		public bool load(string name) {
			if (loaded())
				unload();
			m_dll = LoadLibrary(name);
			return loaded();
		}

		[DllImport("kernel32.dll")]
		private static extern bool FreeLibrary(IntPtr hModule);

		public bool unload() {
			if (!loaded())
				return true;
			if (!FreeLibrary(m_dll))
				return false;
			m_dll = IntPtr.Zero;
			return true;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		public T load_function<T>(string name) where T : class {
			IntPtr address = GetProcAddress(m_dll, name);
			if (address == IntPtr.Zero)
				return null;
			System.Delegate fn_ptr = Marshal.GetDelegateForFunctionPointer(address, typeof(T));
			return fn_ptr as T;
		}
	}
}
