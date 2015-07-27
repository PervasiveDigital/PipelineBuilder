// Guids.cs
// MUST match guids.h
using System;

namespace PervasiveDigitalLLC.VsPipelineBuilderPkg
{
    static class GuidList
    {
        public const string guidVsPipelineBuilderPkgPkgString = "769338a1-18fa-4184-af2e-29ed5be1d61e";
        public const string guidVsPipelineBuilderPkgCmdSetString = "4a3176c6-0642-470f-ad34-53575292b317";

        public static readonly Guid guidVsPipelineBuilderPkgCmdSet = new Guid(guidVsPipelineBuilderPkgCmdSetString);
    };
}