﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaAssistant.Entities;
using AgendaAssistant.Shared;

namespace AgendaAssistant.Repositories
{
    public static class EntityMapper
    {
        public static Event Map(DB.Event dbEvent, bool includeParticipants = true, bool includeFlights = true)
        {
            var evn = new Event()
                {
                    Id = dbEvent.ID,
                    Description = dbEvent.Description,
                    Status = dbEvent.Status,
                    Title = dbEvent.Title,
                    IsConfirmed = dbEvent.IsConfirmed,
                    Organizer = Map(dbEvent.Organizer),
                    Participants = new List<Participant>()
                };

            if (includeParticipants)
            {
                dbEvent.Participants.ToList().ForEach(p => evn.Participants.Add(Map(p)));
            }

            if (includeFlights)
            {
                evn.OutboundFlightSearch = Map(dbEvent.OutboundFlightSearch);
                evn.InboundFlightSearch = Map(dbEvent.InboundFlightSearch);
            }

            return evn;
        }

        public static Person Map(DB.Person dbPerson)
        {
            return new Person
                {
                    Id = dbPerson.ID,
                    Name = dbPerson.Name,
                    Email = dbPerson.Email,
                    FirstNameInPassport = dbPerson.FirstNameInPassport,
                    LastNameInPassport = dbPerson.LastNameInPassport,
                    DateOfBirth = dbPerson.DateOfBirth,
                    Gender = dbPerson.Gender.HasValue ? (Gender)dbPerson.Gender.Value : (Gender?)null
                };
        }

        public static FlightSearch Map(DB.FlightSearch dbFlightSearch)
        {
            if (dbFlightSearch == null)
                return null;

            var flightSearch = new FlightSearch
                {
                    Id = dbFlightSearch.ID,
                    DepartureStation = dbFlightSearch.DepartureStation.Trim(),
                    ArrivalStation = dbFlightSearch.ArrivalStation.Trim(),
                    BeginDate = dbFlightSearch.StartDate,
                    EndDate = dbFlightSearch.EndDate,
                    DaysOfWeek = (short)dbFlightSearch.DaysOfWeek,
                    MaxPrice = (short?)dbFlightSearch.MaxPrice,
                    Flights = new List<Flight>()
                };

            dbFlightSearch.Flights.ToList().ForEach(f => flightSearch.Flights.Add(Map(f)));

            // link selected flight from collection
            if (dbFlightSearch.SelectedFlightID.HasValue)
            {
                flightSearch.SelectedFlight =
                    flightSearch.Flights.Single(f => f.Id.Equals(dbFlightSearch.SelectedFlightID));
            }

            return flightSearch;
        }

        public static Flight Map(DB.Flight dbFlight)
        {
            return new Flight
                {
                    Id = dbFlight.ID,
                    CarrierCode = dbFlight.CarrierCode,
                    FlightNumber = (short)dbFlight.FlightNumber,
                    ArrivalStation = dbFlight.FlightSearch.ArrivalStation.Trim(),
                    DepartureStation = dbFlight.FlightSearch.DepartureStation.Trim(),
                    DepartureDate = dbFlight.DepartureDate,
                    STA = dbFlight.STA,
                    STD = dbFlight.STD,
                    Price = dbFlight.Price / 100M
                };
        }

        public static Availability Map(DB.Availability dbAvailability)
        {
            return new Availability
            {
                ParticipantId = dbAvailability.ParticipantID,
                FlightId = dbAvailability.FlightID,
                Value = dbAvailability.Value ?? 0,
                CommentText = dbAvailability.Comment.Trim(),
                Name = dbAvailability.Participant.Person.Name // todo: improve!!!
            };
        }

        public static Participant Map(DB.Participant dbParticipant)
        {
            return new Participant
            {
                Id = dbParticipant.ID,

                EventId = dbParticipant.EventID,
                Person = Map(dbParticipant.Person),

                Bagage = dbParticipant.Bagage.Trim()
            };
        }
    }
}