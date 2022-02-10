using System;
using System.Collections.Generic;
using System.Text;

namespace TripSorterBusinessLogic.BoadringCardType.Aeroplane
{
    public class AirplaneBoardingJourneyCardExtension : IBoardingJourneyCard
    {
        /// <summary>
        /// Creates the boarding journey card.
        /// </summary>
        /// <returns>AirplaneBoardingJourneyCard</returns>
        public BoardingJourneyCard CreateBoardingJourneyCard()
        {
            return new AirplaneBoardingJourneyCard();
        }

    }
}
