#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using WhereBNB.API.Model;
using WhereBNB.API.Repositories;
using WhereBNB.API.Util;

namespace WhereBNB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private IRepository<Listing> ListingRepository { get; set; }
        private IRepository<SummaryListing> SummaryListingRepository { get; set; }

        public ListingsController(IRepository<Listing> listingRepository,
            IRepository<SummaryListing> summaryListingRepository)
        {
            ListingRepository = listingRepository;
            SummaryListingRepository = summaryListingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string? type)
        {
            var listings = await SummaryListingRepository.GetAll();
            if (type == "geojson")
            {
                List<Feature> features = new();
                FeatureCollection featureCollection = new(features);
                
                foreach (var listing in listings)
                {
                    if (!listing.Latitude.HasValue || !listing.Longitude.HasValue) continue;
                    FixPosition.FixSummaryListingPosition(listing);
                    features.Add(new Feature(new Point(new Position(listing.Latitude.Value, listing.Longitude.Value)), new {listing.Id}));
                }

                return Ok(featureCollection);
            }
            return Ok(listings.Take(100));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var listing = await ListingRepository.GetById(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }
    }
}