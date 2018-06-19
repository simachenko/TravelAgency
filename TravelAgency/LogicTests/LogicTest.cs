﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using DAL;
using Logic;
using Ninject;
using Ninject.Modules;

namespace LogicTests
{
    [TestFixture]
    public class LogicTest
    {
        [Test]
        public void AddingHotel()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var HotelLogic = new HotelLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");
            HotelLogic.AddHotel(new Logic.DTOs.HotelDTO("auiwhe", 3, "auiwhe--, petuch 44/2"));

            Assert.That(HotelLogic.GetAllHotels().Count() == 1);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Name == "auiwhe");
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Stars == 3);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Address == "auiwhe--, petuch 44/2");
        }

        [Test]
        public void DeletingHotel()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var HotelLogic = new HotelLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            HotelLogic.AddHotel(new Logic.DTOs.HotelDTO("auiwhe", 3, "auiwhe--, petuch 44/2"));

            Assert.That(HotelLogic.GetAllHotels().Count() == 1);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Name == "auiwhe");
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Stars == 3);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Address == "auiwhe--, petuch 44/2");

            HotelLogic.DeleteHotel(1);

            Assert.That(HotelLogic.GetAllHotels().Count() == 0);
            Assert.That(UoW.Object.HotelsRooms.GetAll().Count == 0);


        }

        [Test]
        public void AddingHotelRoomToHotel()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var HotelLogic = new HotelLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            HotelLogic.AddHotel(new Logic.DTOs.HotelDTO("auiwhe", 3, "auiwhe--, petuch 44/2"));

            HotelLogic.AddHotelRoom(HotelLogic.GetAllHotels().ToList()[0].Id, new Logic.DTOs.HotelRoomDTO(1, 3, 250));

            Assert.That(UoW.Object.HotelsRooms.GetAll().Count() == 1);

            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms.Count() == 1);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms[0].Hotel.Id == HotelLogic.GetAllHotels().ToList()[0].Id);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms[0].Number == 1);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms[0].SleepingPlaces == 3);
            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms[0].Price == 250);
        }

        [Test]
        public void AddingTransport()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TransportLogic = new TransportLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            TransportLogic.AddTransport(new Logic.DTOs.TransportDTO("Bus", "Kyiv", DateTimeOffset.Parse("21.12.15 21:15"), "odessacity", DateTimeOffset.Parse("22.12.15 9:15")), 30, 150);

            var Transport = TransportLogic.GetAllTransport().ToList()[0];

            Assert.That(TransportLogic.GetAllTransport().Count() == 1);
            Assert.That(Transport.Type == "Bus");
            Assert.That(Transport.DeparturePoint == "Kyiv");
            Assert.That(Transport.DepartureTime == DateTimeOffset.Parse("21.12.15 21:15"));
            Assert.That(Transport.ArrivalPoint == "odessacity");
            Assert.That(Transport.ArrivalTime == DateTimeOffset.Parse("22.12.15 9:15"));
            Assert.That(Transport.TransportPlaces.Count == 30);
            Assert.That(Transport.TransportPlaces[0].Number == 1);
            Assert.That(Transport.TransportPlaces[0].Price == 150);
            Assert.That(Transport.TransportPlaces[0].Transport.Id == Transport.Id);
        }

        [Test]
        public void DeletingTransport()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TransportLogic = new TransportLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            TransportLogic.AddTransport(new Logic.DTOs.TransportDTO("Bus", "Kyiv", DateTimeOffset.Parse("21.12.15 21:15"), "odessacity", DateTimeOffset.Parse("22.12.15 9:15")), 30, 150);

            var Transport = TransportLogic.GetAllTransport().ToList()[0];

            Assert.That(TransportLogic.GetAllTransport().Count() == 1);

            TransportLogic.DeleteTransport(1);

            Assert.That(TransportLogic.GetAllTransport().Count() == 0);

        }

        [Test]
        public void AddingTour()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TourLogic = new TourLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");


            TourLogic.AddTour(new Logic.DTOs.TourDTO("odessa", 250, "Excursion", "Ukraine", "beach", 3, "go at beach and fuck a bithc"));

            var Tour = TourLogic.GetAllToursTemplates().ToList()[0];

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 1);
            Assert.That(Tour.Name == "odessa");
            Assert.That(Tour.Price == 250);
            Assert.That(Tour.Type == "Excursion");
            Assert.That(Tour.Country == "Ukraine");
            Assert.That(Tour.City == "beach");
            Assert.That(Tour.Duration == 3);
            Assert.That(Tour.Description == "go at beach and fuck a bithc");
        }

        [Test]
        public void EditingTour()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TourLogic = new TourLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");
            TourLogic.AddTour(new Logic.DTOs.TourDTO("odessa", 250, "Excursion", "Ukraine", "beach", 3, "go at beach and fuck a bithc"));

            var Tour = TourLogic.GetAllToursTemplates().ToList()[0];

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 1);
            Assert.That(Tour.Name == "odessa");
            Assert.That(Tour.Price == 250);
            Assert.That(Tour.Type == "Excursion");
            Assert.That(Tour.Country == "Ukraine");
            Assert.That(Tour.City == "beach");
            Assert.That(Tour.Duration == 3);
            Assert.That(Tour.Description == "go at beach and fuck a bithc");

            Tour.City = "odessa";
            Tour.Description = "lalka";

            TourLogic.EditTour(Tour.Id, Tour);

            Tour = TourLogic.GetAllToursTemplates().ToList()[0];

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 1);
            Assert.That(Tour.Name == "ODessa");
            Assert.That(Tour.Price == 250);
            Assert.That(Tour.Type == "Excursion");
            Assert.That(Tour.Country == "Ukraine");
            Assert.That(Tour.City == "kyev");
            Assert.That(Tour.Duration == 3);
            Assert.That(Tour.Description == "Kek");
        }

        [Test]
        public void DeletingTour()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TourLogic = new TourLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa", 250, "Excursion", "Ukraine", "Zaporogie", 3, "Go at beach"));

            var Tour = TourLogic.GetAllToursTemplates().ToList()[0];

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 1);

            TourLogic.DeleteTour(1);

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 0);
        }

        [Test]
        public void FindingTour()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TourLogic = new TourLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");

            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa", 250, "Excursion", "Ukraine", "Zaporogie", 3, "Go at beach"));
            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa1", 300, "Hot", "Hungary", "Moscow", 2, "Go at beach"));
            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa2", 300, "All", "Ukraine", "Moscow", 2, "Go at beach"));
            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa3", 300, "All", "Ukraine", "Kyiv", 3, "Go at beach"));
            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa4", 250, "Hot", "Hungary", "Zaporogie", 3, "Go at beach"));
            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa5", 500, "Hot", "USA", "New Yourk", 10, "Go at beach"));

            Assert.That(TourLogic.GetAllToursTemplates().Count() == 6);

            Assert.That(TourLogic.FindTourTemplates("Hot").Count() == 3);
            Assert.That(TourLogic.FindTourTemplates("Excursion").Count() == 1);
            Assert.That(TourLogic.FindTourTemplates("Error").Count() == 0);
            Assert.That(TourLogic.FindTourTemplates("Excursion").ToList()[0].City == "Zaporogie");

            Assert.That(TourLogic.FindTourTemplates("USA").Count() == 1);
            Assert.That(TourLogic.FindTourTemplates("Ukraine").Count() == 3);
            Assert.That(TourLogic.FindTourTemplates("Error").Count() == 0);
            Assert.That(TourLogic.FindTourTemplates("USA").ToList()[0].City == "New Yourk");

            Assert.That(TourLogic.FindTourTemplates("Moscow").Count() == 2);
            Assert.That(TourLogic.FindTourTemplates("New Yourk").Count() == 1);
            Assert.That(TourLogic.FindTourTemplates("Error").Count() == 0);

            Assert.That(TourLogic.FindTourTemplates("New Yourk").ToList()[0].Country == "USA");

            Assert.That(TourLogic.FindTourTemplatesByDuration(0, 3).Count() == 5);
            Assert.That(TourLogic.FindTourTemplatesByDuration(10, 10).Count() == 1);
            Assert.That(TourLogic.FindTourTemplatesByDuration(1000000, 10000000).Count() == 0);
            Assert.That(TourLogic.FindTourTemplatesByDuration(10, 20).ToList()[0].Country == "USA");
            Assert.That(TourLogic.FindTourTemplatesByDuration(10, 15).ToList()[0].Price == 500);


            Assert.That(TourLogic.FindTourTemplatesByPrice(250, 250).Count() == 2);
            Assert.That(TourLogic.FindTourTemplatesByPrice(250, 300).Count() == 5);
            Assert.That(TourLogic.FindTourTemplatesByPrice(100, 249).Count() == 0);
        }

        [Test]
        public void AddingUser()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.User, "Cherneckii", "Login", "Password"));

            Assert.That(UserLogic.GetAllUsers().Count() == 1);
            Assert.That(UserLogic.GetAllUsers().ToList()[0].Name == "Dimon");
            Assert.That(UserLogic.GetAllUsers().ToList()[0].Surname == "Cherneckii");
            Assert.That(UserLogic.GetAllUsers().ToList()[0].UserType == Logic.DTOs.UserType.User);
            Assert.That(UserLogic.GetAllUsers().ToList()[0].Login == "Login");
            Assert.That(UserLogic.GetAllUsers().ToList()[0].Password == "Password");
        }

        [Test]
        public void DeletingUser()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.User, "Cherneckii", "Login", "Password"));

            Assert.That(UserLogic.GetAllUsers().Count() == 1);

            UserLogic.DeleteUser(1);

            Assert.That(UserLogic.GetAllUsers().Count() == 0);
        }

        [Test]
        public void Entering()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var UserLogic = new UserLogic(UoW.Object);

            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.User, "Cherneckii", "Login", "Password"));

            Assert.That(UserLogic.Login("Login", "Password").Id == UserLogic.GetAllUsers().ToList()[0].Id);
            Assert.Throws<InvalidLoginPasswordCombinationException>(delegate { UserLogic.Login("Wrong", "Password"); });
        }


        [Test]
        public void ReservingRoom()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();
            var UserLogic = new UserLogic(UoW.Object);
            var HotelLogic = new HotelLogic(UoW.Object);
            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.Login("Login", "Password");
            HotelLogic.AddHotel(new Logic.DTOs.HotelDTO("Odessa", 3, "zaporozye, polevaya 44/2"));
            HotelLogic.AddHotelRoom(HotelLogic.GetAllHotels().ToList()[0].Id, new Logic.DTOs.HotelRoomDTO(1, 3, 250));
 

            Assert.That(UoW.Object.HotelsRooms.GetAll().Count() == 1);

            Assert.That(HotelLogic.GetAllHotels().ToList()[0].Rooms.Count() == 1);
            UserLogic.ReserveRoom(1, 1,1, DateTimeOffset.Parse("21.12.2018"), DateTimeOffset.Parse("25.12.2018"));

            var User = UserLogic.GetAllUsers().First(u => u.Id == 1);

            Assert.That(User.HotelRoomReservations.Count == 1);
            Assert.That(User.HotelRoomReservations[0].HotelAddress == "zaporozye, polevaya 44/2");
            Assert.That(User.HotelRoomReservations[0].HotelName == "Odessa");
            Assert.That(User.HotelRoomReservations[0].HotelRoomNumber == 1);
            Assert.That(User.HotelRoomReservations[0].HotelRoomPrice == 250);
            Assert.That(User.HotelRoomReservations[0].HotelRoomSleepingPlaces == 3);
            Assert.That(User.HotelRoomReservations[0].HotelStars == 3);
            Assert.That(User.HotelRoomReservations[0].ClientName == "Dimon");
            Assert.That(User.HotelRoomReservations[0].ClientSurname == "Cherneckii");
            Assert.That(User.HotelRoomReservations[0].ArrivalDate.CompareTo(DateTimeOffset.Parse("21.12.2018")) == 0);
            Assert.That(User.HotelRoomReservations[0].DepartureDate.CompareTo(DateTimeOffset.Parse("25.12.2018")) == 0);

            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("21.12.2018")));
            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("22.12.2018")));
            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("23.12.2018")));
            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("24.12.2018")));
            Assert.That(!HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("25.12.2018")));

            Assert.Throws<AlreadyBookedItemException>(delegate { UserLogic.ReserveRoom(1, 1, 1, DateTimeOffset.Parse("23.12.2018"), DateTimeOffset.Parse("24.12.2018")); });

            UserLogic.ReserveRoom(1, 1, 1, DateTimeOffset.Parse("25.12.2018"), DateTimeOffset.Parse("28.12.2018"));

            User = UserLogic.GetAllUsers().First(u => u.Id == 1);

            Assert.That(User.HotelRoomReservations.Count == 2);
            Assert.That(User.HotelRoomReservations[1].HotelAddress == "zaporozye, polevaya 44/2");
            Assert.That(User.HotelRoomReservations[1].HotelName == "Odessa");
            Assert.That(User.HotelRoomReservations[1].HotelRoomNumber == 1);
            Assert.That(User.HotelRoomReservations[1].HotelRoomPrice == 250);
            Assert.That(User.HotelRoomReservations[1].HotelRoomSleepingPlaces == 3);
            Assert.That(User.HotelRoomReservations[1].HotelStars == 3);
            Assert.That(User.HotelRoomReservations[1].ClientName == "Dimon");
            Assert.That(User.HotelRoomReservations[1].ClientSurname == "Cherneckii");
            Assert.That(User.HotelRoomReservations[1].ArrivalDate.CompareTo(DateTimeOffset.Parse("25.12.2018")) == 0);
            Assert.That(User.HotelRoomReservations[1].DepartureDate.CompareTo(DateTimeOffset.Parse("28.12.2018")) == 0);


            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("27.12.2018")));
            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("26.12.2018")));
            Assert.That(HotelLogic.GetAllHotels().First(h => h.Id == 1).Rooms.First(r => r.Id == 1).BookedDays.Contains(DateTimeOffset.Parse("25.12.2018")));

            Assert.Throws<AlreadyBookedItemException>(delegate { UserLogic.ReserveRoom(1, 1, 1, DateTimeOffset.Parse("19.12.2018"), DateTimeOffset.Parse("30.12.2018")); });
        }

        [Test]
        public void ReservingTicket()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var UserLogic = new UserLogic(UoW.Object);
            var TransportLogic = new TransportLogic(UoW.Object);
            TransportLogic.AddTransport(new Logic.DTOs.TransportDTO("Bus", "Kyiv", DateTimeOffset.Parse("21.12.15 21:15"), "Zaporogie", DateTimeOffset.Parse("22.12.15 9:15")), 30, 150);
            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));
            UserLogic.ReserveTicket(1, 1, 1);

            var User = UserLogic.GetAllUsers().First(u => u.Id == 1);

            Assert.That(User.TransportTickets.Count == 1);
            Assert.That(User.TransportTickets[0].TransportType == "Bus");
            Assert.That(User.TransportTickets[0].PassangerName == "Dimon");
            Assert.That(User.TransportTickets[0].PassangerSurname == "Cherneckii");
            Assert.That(User.TransportTickets[0].NumberOfSeat == 1);
            Assert.That(User.TransportTickets[0].DeparturePoint == "Kyiv");
            Assert.That(User.TransportTickets[0].DepartureTime == DateTimeOffset.Parse("21.12.15 21:15"));
            Assert.That(User.TransportTickets[0].ArrivalPoint == "Zaporogie");
            Assert.That(User.TransportTickets[0].ArrivalTime == DateTimeOffset.Parse("22.12.15 9:15"));
            Assert.That(User.TransportTickets[0].Price == 150);

            Assert.That(TransportLogic.GetAllTransport().First(t => t.Id == 1).TransportPlaces.First(r => r.Id == 1).IsBooked);
            Assert.Throws<AlreadyBookedItemException>(delegate { UserLogic.ReserveTicket(1, 1, 1); });
        }

        [Test]
        public void ReservingTour()
        {
            var UoW = new Mock<UnitOfWork>();
            UoW.Object.DeleteDB();

            var TourLogic = new TourLogic(UoW.Object);
            var UserLogic = new UserLogic(UoW.Object);

            TourLogic.AddTour(new Logic.DTOs.TourDTO("ODessa", 250, "Excursion", "Ukraine", "Zaporogie", 3, "Go at beach"));
            UserLogic.AddUser(new Logic.DTOs.UserDTO("Dimon", Logic.DTOs.UserType.Manager, "Cherneckii", "Login", "Password"));

            UserLogic.ReserveTour(1, 1);

            var User = UserLogic.GetAllUsers().First(u => u.Id == 1);

            Assert.That(User.Tours.Count() == 1);
            Assert.That(User.Tours[0].Name == "ODessa");
            Assert.That(User.Tours[0].Price == 250);
            Assert.That(User.Tours[0].Type == "Excursion");
            Assert.That(User.Tours[0].Country == "Ukraine");
            Assert.That(User.Tours[0].City == "Zaporogie");
            Assert.That(User.Tours[0].Duration == 3);
            Assert.That(User.Tours[0].Description == "Go at beach");
        }
    }   
}
