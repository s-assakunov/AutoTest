using System.Text.Json;
using System.Net.Http.Json;

namespace Test1
{
    public class Test1
    {
        private static HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://reqres.in/api/")
            };
            client.DefaultRequestHeaders.Add("x-api-key", "free_user_3Hs5mIu3RkuJn9BiAvVtw540JdH");             

        }
        [Test]
        public async Task Test()
        {
            using HttpResponseMessage response = await client.GetAsync("users/2");
            response.EnsureSuccessStatusCode();
        }
        [Test]
        public async Task Test2()
        {
            using HttpResponseMessage response = await client.GetAsync("users/2");
            string jsonGet = await response.Content.ReadAsStringAsync();
            UserResponseDTO userResponse = JsonSerializer.Deserialize<UserResponseDTO>(jsonGet);
            UserDataDTO user = userResponse.Data;
        }

        [Test]
        public async Task Test3()
        {
            CreateUserRequestDTO newUser = new CreateUserRequestDTO
            {
                Name = "Sema",
                Job = "QA Engineer"
            };

            using HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);
            response.EnsureSuccessStatusCode();

            string jsonPost = await response.Content.ReadAsStringAsync();
            CreateUserResponseDTO createdUser = JsonSerializer.Deserialize<CreateUserResponseDTO>(jsonPost);
        }

        [Test]
        public async Task Test4()
        {
            CreateUserRequestDTO updateUser = new CreateUserRequestDTO
            {
                Name = "Sema",
                Job = "AutoQA"
            };
            using HttpResponseMessage response = await client.PutAsJsonAsync("users/2", updateUser);
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Test5()
        {

            using HttpResponseMessage response = await client.DeleteAsync("users/2");
            response.EnsureSuccessStatusCode();

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}