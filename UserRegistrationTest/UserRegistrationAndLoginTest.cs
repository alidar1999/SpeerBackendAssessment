using Common.Business.Authentication;
using Common.Data;
using Common.Data.User;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using Twitter.Controllers.Authentication;
using User = Common.Models.User;

namespace UserRegistrationTest
{
    public class UserRegistrationAndLoginTest
    {
        IUserData _userData;
        IAuth _auth;
        AuthenticationController _authController;
        public UserRegistrationAndLoginTest()
        {
            Client.Initialize("https://dssxaentuenuzoyuztgl.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRzc3hhZW50dWVudXpveXV6dGdsIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjI4MzQ4NzksImV4cCI6MTk3ODQxMDg3OX0.h91D9DeUKokd7-HS4kER0RkSMmGZ3r1rH-uK36sxfRo");

            _userData = new UserSupabaseHelper(new DataClient());
            _auth = new UserAuth(_userData);
            _authController = new AuthenticationController(_auth);
        }

        [Theory]
        [InlineData("alilatif2542", "ABCD@1919ok")]
        public void RegisterValidUserShouldReturn200OK(string username, string password)
        {
            //Arrange
            User validUser = new User
            {
                Username = username,
                Password = password,
                Id = 0
            };
            //Act
            var okResult = _authController.Register(validUser);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<string>(item.Value);

            //Now, let us check the value itself.
            var response = item.Value as string;
            Assert.Equal(response, $"{username} Welcome!!!");
        }

        [Theory]
        [InlineData("alilatif", "ABCD@1919ok")]
        public void RegisterExistingUserShouldReturnConflict(string username, string password)
        {
            //Arrange
            User validUser = new User
            {
                Username = username,
                Password = password,
                Id = 0
            };
            //Act
            var conflictResult = _authController.Register(validUser);

            //Assert
            Assert.IsType<ConflictObjectResult>(conflictResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = conflictResult.Result as ConflictObjectResult;

            //We Expect to return a single book
            Assert.IsType<string>(item.Value);

            //Now, let us check the value itself.
            var response = item.Value as string;
            Assert.Equal(response, $"User already exists.");
        }
    }
}