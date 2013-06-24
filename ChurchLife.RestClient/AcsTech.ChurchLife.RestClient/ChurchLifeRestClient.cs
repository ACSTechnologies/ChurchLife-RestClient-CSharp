using System;
using System.Collections.Generic;
using RestSharp;

namespace AcsTech.ChurchLife.RestClient
{
    public class ChurchLifeRestClient
    {  
        public int SiteNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int DefaultPageSize { get; set; }

        public ChurchLifeRestClient(int siteNumber, string username, string password)
        {
            SiteNumber = siteNumber;
            Username = username;
            Password = password;
            DefaultPageSize = 10;
        }

        #region Individuals

        //https://api.accessacs.com/v2/individuals?searchField=<last name, goes-by name, or first name>&pageIndex=<0>&pageSize=<int>

        /// <summary>
        /// Returns a list of individuals the user has rights to view.
        /// http://wiki.acstechnologies.com/display/DevCom/Individuals
        /// </summary>
        /// <param name="searchField">Search the criteria entered; includes last name, goes-by name, and first name.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<IndividualProxy> IndividualsIndex(string searchField, int pageIndex = 0, int pageSize = 0)
        {
            // this sould be a helper so it is just one line
            if (pageSize == 0)
            {
                pageSize = DefaultPageSize;
            }

            var client = new RestSharp.RestClient("https://api.accessacs.com/v2");
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);
            client.AddDefaultHeader("Sitenumber", SiteNumber.ToString());

            var request = new RestSharp.RestRequest("individuals", Method.GET);
            request.AddParameter("searchField", searchField);
            request.AddParameter("pageIndex", pageIndex);
            request.AddParameter("pageSize", pageSize);
            
            var response = client.Execute<PagedList<IndividualProxy>>(request);
            return response.Data;
        }

        /*
         * call names should follow convention
         * Index (Get List)
         * Show (Get One)
         * Create (Create One)
         * Update (Update One)
         * Destroy (Delete One)
         */ 
        #endregion

        #region Events
        //https://secure.accessacs.com/api_accessacs_mobile/v2/<sitenumber>/events?startDate=<date>&stopDate=<date>&pageIndex=<0-based int>&pageSize<int>
        /// <summary>
        /// Returns a list of events regardless of the calendar they belong to
        /// http://wiki.acstechnologies.com/display/DevCom/Events+List+Wildcard
        /// </summary>
        /// <param name="startDate">The first date in the date range you want returned</param>
        /// <param name="stopDate"> The last date in the date range you want returned</param>
        /// <param name="pageIndex">(Optional) Page number for the search results, begins with the 0 value entered for the first set of results</param>
        /// <param name="pageSize">(Optional) Number of results to return per page</param>
        /// <returns></returns>
        public PagedList<Event> EventsIndex(DateTime startDate, DateTime stopDate, int pageIndex = 0, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                pageSize = DefaultPageSize;
            }

            var client = new RestSharp.RestClient("https://secure.accessacs.com/api_accessacs_mobile/v2");
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);
            client.AddDefaultHeader("Sitenumber", SiteNumber.ToString());

            var request = new RestSharp.RestRequest("{sitenumber}/events", Method.GET);
            request.AddUrlSegment("sitenumber", SiteNumber.ToString());
            request.AddParameter("startDate", startDate.ToString());
            request.AddParameter("stopDate", stopDate.ToString());
            request.AddParameter("pageIndex", pageIndex);
            request.AddParameter("pageSize", pageSize);

            var response = client.Execute<PagedList<Event>>(request);
            return response.Data;
        }
        #endregion

        #region Helpers
        #endregion
    }

    #region Entities
    
    /// <summary>
    /// Paged result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public List<T> Page { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Short representation of an individual data returned from a list result
    /// </summary>
    public class IndividualProxy
    {
        public int IndvId { get; set; }
        public int FamId { get; set; }
        public string FamilyPosition { get; set; }
        public string FriendlyName { get; set; }
        public string FullName { get; set; }
        public Uri PictureUrl { get; set; }
    }

    /// <summary>
    /// Representation of an event's data
    /// </summary>
    public class Event
    {
        public string Description { get; set; }
        public Guid EventDateId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public Guid? EventTypeId { get; set; }
        public bool IsPublished { get; set; }
        public Guid LocationId { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public string Status { get; set; }
        public string CalendarName { get; set; }
        public bool IsRecurringEvent { get; set; }
        public Guid CalendarId { get; set; }
        public bool AllowRegistration { get; set; }
    }

    #endregion
}
