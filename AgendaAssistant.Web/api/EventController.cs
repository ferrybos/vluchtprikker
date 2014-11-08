﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AgendaAssistant.Entities;
using AgendaAssistant.Services;
using AgendaAssistant.Shared;

namespace AgendaAssistant.Web.api
{
    public class NewEventData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class ConfirmData
    {
        public string EventCode { get; set; }
    }

    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        private readonly IEventService _service;
        private readonly IAvailabilityService _availabilityService;

        public EventController(IEventService eventService, IAvailabilityService availabilityService)
        {
            _service = eventService;
            _availabilityService = availabilityService;
        }

        // GET api/<controller>/5
        [Route("{id}")]
        [HttpGet]
        public Event Get(string id)
        {
            //if (id == "new")
            //{
            //    // returns the model so it does not have to be duplicated in javascript
            //    var newEvent = EventFactory.NewEvent();
            //    return newEvent;
            //}
            
            // fetch existing event
            var evn = _service.Get(GuidUtil.ToGuid(id));

            var eventAvailabilities = _availabilityService.GetByEvent(evn.Id);
            evn.AddAvailabilities(eventAvailabilities);

            // used to show participants that have not reacted yet (clicked the link in the confirmation email)
            foreach (var participant in evn.Participants)
            {
                participant.HasConfirmed = eventAvailabilities.Any(a => a.ParticipantId == participant.Id);
            }

            return evn;
        }

        // POST api/<controller>
        [Route("")]
        [HttpPost]
        public IHttpActionResult New([FromBody]NewEventData data)
        {
            // create new event
            try
            {
                var newEvent = _service.Create(data.Title, data.Description, data.Name, data.Email);

                return Created(string.Format("api/event/{0}", GuidUtil.ToString(newEvent.Id)),
                               Json(new { Event = newEvent }).Content);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [Route("confirm")]
        [HttpPost]
        public IHttpActionResult Confirm([FromBody]ConfirmData data)
        {
            try
            {
                var id = GuidUtil.ToGuid(data.EventCode);
                _service.Confirm(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("selectflight")]
        [HttpPost]
        public IHttpActionResult SelectFlight(string id, long flightSearchId, long flightId)
        {
            try
            {
                // todo: _service.SelectFlight(flightSearchId, flightId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] Event value)
        {
            try
            {
                _service.Save(value);
                return Ok(string.Format("api/event/{0}", GuidUtil.ToString(value.Id)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}