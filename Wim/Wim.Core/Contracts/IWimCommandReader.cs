﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimCommandReader
    {
        IList<ICommand> ReadCommands();
    }
}
