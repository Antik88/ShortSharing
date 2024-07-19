using AutoMapper;
using NSubstitute;
using User.Service.BLL.Models;
using User.Service.BLL.Service.Implementation;
using User.Service.BLL.Service.Interfaces;
using User.Service.DLL.Repositories.Interfaces;
using User.Service.DLL.Entities;
using User.Service.Shared;
using NSubstitute.ReturnsExtensions;

namespace User.Service.Test
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _userService = new UserService(_userRepository, _mapper);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_InvalidId_ReturnsNull(Guid invalidId)
        {
            // Arrange
            _userRepository.GetByIdAsync(invalidId, default).ReturnsNull();

            // Act
            var result = await _userService.GetByIdAsync(invalidId, default);

            // Assert
            Assert.Null(result);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ValidId_Returns_UserModel(Guid id, UserEntity userEntity, UserModel userModel)
        {
            // Arrange
            _userRepository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(Task.FromResult(userEntity));
            _mapper.Map<UserModel>(userEntity).Returns(userModel);

            // Act
            var result = await _userService.GetByIdAsync(id, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userModel.Id, result.Id);
            Assert.Equal(userModel, result);
        }

        [Theory, AutoMoqData]
        public async Task GetRangeAsync_Returns_PageResult_UserModel(Query query, PageResult<UserEntity> userEntity,
            PageResult<UserModel> userModel)
        {
            // Arrange
            _userRepository.GetRangeAsync(query, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(userEntity));

            _mapper.Map<PageResult<UserModel>>(userEntity).Returns(userModel);

            // Act
            var result = await _userService.GetRangeAsync(query, default);

            // Assert
            Assert.NotNull(result);
        }

        [Theory, AutoMoqData]
        public async Task AddAsync_Returns_UserModel(UserEntity userEntity, UserModel userModel)
        {
            // Arrange
            _mapper.Map<UserEntity>(userModel).Returns(userEntity);
            _userRepository.AddAsync(userEntity, Arg.Any<CancellationToken>()).Returns(Task.FromResult(userEntity));
            _mapper.Map<UserModel>(userEntity).Returns(userModel);

            // Act
            var result = await _userService.AddAsync(userModel, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userModel, result);
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_Returns_UserModel(Guid id, UserEntity userEntity, UserModel userModel)
        {
            // Arrange
            _mapper.Map<UserEntity>(userModel).Returns(userEntity);
            _userRepository.UpdateAsync(id, userEntity, Arg.Any<CancellationToken>()).Returns(Task.FromResult(userEntity));
            _mapper.Map<UserModel>(userEntity).Returns(userModel);

            // Act
            var result = await _userService.UpdateAsync(id, userModel, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userModel, result);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_Deletes_User(Guid id)
        {
            // Arrange
            // No setup needed for DeleteAsync since it returns void

            // Act
            await _userService.Delete(id, default);

            // Assert
            await _userRepository.Received(1).DeleteAsync(id, Arg.Any<CancellationToken>());
        }
    }
}
