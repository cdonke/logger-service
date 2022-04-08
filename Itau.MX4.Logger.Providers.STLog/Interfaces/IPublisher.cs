﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.STLog.Interfaces
{
    internal interface IPublisher
    {
        static event LogEntityHandler LogEntityEnqueued;
        void Postar(string mensagem);
    }
}