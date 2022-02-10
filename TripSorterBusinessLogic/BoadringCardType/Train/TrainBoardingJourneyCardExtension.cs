using System;
using System.Collections.Generic;
using System.Text;

namespace TripSorterBusinessLogic.BoadringCardType.Train
{
    public class TrainBoardingJourneyCardExtension : IBoardingJourneyCard
    {

        /// <summary>
        /// Creates the boarding journey card.
        /// </summary>
        /// <returns>
        /// BoardingJourneyCard
        /// </returns>
        public BoardingJourneyCard CreateBoardingJourneyCard()
        {
            return new TrainBoardingJourneyCard();
        }

    }
}
