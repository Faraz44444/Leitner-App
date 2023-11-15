using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Request.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Request.Role;

namespace Core.Request.BaseRequests.Tests
{
    [TestClass()]
    public class BaseRequestTests
    {
        //Convention for method name is:
        //ActualFunctionName_Scenario_ExpectedResult()
        [TestMethod()]
        public void BaseWhereSql_ClientId()
        {
            //Arrange : Initializing the required objects
            var roleRequest = new RoleRequest() { ClientId = 1};
            var tableAlias = "r";
            var expectedOutput = " r.ClientId = @ClientId";

            //Act : Acting on the initialized objects (like calling a method)
            var result = roleRequest.BaseWhereSql(tableAlias);

            //Assert : Verify the result is correct and as excpeted
            Assert.AreEqual(expectedOutput, result);
        }
        [TestMethod()]
        public void BaseWhereSql_Deleted()
        {
            //Arrange : Initializing the required objects
            var roleRequest = new RoleRequest() { Deleted = true};
            var tableAlias = "r";
            var expectedOutput = " r.Deleted = @Deleted";

            //Act : Acting on the initialized objects (like calling a method)
            var result = roleRequest.BaseWhereSql(tableAlias);

            //Assert : Verify the result is correct and as excpeted
            Assert.AreEqual(expectedOutput, result);
        }
    }
}