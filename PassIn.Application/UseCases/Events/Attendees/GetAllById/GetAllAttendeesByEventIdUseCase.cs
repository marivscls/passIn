using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Attendees.GetAllById
{
    public class GetAllAttendeesByEventIdUseCase
    {

        private readonly PassInDbContext _dbContext;

        public GetAllAttendeesByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();

        }

        public ResponseAllAttendeesJson Execute(Guid eventId)
        {
            var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(Attendees => Attendees.CheckIn).FirstOrDefault(ev => ev.Id == eventId);
            if (entity is null)
                throw new NotFoundException("An event with this id dont exist.");

            return new ResponseAllAttendeesJson
            {
                Attendees = entity.Attendees.Select(attendees => new ResponseAttendeeJson
                {
                    Id = attendees.Id,
                    Name = attendees.Name,
                    Email = attendees.Email,
                    CreatedAt = attendees.Created_At,
                    CheckedInAt = attendees.CheckIn?.Created_at
                }).ToList()
            };
        }
    }
}
