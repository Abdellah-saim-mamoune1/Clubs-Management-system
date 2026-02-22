using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Dtos.ProgressDtos;
using EventsManagement.Interfaces.Repositories.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EventsManagement.Repositories.Student
{
  
        public class AuthenticationRepository : IAuthenticationRepository
        {

        readonly HttpClient _httpClient;
        AppDbContext _db;
        IConfiguration _Configuration;
        public AuthenticationRepository(
            IHttpClientFactory httpClientFactory,
            IConfiguration Configuration,
            AppDbContext db)
        {
            _httpClient = httpClientFactory.CreateClient("ProgressAPI");
            _db = db;
            _Configuration= Configuration;
        }

        public async Task Registre(string uuid, string token)
        {
            try
            {
              
                var data = await GetAsync(uuid, token);

                if (data == null)
                    throw new Exception();

                Classes.User user = new Classes.User
                {
                    Uuid =Guid.Parse( uuid),
                    Age = data.Age,
                    Degree = data.Degree,
                    YearOfDegree = data.YearOfDegree,
                    FullName = data.FullName,
                    ImageUrl = data.ImageUrl,
                };
                _db.Add(user);
                await _db.SaveChangesAsync();

            }
            catch { throw; }
        }




        public async Task<(string? uuid, string? token)> LoginStudentAsync(LoginDto form)
        {

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"authentication/v1/"
            );

         

            var json = JsonSerializer.Serialize(form);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseContent);

            (string? uuid, string? token) res;

            doc.RootElement.TryGetProperty("uuid", out var uuidElement);
            doc.RootElement.TryGetProperty("token", out var tokenElement);


            res.uuid = uuidElement.GetString();
            res.token = tokenElement.GetString();
            return res;

        }



        private async Task<StudentGetDto?> GetAsync(string uuid, string progressToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"infos/bac/{Guid.Parse(uuid)}/dias"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue(progressToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var students = JsonSerializer.Deserialize<List<StudentApiResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (students == null || students.Count == 0)
                return null;

            var student = students.OrderByDescending(s => s.anneeAcademiqueCode).First();

            return new StudentGetDto
            {
                Id = student.Id,
                FullName = $"{student.individuPrenomLatin} {student.individuNomLatin}",
                Age = CalculateAge(DateTime.Parse(student.individuDateNaissance)),
                Degree = student.niveauLibelleLongLt,
                YearOfDegree = student.anneeAcademiqueCode,
                ImageUrl = $"/api/infos/image/{student.uuid}"
            };
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

     
    }


}

