﻿using EzBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzBuy.Services.Contracts
{
    public interface ICartService
    {
        public Task CheckOut(User user, decimal amount);
    }
}
