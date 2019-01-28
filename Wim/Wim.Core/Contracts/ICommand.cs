﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    interface ICommand
    {
        string Name { get; }

        IList<string> Parameters { get; }
    }
}
