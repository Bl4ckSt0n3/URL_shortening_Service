using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URL_shortening_Service.Models.DTOs;
using URL_shortening_Service.Models.Database;
using URL_shortening_Service.Services.Interfaces;
using URL_shortening_Service.Models.Entities;
using System.Web;

namespace URL_shortening_Service.Services
{
    public class ShortenerService : IShortener
    {

        private const int MaximumCharacterLength = 6;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random = new Random();
        private readonly UrlDataDbContext _dbContext;

        public ShortenerService(UrlDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponseDto<string>> RedirectUrl(string codeId)
        {
            // var str = new Uri(codeId);
            // string decodedUrl = HttpUtility.UrlDecode(codeId);
            var shortenedUrl = await _dbContext.ShortenedUrl.FirstOrDefaultAsync(s => s.Code == codeId); //decodedUrl.ToString(
            var response = new ServiceResponseDto<string>
            {
                Data = shortenedUrl?.LongUrl,
                Success = true
            };
            return response;
        }

        public async Task<ServiceResponseDto<string>> GetLongUrl(string shortUrl)
        {
            var longUrl = await _dbContext.ShortenedUrl.FirstOrDefaultAsync(s => s.ShortUrl == shortUrl);
            var response = new ServiceResponseDto<string>
            {
                Data = longUrl?.LongUrl,
                Success = true
            };
            return response;
        }
        
        public async Task<ServiceResponseDto<string>> AddUrl(string url, string shortUrl)
        {
            var uniqueCode =  GenerateUrlCode();
            while (_dbContext.ShortenedUrl.Any(e => e.Code == uniqueCode))
            {
                uniqueCode = GenerateUrlCode();
            }

            var urlData = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = url,
                ShortUrl = shortUrl + uniqueCode,
                Code = uniqueCode,
                Created = DateTime.UtcNow
            };
            _dbContext.Add(urlData);
            await _dbContext.SaveChangesAsync();
            
            var response = new ServiceResponseDto<string> 
            {
                Data = urlData.ShortUrl,
                Success = true
            };
            return response;
        }

        private string GenerateUrlCode() 
        {
            var urlCodeChars = new char[MaximumCharacterLength];
            for (var i = 0; i < MaximumCharacterLength; i++)
            {
                int index = _random.Next(Characters.Length - 1);
                urlCodeChars[i] = Characters[index];
            }
            var uniqueCode = new string(urlCodeChars);
            return uniqueCode;
        }
    }
}