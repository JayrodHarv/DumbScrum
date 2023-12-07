using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests {
    [TestClass]
    public class UserManagerTests {

        IUserManager userManager;

        [TestInitialize]
        public void Init() {
            userManager = new UserManager(new UserAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue() {
            // in TDD (Test Driven Development) you write the test before the method
            // we use the red-green-refactor workflow
            // we write the test method with the A-A-A framework
            // test the test first before doing logic in method

            // Arrange - set up the test conditions
            UserManager employeeManager = new UserManager();
            string testString = "newuser";
            string actualhash = "";
            string expectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";

            // Act - invoke the method being tested and capture results
            actualhash = employeeManager.HashSha256(testString);

            // Assert - use the assert class
            Assert.AreEqual(expectedHash, actualhash);
        }

        [TestMethod]
        public void TestAuthenticateEmployeePassesWithCorrectEmailAndPassword() {
            // arrange
            string email = "joe-bidome@gmail.com";
            string passwordHash = "newuser";
            bool expectedResult = true;
            bool actualResult = false;

            // act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }

}
