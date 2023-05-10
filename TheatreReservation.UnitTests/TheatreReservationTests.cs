using Moq;
using TheatreReservation;
using Xunit;

namespace TheatreReservation.UnitTests;

public class TheatreReservationTests
{
    [Fact]
    public void ReserveSeat_InsufficientSeats_Insufficient()
    {
        // Arrange
        var moqObj = new Mock<ITheaterSeatService>();
        moqObj.Setup(x => x.GetAvailableSeats()).Returns(new[,]
        {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}
        });

        var theatreSeatService = moqObj.Object;
        var service = new SeatReservationService(theatreSeatService);
        
        // Act
        var result = service.ReserveSeats(30);

        // Assert
        Assert.Equal(SeatStatus.Insufficient, result.SeatStatus);
    }
    
    [Fact]
    public void ReserveSeat_SomeSeatsAvailableButStillInsufficientSeats_Insufficient()
    {
        // Arrange
        var moqObj = new Mock<ITheaterSeatService>();
        moqObj.Setup(x => x.GetAvailableSeats()).Returns(new[,]
        {
            {1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1},
            {1, 1, 1, 1, 1}
        });

        var theatreSeatService = moqObj.Object;
        var service = new SeatReservationService(theatreSeatService);
        
        // Act
        var result = service.ReserveSeats(5);

        // Assert
        Assert.Equal(SeatStatus.Insufficient, result.SeatStatus);
    }
    
    [Fact]
    public void ReserveSeat_AllSeatsAvailable_Sufficient()
    {
        // Arrange
        var reservedSeats = "A1,A2,A3,A4,A5";        
        var moqObj = new Mock<ITheaterSeatService>();
        moqObj.Setup(x => x.GetAvailableSeats()).Returns(new[,]
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        });

        var theatreSeatService = moqObj.Object;
        var service = new SeatReservationService(theatreSeatService);
        
        // Act
        var result = service.ReserveSeats(5);

        // Assert
        Assert.Equal(SeatStatus.Sufficient, result.SeatStatus);
        Assert.Equal(reservedSeats, string.Join(",", result.AllocatedSeats));
    }
    
    [Fact]
    public void ReserveSeat_SomeSeatsAvailable_Sufficient()
    {
        // Arrange
        var reservedSeats = "A1,A3,A4,C1,C5";        
        var moqObj = new Mock<ITheaterSeatService>();
        moqObj.Setup(x => x.GetAvailableSeats()).Returns(new[,]
        {
            {0, 1, 0, 0, 1},
            {1, 1, 1, 1, 1},
            {0, 1, 1, 1, 0}
        });

        var theatreSeatService = moqObj.Object;
        var service = new SeatReservationService(theatreSeatService);
        
        // Act
        var result = service.ReserveSeats(5);

        // Assert
        Assert.Equal(SeatStatus.Sufficient, result.SeatStatus);
        Assert.Equal(reservedSeats, string.Join(",", result.AllocatedSeats));
    }
}
