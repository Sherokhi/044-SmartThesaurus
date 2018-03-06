using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchFactory
{
    class ResearchFactory
    {
        /// <summary>
        /// Instancie une recherche
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Research CreateResearch(int mode)
        {
            switch (mode)
            {
                //Recherche ETML
                case 0:
                    return new RsearchETML();
                //Recherche K
                case 1:
                    return new ResearchK();
                //Recherche Educanet2
                case 2:
                    return new ResearchEducanet2();
                //Case default
                default:
                    throw new Exception("ERREUR");
            }
        }

        public List<Research> All()
        {
            List<Research> allResearchs = new List<Research>();
            allResearchs.Add(new RsearchETML());
            allResearchs.Add(new ResearchK());
            allResearchs.Add(new ResearchEducanet2());

            return allResearchs;
        }
    }
}
