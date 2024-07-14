## Task 1
The instructions for getting started didn't mention anything about setting up the DB, so it crashed when creating a new user. I ran the command "dotnet ef database update" from the \tech-test-netcore\Todo folder to set up the DB, and then was able to use the site.

*Note* By having candidates fork your repo, it's possible for us to see the repos that others have created before us. When forking there is a link to all other forks, which would make it easy to cheat.


## Task 4
The task didn't specify what the "friendly" text should be. Given that the code was using ResponsiblePartyId I worked on the assumption that "Responsible Party" would be an understood term, like the whole concept of the ubiquitous language in DDD, but I did also consider putting something a little less formal like "Task Owner" as well. In a real world scenario I might have developed a better feel for the product & audience to know what to use, or if not would check with a product owner or similar if wording was important.

## Task 5
This spec feels quite vague. Would we just want the hiding to be in the context of the current view, in which case a Javascript-based approach would be optimal to keep it all in the browser. Might we want each list to remeber the state, or have a setting at the user level, if so then we'd want to extend the appropriate data models to persist this. My solution here is somewhere inbetween, so it isn't optimal, but I am at least trying to consider the alternatives were I working on some production code.

## Task 6
This change hits the requirement as specified, but I'm thinking that for a production-ready system we'd need to treat lists created by other users differently. Would we be able to see all of the other tasks that aren't assigned to us at all, or might we just get a read-only view of the other tasks, and only be allowed to alter the one that we've been given?

## General note
I like to take a TDD approach to writing code, but the changes here have been very UI-centric, or in the call to database. I'm taking a pragmatic approaching and going with the flow of the code that's already here, but I'd be thinking about how I could add some integration tests, or put in some more structure to the unit tests that would help them look at the sort of changes that are being made if this were a production codebase.

## Task 7 
* There is no detail on what the ranks should be, beyond being able to order by it. We already have the importance field, so I'm presuming that we want something conceptually different, maybe like for sorting the all of the tasks into the order that we plan to do them, so we might have as many different ranks as we have items in a list.  
If this is how we want rank to be used, we might also want to think about making these values unique, or at least making it obvious in the UI if there are clashes. 
For this sort of ordering I'd want 1 to be at the top of the list, with it ordered ascending from that. Any item without a rank will come at the bottom, and for consistency I apply secondary ordering of importance when viewing by rank and vice versa.
As we're updating existing entries, we need their ranks to make sense, so I used a nullable int here to denote the abscense of a rank.
I prefer to avoid using magic values such as defaulting it to zero when possible, nulls have their own issues, but it feels more axiomatic to use them for missing data, even though I've got the UI set up to not allow 0 as a value when creating or editing tasks.

* We already have unit tests around the populating the TodoItemEditFields from the TodoItem, so as well as adding rank to that, it seemed sensible to add some matching tests for reverse of that process since I was changing that as well. A few fields were missing from the original tests, I'm not taking the extra time to add them in, but I'm noting their absence here, and would consider adding them in a production codebase.

* Toggling the order with a bool is quite limiting, but it just about suffices for now.

## Task 8
This seemed like something that would be best done in Javascript, so that the server doesn't need to make all these remote 3rd party calls. 
I burned a lot of time doing this, and ended up getting various CORS errors, so I threw that away and started a server-side version instead. 
This is a rudimentary implementation that will call the service repeatedly for the same user if they are present multiple times. 
Given more time I'd make the service cache calls to Gravatar so we could do a quick dictionary lookup instead of an HTTP API call for each user,
use an HttpClientFactory through dependency injection rather than newing up an HttpClient myself, and maybe bring it in to the loading of the data and the view model, rather than mapping it in the view.

