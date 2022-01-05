﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Books.DiscountStrategies
{
    public interface IDiscountStrategy
    {
        public double calculate(double price);
    }
}
