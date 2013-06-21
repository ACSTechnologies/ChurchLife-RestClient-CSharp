using System.Collections.Generic;

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
        /// <returns></returns>
        public PagedList<IndividualProxy> IndividualsIndex(string searchField, int pageIndex = 0, int pageSize = 0)
        {
            // this sould be a helper so it is just one line
            if (pageSize == 0)
            {
                pageSize = DefaultPageSize;
            }

            var result = new PagedList<IndividualProxy>();
            /*
             {
                "Page": [
                    {
                        "IndvId": 76,
                        "FamId": 1042,
                        "FamilyPosition": "Head",
                        "FriendlyName": "James(Jim) Aaron",
                        "FullName": "Aaron, James (Jim)",
                        "PictureUrl": "https://accesspict/14447/12704_11_PMT.jpg"
                    }
                ],
                "PageCount": 23,
                "PageIndex": 0,
                "PageSize": 1 
             } 
             */
            return result;
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
    public class PagedList<T> : List<T>
    {
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
        //"FamId": 1042,
        //"FamilyPosition": "Head",
        public string FriendlyName { get; set; }
        //"FullName": "Aaron, James (Jim)",
        //"PictureUrl": "https://accesspict/14447/12704_11_PMT.jpg" 
    }

    #endregion
}
