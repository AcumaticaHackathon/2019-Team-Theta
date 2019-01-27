using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MailChimp.Net;
using MailChimp.Net.Models;
      
namespace MailchimpPX
{
    public class MailchimpPXManager
    {
        private readonly MailChimpManager manager;

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

        public Member GetMember(string listId, string email)
        {
            var email_md5 = CalculateMD5Hash(email);
            Member member;
            try
            {
                member = Task.Run(async () => await manager.Members.GetAsync(listId, email_md5)).Result;
            } catch (AggregateException ae)
            {
                throw ae.InnerException;
            }
            return member;
        }

        /*public void AddEmailToList(string email, string listID, string first_name, string last_name)
        {
            
        }
        */
        public List<Activity> GetActivities(string listId, string email)
        {
            var email_md5 = CalculateMD5Hash(email);
            IEnumerable<Activity> mc_activities;
            try
            {
                mc_activities = Task.Run(async () => await manager.Members.GetActivitiesAsync(listId, email_md5)).Result;
            } catch (AggregateException ae)
            {
                throw ae.InnerException;
            }
            
            return mc_activities.ToList();
        }

        // From https://blogs.msdn.microsoft.com/csharpfaq/2006/10/09/how-do-i-calculate-a-md5-hash-from-a-string/
        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

    }
}
