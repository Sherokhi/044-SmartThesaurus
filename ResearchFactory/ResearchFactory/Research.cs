﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchFactory
{
    interface Research
    {
        /// <summary>
        /// Classe utilisée pour la recherche
        /// </summary>
        /// <returns>Tous les résultats</returns>
        string[] start();
    }
}
