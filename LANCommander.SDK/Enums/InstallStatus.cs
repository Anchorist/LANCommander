﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LANCommander.SDK.Enums
{
    public enum GameInstallStatus
    {
        Queued,
        Downloading,
        [Display(Name = "Installing Redistributables")]
        InstallingRedistributables,
        [Display(Name = "Installing Mods")]
        InstallingMods,
        [Display(Name = "Installing Expansions")]
        InstallingExpansions,
        [Display(Name = "Running Scripts")]
        RunningScripts,
        [Display(Name = "Downloading Saves")]
        DownloadingSaves,
        Canceled,
        Failed,
        Complete
    }
}
