using System;
using System.Collections.Generic;
using System.Text;

namespace TripSorterBusinessLogic.BoadringCardType.Bus
{
    public class BusBoardingJourneyCardExtension : IBoardingJourneyCard
    {
        /// <summary>
        /// Creates the boarding journey card.
        /// </summary>
        /// <returns>BusBoardingJourneyCard</returns>
        public BoardingJourneyCard CreateBoardingJourneyCard()
        {
            return new BusBoardingJourneyCard();
        }

    }
}
