using System.Net;
using API.Tests.helpers;
using Newtonsoft.Json;
using RestSharp;

namespace API.Tests;

public class UsersTests : TestBase
{
    [Fact]
    public async Task GetUsers()
    {
        var request = new RestRequest(Endpoints.users, Method.Get);
        request.AddQueryParameter("per_page", "1000");
        var response = await Client.GetAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostUsers()
    {
        var name = "Sebastian";
        var job = "Developer";

        var request = new RestRequest(Endpoints.users, Method.Post);
        request.AddJsonBody(new { name, job });
        var response = await Client.PostAsync(request);

        var user = JsonConvert.DeserializeObject<User>(response.Content!);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(name, user!.Name);
        Assert.Equal(job, user.Job);
        Assert.NotNull(user.Id);
    }

    [Fact]
    public async Task GetUser()
    {
        // //That's how I would do it in normal api, but reqres.in doesn't actually create a user
        // //
        // var name = "Sebastian";
        // var email = "sebastian@example.com";

        // var postRequest = new RestRequest(Endpoints.users, Method.Post);
        // postRequest.AddJsonBody(new { name = name, email = email });
        // var postResponse = await Client.PostAsync(postRequest);
        // var postUserId = JsonConvert.DeserializeObject<User>(postResponse.Content!)!.Id;
        
        // var getRequest = new RestRequest($"{Endpoints.users}/{postUserId}", Method.Get);
        // getRequest.AddQueryParameter("per_page", "1000");

        // var getResponse = await Client.GetAsync(getRequest);
        // var getUser = JsonConvert.DeserializeObject<User>(getResponse.Content!)!;

        // Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        // Assert.Equal(name, getUser.Name);
        // Assert.Equal(email, getUser.Email);
        
        var expectedName = "Janet";
        var expectedEmail = "janet.weaver@reqres.in";

        var getRequest = new RestRequest($"{Endpoints.users}/2", Method.Get);
        var getResponse = await Client.GetAsync(getRequest);
        var getUser = JsonConvert.DeserializeObject<dynamic>(getResponse.Content!)!.data;

        Assert.Equal(expectedName, getUser.first_name.ToString());
        Assert.Equal(expectedEmail, getUser.email.ToString());
    }
}