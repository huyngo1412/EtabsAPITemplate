using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EtabsAPIPlugin.Infrastructure.Bootstrap
{
    public static class AssemblyResolver
    {
        private static bool _registered;

        public static void Register()
        {
            if (_registered) return;
            _registered = true;

            AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
            {
                var name = new AssemblyName(args.Name).Name + ".dll";
                var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var path = Path.Combine(baseDir!, name);

                return File.Exists(path) ? Assembly.LoadFrom(path) : null;
            };
        }
    }
}
