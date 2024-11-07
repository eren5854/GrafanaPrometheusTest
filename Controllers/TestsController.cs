using GrafanaPrometheusTest.Context;
using GrafanaPrometheusTest.DTOs;
using GrafanaPrometheusTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;

namespace GrafanaPrometheusTest.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TestsController(ApplicationDbContext context) : ControllerBase
{
    private static readonly Histogram GetAllRequestDuration = Metrics
        .CreateHistogram("get_all_request_duration_seconds",
                         "Duration of getAll requests in seconds.");

    private static readonly Histogram CreateRequestDuration = Metrics
        .CreateHistogram("create_request_duration_seconds",
                         "Duration of create requests in seconds.");

    [HttpGet]
    public IActionResult GetAll()
    {
        var stopwatch = Stopwatch.StartNew();

        var products = context.Products.ToList();

        stopwatch.Stop();
        GetAllRequestDuration.Observe(stopwatch.Elapsed.TotalSeconds);

        return Ok(products);
    }

    [HttpGet]
    public IActionResult GetProductById(Guid Id)
    {
        var product = context.Products.Where(p => p.Id == Id).FirstOrDefault();
        return Ok(new { Message = product });
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        var stopwatch = Stopwatch.StartNew();

        Product product = new()
        {
            Name = request.Name,
            Price = request.Price,
        };

        context.Add(product);
        context.SaveChanges();

        stopwatch.Stop();
        CreateRequestDuration.Observe(stopwatch.Elapsed.TotalSeconds);

        return Ok(new { Message = "Kayıt başarılı" });
    }

    [HttpGet]
    public IActionResult Delete(Guid Id)
    {
        Product? product = context.Products.Where(p => p.Id == Id).FirstOrDefault();
        if (product is null)
        {
            return BadRequest();
        }
        context.Remove(product);
        context.SaveChanges();
        return Ok(new { Message = "Silme başarılı" });
    }
}
