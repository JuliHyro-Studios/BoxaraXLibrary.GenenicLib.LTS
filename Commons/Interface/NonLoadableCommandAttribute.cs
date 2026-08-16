using System;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Interface
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class NonLoadableCommandAttribute : Attribute
    {
        public string Reason { get; }

        public NonLoadableCommandAttribute(string reason = "")
        {
            Reason = reason;
        }
    }
}