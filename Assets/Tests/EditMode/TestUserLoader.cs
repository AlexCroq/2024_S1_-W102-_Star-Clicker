using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UserParserTests
{
    [Test]
    public void ParseUsers_WhenValidJsonResponse_ReturnsListOfUsers()
    {
        string jsonResponse = @"[{""user_id"":""1"",""username"":""admin"",""password_hashed"":""hashed_password_here"",""star_dust"":""0"",""friendsList"":""[1, 2, 3]"",""starIDList"":""[101, 102, 103]""},{""user_id"":""2"",""username"":""random_user"",""password_hashed"":""random_hashed_password"",""star_dust"":""100"",""friendsList"":""[1, 2, 3]"",""starIDList"":""[101, 102, 103]""}]";
        var userLoader = new UserLoader(); // Assuming UserLoader is your class containing parseUsers method
        var expectedUsers = new User[]{
            new User { user_id = 1, username = "admin", password_hashed = "hashed_password_here", star_dust = 0, friendsList = new List<int> { 1, 2, 3 }, starIDList = new List<int>  { 101, 102, 103 } },
            new User { user_id = 2, username = "random_user", password_hashed = "random_hashed_password", star_dust = 100, friendsList = new List<int>  { 1, 2, 3 }, starIDList = new List<int>  { 101, 102, 103 } }
        };

        var result = userLoader.ParseUsers(jsonResponse);

        Assert.AreEqual(expectedUsers.Length, result.Count);
        for (int i = 0; i < expectedUsers.Length; i++)
        {
            Assert.AreEqual(expectedUsers[i].user_id, result[i].user_id);
            Assert.AreEqual(expectedUsers[i].username, result[i].username);
            Assert.AreEqual(expectedUsers[i].password_hashed, result[i].password_hashed);
            Assert.AreEqual(expectedUsers[i].star_dust, result[i].star_dust);
            CollectionAssert.AreEqual(expectedUsers[i].friendsList, result[i].friendsList);
            CollectionAssert.AreEqual(expectedUsers[i].starIDList, result[i].starIDList);
        }
    }
}
