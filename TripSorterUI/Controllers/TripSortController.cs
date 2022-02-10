using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TripSorterBusinessLogic.BoadringCardType;
using TripSorterBusinessLogic.BoadringCardType.Aeroplane;
using TripSorterBusinessLogic.BoadringCardType.Bus;
using TripSorterBusinessLogic.BoadringCardType.LocationHelper;
using TripSorterBusinessLogic.BoadringCardType.Train;
using TripSorterBusinessLogic.BoadringCardType.TransportMode;
using TripSorterBusinessLogic.TripSortLogic;

namespace TripSorterUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripSortController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TripSortController> _logger;

        public TripSortController(ILogger<TripSortController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] Get()
        {
            string[] sortedlist = GetSortedTrip().Split('|');
            return sortedlist;
        }

        public string GetSortedTrip()
        {
            IBoardingJourneyCard airplaneBoardingJourneyCardFactory = new AirplaneBoardingJourneyCardExtension();
            IBoardingJourneyCard trainBoardingJourneyCardFactory = new TrainBoardingJourneyCardExtension();
            IBoardingJourneyCard busBoardingJourneyCardFactory = new BusBoardingJourneyCardExtension();

            List<BoardingJourneyCard> unorderedBoardingJourneyCards = new List<BoardingJourneyCard>()
            {
                    airplaneBoardingJourneyCardFactory.CreateBoardingJourneyCard(),
                    trainBoardingJourneyCardFactory.CreateBoardingJourneyCard(),
                    airplaneBoardingJourneyCardFactory.CreateBoardingJourneyCard(),
                    busBoardingJourneyCardFactory.CreateBoardingJourneyCard()
            };

            #region 4 boarding passes Journey Cards Data
            var bJourneyCard1 = (AirplaneBoardingJourneyCard)unorderedBoardingJourneyCards[0];

            var bJourneyCard2 = (TrainBoardingJourneyCard)unorderedBoardingJourneyCards[1];

            var bJourneyCard3 = (AirplaneBoardingJourneyCard)unorderedBoardingJourneyCards[2];

            var bJourneyCard4 = (BusBoardingJourneyCard)unorderedBoardingJourneyCards[3];

            bJourneyCard1.SetFlightNumber("SK22")
                .SetGate("22")
                .SetBaggageTicketCounter(null)
                .SetId("004")
                .SetTransport(new Airplane())
                .SetSeat("7B")
                .SetDeparture(new Location("stockolm-sweeden", "Stockolm", "Sweeden"))
                .SetDestination(new Location("ny-usa", "New York", "USA"));

            bJourneyCard2
                .SetId("001")
                .SetTransport(new Train("78A"))
                .SetSeat("45B")
                .SetDeparture(new Location("madrid-spain", "Madrid", "Spain"))
                .SetDestination(new Location("barcelona-spain", "Barcelona", "Spain"));

            bJourneyCard3
                .SetFlightNumber("SK455")
                .SetGate("45B")
                .SetBaggageTicketCounter("344")
                .SetId("003")
                .SetSeat("3A")
                .SetDeparture(new Location("geronaairport-spain", "Gerona", "Spain"))
                .SetDestination(new Location("stockolm-sweeden", "Stockolm", "Sweeden"));

            bJourneyCard4
                .SetId("002")
                .SetSeat(null)
                .SetDeparture(new Location("barcelona-spain", "Barcelona", "Spain"))
                .SetDestination(new Location("geronaairport-spain", "Gerona", "Spain"));
            #endregion

            // Establish the ordering Technique following the Technique pattern
            Journey Journey = new Journey(unorderedBoardingJourneyCards);
            Journey.SetOrderingTravelTechnique(new TechniqueNoStartNoEnd());

            // Order travel cards
            Journey.SortJourney(unorderedBoardingJourneyCards);

            return Journey.GetMoreInformationFroTravel();
        }
    }
}
