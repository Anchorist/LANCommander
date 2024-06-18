﻿using LANCommander.Client.Data;
using LANCommander.Client.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LANCommander.Client.Services
{
    public class MultiplayerModeService : BaseDatabaseService<MultiplayerMode>
    {
        public MultiplayerModeService(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
