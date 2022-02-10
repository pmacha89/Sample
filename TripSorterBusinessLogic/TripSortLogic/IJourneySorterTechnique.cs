using System;
using System.Collections.Generic;
using System.Text;
using TripSorterBusinessLogic.BoadringCardType;

namespace TripSorterBusinessLogic.TripSortLogic
{
    public interface IJourneySorterTechnique
    {
        /// <summary>
        /// Sorts the journey.
        /// It allows to obtain the information contained in a boarding pass.
        /// Depending on the type of transport, the information displayed will be different.
        /// </summary>
        /// <param name="BoardingJourneyCards">The boarding journey cards.</param>
        /// <returns>List<BoardingJourneyCard></returns>
        List<BoardingJourneyCard> SortJourney(List<BoardingJourneyCard> BoardingJourneyCards);
    }
}
