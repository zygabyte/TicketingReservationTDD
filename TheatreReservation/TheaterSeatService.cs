namespace TheatreReservation
{
    public class TheaterSeatService : ITheaterSeatService
    {
        public int[,] GetAvailableSeats()
        {
            return new [,]
            {
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0},
            };
        }
    }
}
