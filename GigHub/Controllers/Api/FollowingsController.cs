using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitofwork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var following = _unitofwork.Followings.GetFollowing(userId, dto.FolloweeId);

            if (following != null)
                return BadRequest("Following already exists.");

            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _unitofwork.Followings.Add(following);
            _unitofwork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitofwork.Followings.GetFollowing(userId, id);

            if (following == null)
                return NotFound();

            _unitofwork.Followings.Remove(following);
            _unitofwork.Complete();

            return Ok(id);
        }
    }
}
