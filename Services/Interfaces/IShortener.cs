using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URL_shortening_Service.Models.DTOs;
using URL_shortening_Service.Models.Entities;

namespace URL_shortening_Service.Services.Interfaces
{
    public interface IShortener
    {
        Task<ServiceResponseDto<string>> AddUrl(string url, string shortUrl);
        Task<ServiceResponseDto<string>> RedirectUrl(string shortUrl);
        Task<ServiceResponseDto<string>> GetLongUrl(string shortUrl);
        Task<ServiceResponseDto<List<ShortenedUrl>>> GetAllUrls();
    }
}