﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class MarketException:Exception
    {

        public MarketException()
        {
                
        }
        public MarketException(string mesa):base(mesa)
        {
                
        }
    }
}
