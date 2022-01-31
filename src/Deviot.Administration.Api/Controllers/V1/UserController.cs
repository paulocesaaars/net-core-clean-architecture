using AutoMapper;
using Deviot.Administration.Api.Bases;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.Exceptions;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Common;
using Microsoft.AspNetCore.Mvc;

namespace Deviot.Administration.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : CustomControllerBase
    {
        private readonly IFindAllUsersUseCase _findAllUsersUseCase;
        private readonly IFindUserByIdUseCase _findUserByIdUseCase;
        private readonly IAddUserUseCase _saveUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;

        public UserController(INotifier notifier,
                              IMapper mapper,
                              ILogger<UserController> logger,
                              IFindAllUsersUseCase findAllUsersUseCase,
                              IFindUserByIdUseCase findUserByIdUseCase,
                              IAddUserUseCase saveUserUseCase,
                              IUpdateUserUseCase updateUserUseCase
                             ) : base(notifier, mapper, logger)
        {
            _findAllUsersUseCase = findAllUsersUseCase;
            _findUserByIdUseCase = findUserByIdUseCase;
            _saveUserUseCase = saveUserUseCase;
            _updateUserUseCase = updateUserUseCase;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Nullable))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAsync(
            int pageNumber = 0,
            int pageSize = 1000,
            string fullname = "",
            string email = ""
            )
        {
            try
            {
                var users = await _findAllUsersUseCase.ExecuteAsync(
                    pageNumber, 
                    pageSize, 
                    fullname, 
                    email
                    );

                if (_notifier.HasNotifications)
                    return CustomResponse();

                var usersModelView = _mapper.Map<IEnumerable<UserViewModel>>(users);
                return CustomResponse(usersModelView);
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Nullable))]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetForIdAsync(string id)
        {
            try
            {
                var user = await _findUserByIdUseCase.ExecuteAsync(new Id(id));

                if (_notifier.HasNotifications)
                    return CustomResponse();

                var userModelView = _mapper.Map<UserViewModel>(user);
                return CustomResponse(userModelView);
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Nullable))]
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] NewUserViewModel newUserViewModel)
        {
            try
            {
                var user = _mapper.Map<User>(newUserViewModel);
                await _saveUserUseCase.ExecuteAsync(user);

                return CustomResponse();
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Nullable))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Nullable))]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(string id, [FromBody] UserViewModel userModelView)
        {
            try
            {
                userModelView.Id = id;

                var user = _mapper.Map<User>(userModelView);
                await _updateUserUseCase.ExecuteAsync(user);

                return CustomResponse();
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
