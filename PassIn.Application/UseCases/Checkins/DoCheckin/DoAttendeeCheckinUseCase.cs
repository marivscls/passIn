using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkins.DoCheckin
{
    public class DoAttendeeCheckinUseCase
    {

        private readonly PassInDbContext _dbContext;

        public DoAttendeeCheckinUseCase()
        {
            _dbContext = new PassInDbContext();

        }


        public ResponseRegisteredJson Execute(Guid attendeeId)
        {

            Validade(attendeeId);

            var entity = new CheckIn
            {
                Attendee_Id = attendeeId,
                Created_at = DateTime.UtcNow,

            };

            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson()
            {
                Id = entity.Id,
            };
        }

        private void Validade(Guid attendeeID)
        {
           var existeAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeID);
            if (existeAttendee == false)
            {
                throw new NotFoundException("The Attendee with this Id was not found");
            }

            var existCheckin = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeID);
            if(existCheckin)
            {
                throw new ConflictException("Attendee can not do checking twice in the same event.");
            }
        }
    }
}
