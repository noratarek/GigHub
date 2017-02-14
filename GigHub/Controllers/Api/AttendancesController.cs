using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitofwork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitofwork.Attendances.GetAttendance(dto.GigId, userId);

            if (attendance != null)
                return BadRequest("The attendance already exists.");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitofwork.Attendances.Add(attendance);
            _unitofwork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitofwork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitofwork.Attendances.Remove(attendance);
            _unitofwork.Complete();

            return Ok(id);
        }
    }
}
