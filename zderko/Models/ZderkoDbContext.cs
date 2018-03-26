﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace zderko.Models
{
    public class ZderkoDbContext : IdentityDbContext<User>
    {

        public ZderkoDbContext() : base("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\mstub\\source\\repos\\zderko\\zderko\\App_Data\\zderko.mdf;Integrated Security = True")
        {
        }

        public static ZderkoDbContext Create()
        {
            return new ZderkoDbContext();
        }

    }
}