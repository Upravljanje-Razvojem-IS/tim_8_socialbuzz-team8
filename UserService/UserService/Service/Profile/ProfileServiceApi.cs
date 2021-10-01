using Newtonsoft.Json;
using ProfileService.Models;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Service.ProfileServiceApi
{
    public class ProfilesServiceApi : IProfilesServiceApi
    {
        private readonly HttpClient _profileService;
        private const string API = "http://localhost:10498/api/";
        private const string CORPORATE_USER_DETAILS = "CorporateUserDetails/";
        private const string USER_DETAILS = "UserDetails/";

        public ProfilesServiceApi(HttpClient profileService)
        {
            _profileService = profileService;
            _profileService.DefaultRequestHeaders.Accept.Clear();
            _profileService.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task DeleteUserDetails(Guid id)
        {
            HttpResponseMessage res = await _profileService.DeleteAsync(API + USER_DETAILS + id.ToString());
            if(!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task<List<CorporateUserDetailsDto>> GetCorporateUserDetails()
        {
            HttpResponseMessage res = await _profileService.GetAsync(API + CORPORATE_USER_DETAILS);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
            return await res.Content.ReadAsAsync<List<CorporateUserDetailsDto>>();
        }

        public async Task<CorporateUserDetailsDto> GetCorporateUserDetailsById(Guid id)
        {
            HttpResponseMessage res = await _profileService.GetAsync(API + CORPORATE_USER_DETAILS + id);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
            return await res.Content.ReadAsAsync<CorporateUserDetailsDto>();
        }

        public async Task<List<UserDetailsDto>> GetUserDetails()
        {
            HttpResponseMessage res = await _profileService.GetAsync(API + USER_DETAILS);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
            return await res.Content.ReadAsAsync<List<UserDetailsDto>>();
        }

        public async Task<UserDetailsDto> GetUserDetailsById(Guid id)
        {
            HttpResponseMessage res = await _profileService.GetAsync(API + USER_DETAILS + id);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
            return await res.Content.ReadAsAsync<UserDetailsDto>();
        }

        public async Task InserCorporatetUserDetails(CorporateUserDetailsMutationDto corporateUserDetails)
        {
            //HttpContent postContent = new StringContent(content:JsonConvert.SerializeObject(corporateUserDetails), encoding: Encoding.UTF8, mediaType: "application/json");
            HttpResponseMessage res = await _profileService.PostAsJsonAsync<CorporateUserDetailsMutationDto>(API + CORPORATE_USER_DETAILS, corporateUserDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task InsertUserDetails(UserMutationDto userDetails)
        {
            //HttpContent postContent = new StringContent(content: JsonConvert.SerializeObject(userDetails), encoding: Encoding.UTF8, mediaType: "application/json");
            HttpResponseMessage res = await _profileService.PostAsJsonAsync<UserMutationDto>(API + USER_DETAILS, userDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task UpdateCorporateUserDetails(CorporateUserDetailsMutationDto newCorporateUserDetails, Guid id)
        {
            HttpContent putContent = new StringContent(JsonConvert.SerializeObject(newCorporateUserDetails));
            HttpResponseMessage res = await _profileService.PutAsJsonAsync(API + CORPORATE_USER_DETAILS + id, putContent);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task UpdateUserDetails(UserMutationDto newUserDetails, Guid id)
        {
            HttpContent putContent = new StringContent(JsonConvert.SerializeObject(newUserDetails));
            HttpResponseMessage res = await _profileService.PutAsJsonAsync(API + USER_DETAILS + id, putContent);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }
    }
}
