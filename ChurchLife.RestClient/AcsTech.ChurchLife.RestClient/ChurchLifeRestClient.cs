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
        /// <param name="searchField"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <retu``rns></returns>
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

        #region Helpers
        #endregion
    }

    #region Entities
    
    /// <summary>
    /// 
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

    #endregion
}
