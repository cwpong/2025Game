using System.Reflection;
using System.Runtime.Loader;
using Model;

namespace Fantasy
{
    public static class AssemblyHelper
    {
        private const string HotfixDll = "Hotfix";
        private static AssemblyLoadContext? _assemblyLoadContext = null;
        
        public static System.Reflection.Assembly[] Assemblies
        {
            get
            {
                var assemblies = new System.Reflection.Assembly[3];
                assemblies[0] = LoadEntityAssembly();
                assemblies[1] = LoadHotfixAssembly();
                assemblies[2] = LoadModleAssembly();
                return assemblies;
            }
        }
        
        private static System.Reflection.Assembly LoadEntityAssembly()
        {
            return typeof(AssemblyHelper).Assembly;
        }

        private static System.Reflection.Assembly LoadModleAssembly()
        {
            return typeof(ModelHelper).Assembly;
        }
        
        private static System.Reflection.Assembly LoadHotfixAssembly()
        {
            if (_assemblyLoadContext != null)
            {
                _assemblyLoadContext.Unload();
                System.GC.Collect();
            }

            _assemblyLoadContext = new AssemblyLoadContext(HotfixDll, true);
            var dllBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, $"{HotfixDll}.dll"));
            var pdbBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, $"{HotfixDll}.pdb"));
            return _assemblyLoadContext.LoadFromStream(new MemoryStream(dllBytes), new MemoryStream(pdbBytes));
        }
    }
}