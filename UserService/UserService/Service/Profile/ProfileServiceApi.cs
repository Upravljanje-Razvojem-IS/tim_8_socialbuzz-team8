using Newtonsoft.Json;
using ProfileService.Models;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task DeleteUserDetails(Guid id, string token)
        {
            _profileService.DefaultRequestHeaders.Add("Authorization", token);
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
                return null;
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
                return null;
            }
            return await res.Content.ReadAsAsync<UserDetailsDto>();
        }

        public async Task InserCorporatetUserDetails(CorporateUserDetailsMutationDto corporateUserDetails)
        {
            HttpResponseMessage res = await _profileService.PostAsJsonAsync<CorporateUserDetailsMutationDto>(API + CORPORATE_USER_DETAILS, corporateUserDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task InsertUserDetails(UserMutationDto userDetails)
        {
            HttpResponseMessage res = await _profileService.PostAsJsonAsync<UserMutationDto>(API + USER_DETAILS, userDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task UpdateCorporateUserDetails(CorporateUserDetailsMutationDto newCorporateUserDetails, Guid id, string token)
        {
            _profileService.DefaultRequestHeaders.Add("Authorization", token);
            HttpResponseMessage res = await _profileService.PutAsJsonAsync< CorporateUserDetailsMutationDto>(API + CORPORATE_USER_DETAILS + id, newCorporateUserDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }

        public async Task UpdateUserDetails(UserMutationDto newUserDetails, Guid id, string token)
        {
            _profileService.DefaultRequestHeaders.Add("Authorization", token);
            HttpResponseMessage res = await _profileService.PutAsJsonAsync<UserMutationDto>(API + USER_DETAILS + id, newUserDetails);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(res.Content.ToString());
            }
        }
    }
}
