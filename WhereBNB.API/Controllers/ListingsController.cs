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
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using WhereBNB.API.Controllers.Parameters;
using WhereBNB.API.Model;
using WhereBNB.API.Repositories;

namespace WhereBNB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private IListingRepository ListingRepository { get; }
        private IRepository<SummaryListing> SummaryListingRepository { get; }

        public ListingsController(IListingRepository listingRepository,
            IRepository<SummaryListing> summaryListingRepository)
        {
            ListingRepository = listingRepository;
            SummaryListingRepository = summaryListingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListingParameters parameters)
        {
            if (parameters.Type == "geojson")
            {
                return Ok(await GetListingsFeatureCollection());
            }

            if (!parameters.Page.HasValue || !parameters.PageSize.HasValue)
            {
                return Ok(await ListingRepository.Count(parameters));
            }

            try
            {
                var listings = await ListingRepository.Get(parameters);
                return Ok(listings);
            }
            catch (SqlException)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var listing = await ListingRepository.GetById(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }

        private async Task<FeatureCollection> GetListingsFeatureCollection()
        {
            var listings = await SummaryListingRepository.GetAll();
            List<Feature> features = new();
            FeatureCollection featureCollection = new(features);

            foreach (var listing in listings)
            {
                if (!listing.Latitude.HasValue || !listing.Longitude.HasValue) continue;
                features.Add(new Feature(new Point(new Position(listing.Latitude.Value, listing.Longitude.Value)),
                    new {listing.Id}));
            }

            return featureCollection;
        }
    }
}