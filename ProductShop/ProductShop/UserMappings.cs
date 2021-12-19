using ProductShop.Datasets.Dtos.Input;
using ProductShop.Models;

namespace ProductShop
{
    public static class UserMappings
    {
        public static User MapToDomainUser (this UserInputDto userDto)
        {
            return new User
            {
                Age = userDto.Age,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };
        }
        public static UserInputDto MapToUserDto (this User user)
        {
            return new UserInputDto
            {
                Age = user.Age,
                FirstName = user.FirstName,
                LastName= user.LastName
            };
        }
    }
}
