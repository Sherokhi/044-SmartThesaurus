﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fillDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            FillK recherche = FillK.CreateResearch();
            recherche.BeginTheReasearch();
        }
    }
}
