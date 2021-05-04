﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("{listID}")]
        public async Task<IActionResult> UpdateRange(string listID)
        {
            return Ok(await _notificationService.UpdateRange(listID));
        }
        [AllowAnonymous]
        [HttpPost("{ID}")]
        public async Task<IActionResult> Update(int ID)
        {
            var obj =await _notificationService.Update(ID);
           // NotificationHub.SendNotifications();
            return Ok(obj);
        }
    }
}