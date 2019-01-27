# Team Theta 2019

Acumatica Hackathon Team Theta 2019

## MailchimpPXmanager API interface

    using MailchimpPX;

    var manager = new MailchimpPXManager([api key])
	List<MCList> lists = manager.GetLists()
	
	// MCList.Id, MCList.Name
	
	IEnumerable<Activity> activity = manager.GetActivities("asdf6789", "jeremy@example.com")
	// returns Mailchimp.Net.Models.Activity
