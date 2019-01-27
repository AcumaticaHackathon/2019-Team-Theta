# Team Theta 2019

Acumatica Hackathon Team Theta 2019

## MailchimpPXmanager API interface

    using MailchimpPX;

    var manager = new MailchimpPXManager([api key]);
	
	List<MCList> lists = manager.GetLists();
	// MCList.Id, MCList.Name
	
	manager.GetMember("asdf6789", "jeremy@example.com");
	// returns Mailchimp.Net.Models.Member
	
	List<Activity> activity = manager.GetActivities("asdf6789", "jeremy@example.com");
	// returns Mailchimp.Net.Models.Activity
	
Contacts can be subscribed to a Mailchimp list and are linked back to the contact via the required ContactID field. If they are already subscribed, then their information is updated:

    using MailchimpPX;
	using MailChimp.Net.Models;
	
	var manager = new MailchimpPXManager([api key]);
	// Simple:
	manager.AddEmailToList("jeremy@example.com", "asdf6789", "Jeremy", "Osterhouse", 789);
	// Where "asdf6789" is the Mailchimp list ID and 789 is the Acumatica Contact ID
	
	// Or with full control:
	var member = new Member
	{
		EmailAddress = "jeremy@example.com",
		MergeFields = new Dictionary<string, object>
		{
			{ "FNAME", "Jeremy" },
			{ "LNAME", "Osterhouse" },
			{ "type", "Customer" }
		}
	};
	manager.AddEmailToList("asdf6789", member, 789)
	
In either case, the Acumatica Contact ID is added to the Merge Fields, the current time is used at the signup timestamp, and the status is set to "subscribed" if the they are new. The new or updated member is returned.
	
