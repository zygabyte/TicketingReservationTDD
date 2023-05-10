using System;
using System.Collections.Generic;
using TheatreReservation;

namespace TheatreReservation;

public class SeatReservationService
{
    private readonly ITheaterSeatService _theaterSeatService;

    public SeatReservationService(ITheaterSeatService theaterSeatService)
    {
        _theaterSeatService = theaterSeatService;
    }

    public ReservationModel ReserveSeats(int requestedSeats)
    {
        var theatreSeats = _theaterSeatService.GetAvailableSeats();

        return AreSeatsAvailable(theatreSeats, requestedSeats);
    }

    private ReservationModel AreSeatsAvailable(int [,] seats, int requestedSeats)
    {
        var availableSeats = 0;
        var reservedSeats = new List<string>();
        
        var rows = new Dictionary<int, string>
        {
            {0, "A"}, {1, "B"}, {2, "C"}
        };

        for (var i = 0; i < seats.GetLength(0); i++)
        {
            for (var j = 0; j < seats.GetLength(1); j++)
            {
                if (seats[i, j] == 0)
                {
                    availableSeats++;
                    
                    reservedSeats.Add($"{rows[i]}{j+1}");
                    if (availableSeats == requestedSeats) 
                        return new ReservationModel(SeatStatus.Sufficient, reservedSeats.ToArray());
                }
            }
        }

        return new ReservationModel(SeatStatus.Insufficient, new []{""});
    }
}
