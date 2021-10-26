
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RumineSimulator.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (RumineSimulator.Properties.Resources.resourceMan == null)
                    RumineSimulator.Properties.Resources.resourceMan = new ResourceManager("RumineSimulator.Properties.Resources", typeof(RumineSimulator.Properties.Resources).Assembly);
                return RumineSimulator.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => RumineSimulator.Properties.Resources.resourceCulture;
            set => RumineSimulator.Properties.Resources.resourceCulture = value;
        }
    }
}
