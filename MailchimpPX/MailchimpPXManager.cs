using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailChimp.Net;
using MailChimp.Net.Core;
      
namespace MailchimpPX
{
    public class MailchimpPXManager
    {
        private readonly MailChimp.Net.MailChimpManager manager;

        public MailchimpPXManager(string apiKey)
        {
            manager = new MailChimpManager(apiKey);
        }

        public List<MCList> GetLists()
        {
            var mc_lists = Task.Run(async () => await manager.Lists.GetAllAsync()).Result;
            var result = new List<MCList>();
            foreach (var item in mc_lists)
            {
                result.Add(new MCList { ID = item.Id, Name = item.Name });
            }
            return result;
        }

        public void AddEmailToList(string email, string listID, string first_name, string last_name, )
        {
            
        }


    }
}
