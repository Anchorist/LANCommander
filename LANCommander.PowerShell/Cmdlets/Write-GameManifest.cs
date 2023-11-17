﻿using LANCommander.SDK;
using LANCommander.SDK.Helpers;
using System.Management.Automation;

namespace LANCommander.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommunications.Write, "GameManifest")]
    [OutputType(typeof(string))]
    public class WriteGameManifestCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Path { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public GameManifest Manifest { get; set; }

        protected override void ProcessRecord()
        {
            var destination = ManifestHelper.Write(Manifest, Path);

            WriteObject(destination);
        }
    }
}
