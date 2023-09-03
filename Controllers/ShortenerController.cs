using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using URL_shortening_Service.Models.DTOs;
using URL_shortening_Service.Models.Entities;
using URL_shortening_Service.Services.Interfaces;

namespace URL_shortening_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public class ShortenerController : ControllerBase
{
    private readonly IShortener _shortenerService;
    public ShortenerController(IShortener shortenerService)
    {
        _shortenerService = shortenerService;
    }
    [HttpGet("{codeId}")]
    public async Task<IActionResult> RedirectTo(string codeId)
    {
        var r = this.Request.GetDisplayUrl();
        Console.WriteLine(r);
        var url = await _shortenerService.RedirectUrl(codeId);

        return url.Data == null ? NotFound() : Redirect(url.Data);
    }
    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten(UrlDataDto requestUrlData)
    {
        if (!Uri.TryCreate(requestUrlData.Url, UriKind.Absolute, out _))
        {
            return BadRequest("URL is invalid!");
        }
        var uri = new Uri(requestUrlData.Url);
        var shortUrl = $"http://{uri.Host}/"; //$"{this.Request.Scheme}://{this.Request.Host}/api/";
        var response = await _shortenerService.AddUrl(requestUrlData.Url, shortUrl);

        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost("getlongurl")]
    public async Task<IActionResult> GetLongUrl(UrlDataDto requestUrlData)
    {
        if (!Uri.TryCreate(requestUrlData.Url, UriKind.Absolute, out _))
        {
            return BadRequest("URL is invalid!");
        }
        var response = await _shortenerService.GetLongUrl(requestUrlData.Url);
        
        return response == null ? NotFound() : Ok(response);
    }
    [HttpGet("getAll")]
    public async Task<ActionResult<ServiceResponseDto<List<ShortenedUrl>>>> GetAllUrls()
    {
        var response = await _shortenerService.GetAllUrls();
        return response == null ? NotFound() : Ok(response);
    }
}
