﻿using System.Collections.Generic;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface IRateService
    {
        IEnumerable<BLLRate> GetUserRates(int userId);
        IEnumerable<BLLRate> GetLotRates(int lotId);
        BLLRate GetLotLastRate(int lotId);
    }
}