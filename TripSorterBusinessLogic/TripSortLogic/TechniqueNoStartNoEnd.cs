using System;
using System.Collections.Generic;
using System.Text;
using TripSorterBusinessLogic.BoadringCardType;

namespace TripSorterBusinessLogic.TripSortLogic
{
    public class TechniqueNoStartNoEnd : IJourneySorterTechnique
    {

        /// <summary>
        /// The departures cards
        /// </summary>
        private Dictionary<String, BoardingJourneyCard> departuresCards = new Dictionary<string, BoardingJourneyCard>();
        /// <summary>
        /// The destinations cards
        /// </summary>
        private Dictionary<String, BoardingJourneyCard> destinationsCards = new Dictionary<string, BoardingJourneyCard>();

        /// <summary>
        /// Sorts the journey.
        /// </summary>
        /// <param name="BoardingJourneyCards">The boarding journey cards.</param>
        /// <returns></returns>
        public List<BoardingJourneyCard> SortJourney(List<BoardingJourneyCard> BoardingJourneyCards)
        {
            //
            // First, you have to find out which is the boarding card where you start
            // the trip, and from there we will know the itinerary ordered, since
            // each boarding pass contains the place of departure and arrival.
            //

            // We build the dictionaries or tables necessary to perform the operations
            // required for this sorting algorithm            
            BuildDeparturesDestinationsTables(BoardingJourneyCards);

            BoardingJourneyCard initialBoardingJourneyCard = GetInitialBoardingJourneyCard();

            List<BoardingJourneyCard> sortedBoardingJourneyCards = SortBoardingJourneyCards(initialBoardingJourneyCard);

            return sortedBoardingJourneyCards;
        }

        /// <summary>
        /// Builds the departures destinations tables.
        /// Build four dictionaries or tables with key-value pairs to map
                /// boarding passes and places of origin and destination of the trip.
        /// </summary>
        /// <param name="BoardingJourneyCards">The boarding journey cards.</param>
        public void BuildDeparturesDestinationsTables(List<BoardingJourneyCard> BoardingJourneyCards)
        {
            foreach (BoardingJourneyCard bc in BoardingJourneyCards)
            {
                departuresCards.Add(bc.GetDeparture().Getid(), bc);
                destinationsCards.Add(bc.GetDestination().Getid(), bc);
            }
        }

        /// <summary>
        /// Gets the initial boarding journey card.
        /// It allows to obtain the boarding card in which the trip begins.
                 /// The place of origin of the trip can not be the place of the end of the trip.
        /// </summary>
        /// <returns></returns>
        private BoardingJourneyCard GetInitialBoardingJourneyCard()
        {
            //
            // To find out the boarding pass corresponding to the beginning of the trip, I base myself
            // where the place of departure can not appear as a place of destination in any
            // boarding pass, bearing in mind that the place of origin of the trip can not be
            // be the place of the end of the trip            
            //
            foreach (var departure in departuresCards)
            {
                if (!destinationsCards.ContainsKey(departure.Key))
                {
                    return departuresCards[departure.Key];
                }
            }
            return null;
        }

        /// <summary>
        /// Sorts the boarding journey cards.
        /// Allows you to obtain a list of boarding cards ordered by your order during
        /// a trip, starting from a boarding pass used at the starting point from the trip.        
        /// </summary>
        /// <param name="initialBoardingJourneyCard">The initial boarding journey card.</param>
        /// <returns>List<BoardingJourneyCard></returns>
        private List<BoardingJourneyCard> SortBoardingJourneyCards(BoardingJourneyCard initialBoardingJourneyCard)
        {
            //
            // List in which boarding cards are added in order from the start
            // until the end of the trip            
            //
            List<BoardingJourneyCard> sortedBoardingJourneyCards = new List<BoardingJourneyCard>();

            //
            // We receive as a parameter of this method the card used at the beginning of the trip
            // (we already got it in the initialBoardingJourneyCard method), so we added it in the
            // first place in the list of cards ordered            
            //
            sortedBoardingJourneyCards.Add(initialBoardingJourneyCard);

            //
            // By having the boarding pass used at the starting point of the trip, we already know
            // which is the next destination of the trip since it is a data contained in the card itself            
            //
            String idNextDestination = initialBoardingJourneyCard.GetDestination().Getid();

            //
            // As from a card we know the next destination of the trip, we can
            // find which card has the destination of the card as the place of origin or departure
            // previous, using the departuresCards table where we have an id mapping
            // of the place of origin corresponding to each card.
            // The destination of a card used in a previous step of the trip, will be the place of origin
            // of the card used in the next step of the trip.
            //
            while (departuresCards.ContainsKey(idNextDestination))
            {
                sortedBoardingJourneyCards.Add(departuresCards[idNextDestination]);
                idNextDestination = departuresCards[idNextDestination].GetDestination().Getid();
            }
            return sortedBoardingJourneyCards;
        }
    }
}
