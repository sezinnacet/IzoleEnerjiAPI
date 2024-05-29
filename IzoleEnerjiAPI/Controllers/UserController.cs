using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.RequestModels;
using Entities.Concrete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzoleEnerjiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService userService = userService;

        [HttpPost]
        [Route("GetUserDetail")]
        public IActionResult GetUserDetail([FromForm] UserDetailRequestModel model)
        {
            try
            {
                var result = userService.isSignedUp(model) ? userService.GetUserDetail(model.Email) : null;
                return new JsonResult(new Result<UserViewModel> { ResultObject = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Result<UserViewModel> { Message = ex.Message, Success = false });
            }

        }
        [HttpPost]
        [Route("GetUserDetailGoogle")]
        public IActionResult GetUserDetailGoogle([FromForm] UserDetailRequestModel model)
        {
            try
            {
                UserViewModel result = null;
                if (userService.isSignedUp(model, true))
                {
                    result = userService.GetUserDetail(model.Email);
                }
                else
                {
                    result = userService.AddUser(new AddUserRequestModel
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Name = model.Name,
                        Surname = model.Surname
                    });
                }
                return new JsonResult(new Result<UserViewModel> { ResultObject = result });
            }
            catch (Exception ex)
            {

                return new JsonResult(new Result<UserViewModel> { Message = ex.Message, Success = false });
            }

        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromForm] AddUserRequestModel user)
        {
            try
            {
                var result = userService.AddUser(user);
                return new JsonResult(new Result<UserViewModel> { ResultObject = result });
            }
            catch (Exception ex)
            {

                return new JsonResult(new Result<UserViewModel> { Message = ex.Message, Success = false });
            }

        }

        [HttpPost]
        [Route("UpdgradeToPremium")]
        public IActionResult UpdgradeToPremium(UpgradePremiumRequestModel model)
        {
            try
            {
                var result = userService.UpgradeToPremium(model);
                return new JsonResult(new Result<UserViewModel>() { ResultObject = result });
            }
            catch (Exception ex)
            {

                return new JsonResult(new Result<object> { Message = ex.Message, Success = false });
            }

        }
    }
}
