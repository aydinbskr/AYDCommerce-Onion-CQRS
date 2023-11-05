﻿using AYDCommerce.API.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Exceptions
{
    public class NotBeSameException : BaseException
    {
        public NotBeSameException(string message) : base(message) { }
    }
}
