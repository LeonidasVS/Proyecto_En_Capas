﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class DataBase
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NWConnection"].ConnectionString;
            }
        }
    }
}
