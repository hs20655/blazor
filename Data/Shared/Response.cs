﻿using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class Response<T> 
    {
        public T? Result { get; set; }
    }
}
