using BBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBot.Services
{
     interface IStorage
    {
        Session GetSession(long chatId);
    }
}
