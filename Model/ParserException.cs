﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    public class ParserException : Exception
    {
        public ParserException(string msg) : base(msg) { }
    }
}
