using System;
using WhereBNB.API.Model;

namespace WhereBNB.API.Util
{
    public static class FixPosition
    {
        public static void FixSummaryListingPosition(SummaryListing listing)
        {
            if (!listing.Latitude.HasValue || !listing.Longitude.HasValue) return;
            listing.Latitude = FixLat(listing.Latitude.Value);
            listing.Longitude = FixLong(listing.Longitude.Value);
        }

        public static double FixLong(double longitude)
        {
            return double.Parse($"{longitude.ToString()[0]}.{longitude.ToString().Substring(1)}");
        }
        
        public static double FixLat(double latitude)
        {
            return double.Parse($"{latitude.ToString().Substring(0,2)}.{latitude.ToString().Substring(2)}");
        }
    }
}